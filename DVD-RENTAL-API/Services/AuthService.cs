using DVD_RENTAL_API.DTOs;
using DVD_RENTAL_API.Models;
using DVD_RENTAL_API.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DVD_RENTAL_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        // Registration method for creating a new user
        public async Task<UserResponseDTO> RegisterAsync(UserDTO userDTO)
        {
            var existingUser = await _userRepository.GetByEmailAsync(userDTO.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists with this email.");
            }

            // Hash password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            var newUser = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = hashedPassword,
                NIC = userDTO.NIC,
                Phone = userDTO.Phone,
                Role = userDTO.Role ?? "user" // Default to "user" role if not provided
            };

            await _userRepository.RegisterAsync(newUser);
            var isSaved = await _userRepository.SaveChangesAsync();

            if (!isSaved)
            {
                throw new Exception("User registration failed.");
            }

            return new UserResponseDTO
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Role = newUser.Role
                
            };
        }

        // Login method that validates user credentials and returns a JWT token
        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userRepository.GetByEmailAsync(loginDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // Generate JWT token
            return GenerateJwtToken(user);
        }

        // Method to generate JWT token
        private string GenerateJwtToken(User user)
        {
            var claimslist = new List<Claim>();
            {
                claimslist.Add(new Claim("Id", user.Id.ToString()));
                claimslist.Add(new Claim("UserName",user.Name ));
                claimslist.Add(new Claim("Nic", user.NIC));
                claimslist.Add(new Claim("Role", user.Role));


            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claimslist,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

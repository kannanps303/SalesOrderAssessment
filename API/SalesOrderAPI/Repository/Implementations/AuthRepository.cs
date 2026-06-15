using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using SalesOrderAPI.Data;
using SalesOrderAPI.Models;
using SalesOrderAPI.Repository.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;


namespace SalesOrderAPI.Repository.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DbContextADO _db;
        private readonly IConfiguration _configuration;

        public AuthRepository(
            DbContextADO db,
            IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public string Login(LoginRequest request)
        {
            try {
                using SqlConnection con = _db.CreateConnection();

                SqlCommand cmd = new SqlCommand("sp_Login", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Username",
                    request.Username);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    throw new Exception("Invalid Username");
                }

                int userId = Convert.ToInt32(reader["Id"]);
                string username = reader["Username"].ToString()!;
                string dbPassword = reader["PasswordHash"].ToString()!;
                string role = reader["RoleName"].ToString()!;

                // Password Validation
                if (dbPassword != request.Password)
                {
                    throw new Exception("Invalid Password");
                }

                return GenerateJwtToken(userId, username, role);
            }
            catch(Exception ex) {
                return null;
                    }
            
        }

        private string GenerateJwtToken(
            int userId,
            string username,
            string role)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
            new Claim(ClaimTypes.Name,username),
            new Claim(ClaimTypes.Role,role)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}

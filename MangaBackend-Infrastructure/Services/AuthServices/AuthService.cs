using MangaBackend.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MangaBackend.Domain.Interfaces.IAuthService;
using MangaBackend.Infrastructure.Auth.JWT;
using MangaBackend_Application.DTOS.AuthDtos;
using MangaBackend_Application.Common.ResponseType;
using ArcadeGameBackend.Application.MiddleWare;

namespace MangaBackend.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMongoCollection<Tb_User> _users;
        private readonly string _secretKey;

        public AuthService(IConfiguration configuration)
        {
            var mongoConnectionString = configuration["MongoDbSettings:ConnectionString"];
            var mongoDatabaseName = configuration["MongoDbSettings:DatabaseName"];

            if (string.IsNullOrEmpty(mongoConnectionString))
                throw new ArgumentNullException(nameof(mongoConnectionString), "MongoDb connection string is missing.");
            if (string.IsNullOrEmpty(mongoDatabaseName))
                throw new ArgumentNullException(nameof(mongoDatabaseName), "MongoDb database name is missing.");

            var client = new MongoClient(mongoConnectionString);
            var database = client.GetDatabase(mongoDatabaseName);
            _users = database.GetCollection<Tb_User>("Users");

            _secretKey = configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException(nameof(_secretKey), "JWT Secret Key is missing.");
        }


        public ResponseModel Login(LoginRequest request)
        {
            try
            {
                if ((string.IsNullOrEmpty(request.Email) && string.IsNullOrEmpty(request.Username)) || string.IsNullOrEmpty(request.Password))
                {
                    return ResponseData.NotSuccessResponse("Username or Email and Password are required.");
                }

                Tb_User user = null;

                if (!string.IsNullOrEmpty(request.Email) && request.Email != "string")
                {
                    user = _users.Find(u => u.Email.ToLower() == request.Email.ToLower()).FirstOrDefault();
                }

                if (user == null && !string.IsNullOrEmpty(request.Username) && request.Username != "string")
                {
                    user = _users.Find(u => u.Username.ToLower() == request.Username.ToLower()).FirstOrDefault();
                }

                if (user == null)
                {
                    return ResponseData.NotSuccessResponse("Invalid email/username or password.");
                }

                if (user.Password.Trim() != request.Password.Trim())
                {
                    return ResponseData.NotSuccessResponse("Invalid email/username or password.");
                }

                var token = JwtTokenHelper.GenerateToken(
                    user.Email ?? string.Empty,
                    user.Id ?? string.Empty,
                    user.Role,
                    _secretKey
                );

                var loginResponse = new LoginResponse
                {
                    Token = token,
                    Id = user.Id ?? string.Empty,
                    Username = user.Username ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    Password = user.Password ?? string.Empty,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt
                };

                var response = ResponseData.GetSuccessResponse(loginResponse, "Login successful.");
                response.Success = true;
                response.IsError = false;
                response.statusCode = 200;

                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = ResponseData.ErrorResponse(string.Empty, ex.Message);
                errorResponse.Success = false;
                errorResponse.IsError = true;
                errorResponse.statusCode = 500;
                return errorResponse;
            }
        }




    }
}

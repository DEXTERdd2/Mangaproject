
using MangaBackend_Application.Common.ResponseType;
using MangaBackend.Domain.Interfaces.ITb_UserService;
using MangaBackend.Infrastructure.Data;
using ArcadeGameBackend.Application.MiddleWare;
using MangaBackend_Application.DTOS.Tb_UserDtos;

namespace MangaBackend.Infrastructure.Services.Tb_UserServices
{
    public class UserService : IUserService
    {
        private readonly MongoUserAccess _mongo;

        public UserService(MongoUserAccess mongo)
        {
            _mongo = mongo;
        }

        public ResponseModel GetAllUsers()
        {
            try
            {
                var users = _mongo.GetAll();
                return ResponseData.FoundSuccessResponse(users);
            }
            catch (Exception ex)
            {
                return ResponseData.ErrorResponse("Error fetching users", ex.Message);
            }
        }

        public ResponseModel GetUserById(string id)
        {
            try
            {
                var user = _mongo.GetById(id);
                return user != null
                    ? ResponseData.FoundSuccessResponse(user)
                    : ResponseData.NotSuccessResponse("User not found");
            }
            catch (Exception ex)
            {
                return ResponseData.ErrorResponse("Error fetching user", ex.Message);
            }
        }

        public ResponseModel AddUser(UserDto dto)
        {
            try
            {
                dto.CreatedAt = DateTime.UtcNow;
                dto.Role = string.IsNullOrWhiteSpace(dto.Role) || dto.Role == "string"
                ? "User"
                : dto.Role;

                if (string.IsNullOrEmpty(dto.Id) || dto.Id == "string")
                {
                    dto.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(); 
                }

               
                if (_mongo.Exists(dto.Id, dto.Email, dto.Username))
                {
                    return ResponseData.NotSuccessResponse("Id, Email or Username already exists.");
                }

                // 🔥 Recovery Codes generate kar rahe hain:
                dto.RecoveryCode1 = GenerateRecoveryCode();
                dto.RecoveryCode2 = GenerateRecoveryCode();

                _mongo.Add(dto);
                return ResponseData.SaveResponse(new
                {
                    Message = "User created successfully",
                    RecoveryCode1 = dto.RecoveryCode1,
                    RecoveryCode2 = dto.RecoveryCode2
                });
            }
            catch (Exception ex)
            {
                return ResponseData.ErrorResponse("Error creating user", ex.Message);
            }
        }

        private string GenerateRecoveryCode(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public ResponseModel UpdateUser(string id, UserDto dto)
        {
            try
            {
                var result = _mongo.Update(id, dto);
                return result
                    ? ResponseData.SaveResponse("User updated successfully")
                    : ResponseData.NotSuccessResponse("User not found");
            }
            catch (Exception ex)
            {
                return ResponseData.ErrorResponse("Error updating user", ex.Message);
            }
        }

        public ResponseModel DeleteUser(string id)
        {
            try
            {
                var result = _mongo.Delete(id);
                return result
                    ? ResponseData.DeleteSuccessResponse("User deleted successfully")
                    : ResponseData.NotSuccessResponse("User not found");
            }
            catch (Exception ex)
            {
                return ResponseData.ErrorResponse("Error deleting user", ex.Message);
            }
        }

        public UserDto? GetUserByRecoveryCode(string email, string recoveryCode)
        {
            var allUsers = _mongo.GetAll();
            return allUsers.FirstOrDefault(u =>
                u.Email.ToLower() == email.ToLower() &&
                (u.RecoveryCode1 == recoveryCode || u.RecoveryCode2 == recoveryCode));
        }
    }
}

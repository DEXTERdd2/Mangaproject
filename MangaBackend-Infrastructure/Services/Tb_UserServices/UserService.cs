
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
                dto.Role ??= "User";
                if (string.IsNullOrWhiteSpace(dto.Id) || dto.Id == "string")
                {
                    dto.Id = null; 
                }
                
                _mongo.Add(dto);
                return ResponseData.SaveResponse("User created successfully");
            }
            catch (Exception ex)
            {
                return ResponseData.ErrorResponse("Error creating user", ex.Message);
            }
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
    }
}

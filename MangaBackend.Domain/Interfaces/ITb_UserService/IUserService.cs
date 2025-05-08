
using MangaBackend_Application.Common.ResponseType;
using MangaBackend_Application.DTOS.Tb_UserDtos;

namespace MangaBackend.Domain.Interfaces.ITb_UserService
{
    public interface IUserService
    {
        ResponseModel GetAllUsers();
        ResponseModel GetUserById(string id);
        ResponseModel AddUser(UserDto dto);
        ResponseModel UpdateUser(string id, UserDto dto);
        ResponseModel DeleteUser(string id);
        UserDto? GetUserByRecoveryCode(string email, string recoveryCode);
    }
}

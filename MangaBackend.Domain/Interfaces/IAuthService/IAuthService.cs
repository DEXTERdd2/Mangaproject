using MangaBackend_Application.Common.ResponseType;
using MangaBackend_Application.DTOS.AuthDtos;

namespace MangaBackend.Domain.Interfaces.IAuthService
{
    public interface IAuthService
    {
        ResponseModel Login(LoginRequest request);
    }
}

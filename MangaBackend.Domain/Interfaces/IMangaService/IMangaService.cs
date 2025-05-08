
using MangaBackend_Application.Common.ResponseType;
using MangaBackend_Application.DTOS.MangaDtos;

namespace MangaBackend.Domain.Interfaces.IMangaService
{
    public interface IMangaService
    {
        ResponseModel GetAllMangas();
        ResponseModel GetMangaBySlug(string slug);
        ResponseModel GetFeaturedMangas();
        ResponseModel AddManga(MangaDto dto);
    }
}

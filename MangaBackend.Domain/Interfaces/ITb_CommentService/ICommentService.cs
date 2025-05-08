
using MangaBackend_Application.Common.ResponseType;
using MangaBackend_Application.DTOS.Tb_CommentDto;

namespace MangaBackend.Domain.Interfaces.ITb_CommentService
{
    public interface ICommentService
    {
        ResponseModel AddComment(CommentDto dto);
        ResponseModel GetComments(string mangaSlug, string chapterSlug);
    }
}

using ArcadeGameBackend.Application.MiddleWare;
using MangaBackend.Domain.Interfaces.ITb_CommentService;
using MangaBackend_Application.Common.ResponseType;
using MangaBackend_Application.DTOS.Tb_CommentDto;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MangaBackend_Infrastructure.Services.Tb_CommentService
{
    public class CommentService : ICommentService
    {
        private readonly IMongoCollection<CommentDto> _comments;

        public CommentService(IConfiguration config)
        {
            var connectionString = config.GetSection("MongoDbSettings:ConnectionString").Value;
            var databaseName = config.GetSection("MongoDbSettings:DatabaseName").Value;

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _comments = database.GetCollection<CommentDto>("Tb_Comment");
        }

        public ResponseModel AddComment(CommentDto dto)
        {
            try
            {
                var comment = new CommentDto
                {
                    MangaSlug = dto.MangaSlug,
                    ChapterSlug = dto.ChapterSlug,
                    Username = dto.Username,
                    Content = dto.Content
                };

                _comments.InsertOne(comment);

                return ResponseData.SaveResponse("Comment added successfully");
            }
            catch (Exception ex)
            {
                return ResponseData.ErrorResponse("Error adding comment", ex.Message);
            }
        }

        public ResponseModel GetComments(string mangaSlug, string chapterSlug)
        {
            try
            {
                var comments = _comments
                    .Find(c => c.MangaSlug == mangaSlug && c.ChapterSlug == chapterSlug)
                    .SortByDescending(c => c.CreatedAt)
                    .ToList();

                return ResponseData.FoundSuccessResponse(comments);
            }
            catch (Exception ex)
            {
                return ResponseData.ErrorResponse("Error fetching comments", ex.Message);
            }
        }
    }

}

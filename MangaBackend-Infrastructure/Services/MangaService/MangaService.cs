using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcadeGameBackend.Application.MiddleWare;
using MangaBackend.Domain.Entities;
using MangaBackend.Domain.Interfaces.IMangaService;
using MangaBackend_Application.Common.ResponseType;
using MangaBackend_Application.DTOS.MangaDtos;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MangaBackend_Infrastructure.Services.MangaService
{
    public class MangaService : IMangaService
    {
        private readonly IMongoCollection<Tb_Manga> _manga;

        public MangaService(IConfiguration config)
        {
            var connectionString = config.GetSection("MongoDbSettings:ConnectionString").Value;
            var databaseName = config.GetSection("MongoDbSettings:DatabaseName").Value;

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _manga = database.GetCollection<Tb_Manga>("Tb_Manga");
        }

        public ResponseModel GetAllMangas()
        {
            try
            {
                var mangas = _manga.Find(_ => true).ToList();
                return ResponseData.FoundSuccessResponse(mangas);
            }
            catch (Exception ex)
            {
                return ResponseData.ErrorResponse("Error fetching mangas", ex.Message);
            }
        }

        public ResponseModel GetMangaBySlug(string slug)
        {
            try
            {
                var manga = _manga.Find(m => m.Slug == slug).FirstOrDefault();
                if (manga == null)
                    return ResponseData.NotSuccessResponse("Manga not found");

                return ResponseData.FoundSuccessResponse(manga);
            }
            catch (Exception ex)
            {
                return ResponseData.ErrorResponse("Error fetching manga", ex.Message);
            }
        }

        public ResponseModel GetFeaturedMangas()
        {
            try
            {
                var featured = _manga.Find(_ => true).Limit(5).ToList();
                return ResponseData.FoundSuccessResponse(featured);
            }
            catch (Exception ex)
            {
                return ResponseData.ErrorResponse("Error fetching featured mangas", ex.Message);
            }
        }

        public ResponseModel AddManga(MangaDto dto)
        {
            try
            {
                var manga = new Tb_Manga
                {
                    Title = dto.Title,
                    Slug = dto.Slug,
                    Thumbnail = dto.Thumbnail,
                    Status = dto.Status,
                    Genres = dto.Genres,
                    Author = dto.Author,
                    Description = dto.Description,
                    Rating = dto.Rating,
                    Chapters = dto.Chapters.Select(c => new Tb_MangaChapter
                    {
                        Id = c.Id,
                        Slug = c.Slug,
                        Title = c.Title,
                        Number = c.Number
                    }).ToList()
                };

                _manga.InsertOne(manga);

                return ResponseData.SaveResponse("Manga added successfully");
            }
            catch (Exception ex)
            {
                return ResponseData.ErrorResponse("Error adding manga", ex.Message);
            }
        }
    }
}


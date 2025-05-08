
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace MangaBackend_Application.DTOS.Tb_UserDtos
{
    public class UserDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; } = "User";

        public string? RecoveryCode1 { get; set; }
        public string? RecoveryCode2 { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}

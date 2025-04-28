using MangaBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestMongoController : ControllerBase
{
    private readonly MongoConnectionTest _mongoTest;

    public TestMongoController(MongoConnectionTest mongoTest)
    {
        _mongoTest = mongoTest;
    }

    [HttpGet("test")]
    public IActionResult TestMongo()
    {
        bool isConnected = _mongoTest.TestConnection();
        return Ok(new { connected = isConnected });
    }
}

public static class UserQueries
{
    public const string GetAll = "SELECT * FROM Tb_Users";
    public const string GetById = "SELECT * FROM Tb_Users WHERE Id = @Id";
    public const string Insert = @"
        INSERT INTO Tb_Users (Username, Email, Password, Role, CreatedAt)
        VALUES (@Username, @Email, @Password, @Role, @CreatedAt);
        SELECT CAST(SCOPE_IDENTITY() as int);";

    public const string Update = @"
        UPDATE Tb_Users SET
            Username = @Username,
            Email = @Email,
            Password = @Password,
            Role = @Role,
            CreatedAt = @CreatedAt
        WHERE Id = @Id";

    public const string Delete = "DELETE FROM Tb_Users WHERE Id = @Id";
}

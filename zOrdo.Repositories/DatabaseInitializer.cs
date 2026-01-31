namespace zOrdo.Repositories;

public class DatabaseInitializer(ISharedDatabaseUtils utils)
{
    public void Initialize()
    {
        using var connection = utils.CreateConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = """
                              -- Users Table
                              CREATE TABLE IF NOT EXISTS Users (
                                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                  FirstName TEXT NOT NULL,
                                  MiddleName TEXT,
                                  LastName TEXT NOT NULL,
                                  Email TEXT NOT NULL UNIQUE,
                                  PasswordHash TEXT,
                                  InsertedOnUtc TEXT NOT NULL,
                                  UpdatedOnUtc TEXT,
                                  DeletedOnUtc TEXT,
                                  DeletedBy TEXT
                              );
                              
                              -- Tasks Table
                              CREATE TABLE IF NOT EXISTS Tasks (
                                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                  Title TEXT NOT NULL,
                                  Description TEXT NOT NULL,
                                  Priority INTEGER NOT NULL,
                                  DueDateUtc TEXT NOT NULL,
                                  CompletedOnUtc TEXT,
                                  Status INTEGER NOT NULL,
                                  InsertedOnUtc TEXT NOT NULL,
                                  UpdatedOnUtc TEXT,
                                  DeletedOnUtc TEXT,
                                  DeletedBy TEXT
                              );
                              
                              -- Indexes for better query performance
                              CREATE INDEX IF NOT EXISTS idx_users_email ON Users(Email);
                              CREATE INDEX IF NOT EXISTS idx_tasks_status ON Tasks(Status);
                              CREATE INDEX IF NOT EXISTS idx_tasks_priority ON Tasks(Priority);
                              CREATE INDEX IF NOT EXISTS idx_tasks_duedate ON Tasks(DueDateUtc);
                              """;
        command.ExecuteNonQuery();
    }
}
using Dapper;
using zOrdo.Models;
using zOrdo.Models.Models;
using zOrdo.Models.Requests;

namespace zOrdo.Repositories.TodoItemRepository;

public class TodoItemRepository(ISharedDatabaseUtils utils) : ITodoItemRepository
{
    public async Task<TodoItem?> CreateTodoItemAsync(int userId, TodoItem todoItemRequest)
    {
        using var connection = utils.CreateConnection();
        const string sql = """
                           INSERT INTO TODO_ITEMS (
                                                  USER_ID,
                                                  TITLE,
                                                  DESCRIPTION,
                                                  PRIORITY,
                                                  STATUS,
                                                  DUE_DATE_UTC,
                                                  INSERTED_ON_UTC
                           ) VALUES (
                                   @user_id, 
                                   @title, 
                                   @description,
                                   @priority,
                                   @status,
                                   @due_date_utc,
                                   @inserted_on_utc
                           ) RETURNING ID
                           """;

        var insertedOnUtc = DateTime.UtcNow;
        var insertedId = await connection.ExecuteScalarAsync<int>(sql, new
        {
            user_id = todoItemRequest.UserId,
            title = todoItemRequest.Title,
            description = todoItemRequest.Description,
            priority = todoItemRequest.Priority.ToString(),
            status = todoItemRequest.Status.ToString(),
            due_date_utc = todoItemRequest.DueDateUtc,
            inserted_on_utc = insertedOnUtc
        });

        todoItemRequest.Id = insertedId;
        todoItemRequest.InsertedOnUtc = insertedOnUtc;
        return insertedId > 0 ? todoItemRequest : null;
    }

    public async Task<Paginated<TodoItem>> GetTodoItemsAsync(int userId, int pageNumber, int pageSize)
    {
        using var connection = utils.CreateConnection();
        
        const string sql = """
                           SELECT 
                               ID               AS Id,
                               USER_ID          AS UserId,
                               TITLE            AS Title,
                               DESCRIPTION      AS Description,
                               PRIORITY         AS Priority,
                               INSERTED_ON_UTC  AS InsertedOnUtc,
                               DUE_DATE_UTC     AS DueDateUtc,
                               STATUS           AS Status
                           FROM TODO_ITEMS
                           WHERE DELETED_ON_UTC IS NULL
                                AND USER_ID = @user_id
                           ORDER BY INSERTED_ON_UTC DESC
                           LIMIT @page_size OFFSET @offset
                           """;
        
        var result = 
            await connection.QueryAsync<TodoItem>(sql, new { page_size = pageSize, offset = (pageNumber - 1) * pageSize });
        
        return new Paginated<TodoItem>
        {
            Items = result.ToList(),
            // TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public Task<TodoItem> GetTTodoItemAsync(string userEmail, int taskId)
    {
        throw new NotImplementedException();
    }

    public Task<TodoItem> UpdateTodoItemAsync(string userEmail, TodoItemRequest todoItemRequest, int taskId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTodoItemAsync(string userEmail, int taskId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CompleteTodoItemAsync(string userEmail, int taskId)
    {
        throw new NotImplementedException();
    }
}
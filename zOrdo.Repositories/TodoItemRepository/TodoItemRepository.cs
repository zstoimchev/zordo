using Dapper;
using zOrdo.Models;
using zOrdo.Models.Enums;
using zOrdo.Models.Models;

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
            await connection.QueryAsync<TodoItem>(sql, new
            {
                user_id = userId,
                page_size = pageSize,
                offset = (pageNumber - 1) * pageSize
            });

        return new Paginated<TodoItem>
        {
            Items = result.ToList(),
            // TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<TodoItem?> GetTodoItemAsync(int userId, int taskId)
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
                                AND ID = @id
                           """;

        var result = await connection.QueryFirstOrDefaultAsync<TodoItem>(sql, new { user_id = userId, id = taskId });
        return result;
    }

    public async Task<TodoItem?> UpdateTodoItemAsync(int userId, TodoItem item)
    {
        using var connection = utils.CreateConnection();

        const string sql = """
                           UPDATE TODO_ITEMS
                           SET
                               TITLE = @title,
                               DESCRIPTION = @description,
                               PRIORITY = @priority,
                               STATUS = @status,
                               DUE_DATE_UTC = @due_date_utc
                           WHERE USER_ID = @user_id
                                 AND ID = @task_id
                                 AND DELETED_ON_UTC IS NULL
                           RETURNING 
                               ID               AS Id,
                               USER_ID          AS UserId,
                               TITLE            AS Title,
                               DESCRIPTION      AS Description,
                               PRIORITY         AS Priority,
                               INSERTED_ON_UTC  AS InsertedOnUtc,
                               DUE_DATE_UTC     AS DueDateUtc,
                               STATUS           AS Status
                           """;

        return await connection.QueryFirstOrDefaultAsync<TodoItem>(sql, new
        {
            user_id = userId,
            task_id = item.Id,
            title = item.Title,
            description = item.Description,
            priority = item.Priority.ToString(),
            status = item.Status.ToString(),
            due_date_utc = item.DueDateUtc
        });
    }

    public async Task<bool> DeleteTodoItemAsync(int userId, int taskId)
    {
        using var connection = utils.CreateConnection();

        const string sql = """
                           UPDATE TODO_ITEMS
                           SET DELETED_ON_UTC = @deleted_on_utc
                           WHERE USER_ID = @user_id
                                 AND ID = @task_id
                                 AND DELETED_ON_UTC IS NULL
                           """;

        var affectedRows = await connection.ExecuteAsync(sql, new
        {
            user_id = userId,
            task_id = taskId,
            deleted_on_utc = DateTime.UtcNow
        });

        return affectedRows > 0;
    }

    public async Task<bool> CompleteTodoItemAsync(int userId, int taskId)
    {
        using var connection = utils.CreateConnection();

        const string sql = """
                           UPDATE TODO_ITEMS
                           SET STATUS = @status
                           WHERE USER_ID = @user_id
                                 AND ID = @task_id
                                 AND DELETED_ON_UTC IS NULL
                           """;

        var affectedRows = await connection.ExecuteAsync(sql, new
        {
            user_id = userId,
            task_id = taskId,
            status = nameof(Status.Finished)
        });

        return affectedRows > 0;
    }

    public async Task<List<TodoItem>> GetIncompleteTasksAsync(int userId)
    {
        using var connection = utils.CreateConnection();
        
        var sql = """
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
                  FROM TODO_ITEMS
                  WHERE USER_ID = @user_id
                    AND STATUS != @status
                  """;

        var result = await connection.QueryAsync<TodoItem>(
            sql,
            new
            {
                user_id = userId,
                status = nameof(Status.Finished)
            });

        return result.ToList();
    }
}
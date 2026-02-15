# 🧠 zOrdo

**zOrdo** is a smart task management backend designed to help users not only store their TODO items — but eventually understand *when* and *in what order* to do them.

It’s not just another task list.
It’s a foundation for intelligent task prioritization.

## 🚀 Vision

Most TODO apps let you write tasks.

zOrdo aims to:

* Store structured tasks (priority, due date, status)
* Track task lifecycle
* Enable smart ordering & scheduling in the future
* Suggest what to work on next

Today: clean backend architecture.
Tomorrow: intelligent productivity engine.

## 🏗 Architecture

zOrdo follows a layered design:

* **Database API** – ASP.NET Core REST API
* **Repositories** – Dapper-based PostgreSQL access
* **HTTP Clients** – Clean internal service communication
* **Models** – Shared domain contracts

Key principles:

* Async everywhere
* Soft deletes
* Pagination support
* Enum-based priority & status
* Clean REST conventions

## 📦 Features (Current)

* ✅ Create TODO item
* ✅ Update TODO item
* ✅ Soft delete
* ✅ Mark as completed
* ✅ Pagination
* ✅ Priority levels
* ✅ Due date tracking

## 🛠 Example API Usage

### Create Task

```
POST /api/todoItem/{userId}
```

### Get Tasks

```
GET /api/todoItem/{userId}?pageNumber=1&pageSize=10
```

### Complete Task

```
PUT /api/todoItem/{userId}/{todoItemId}/complete
```

## Postman Collection
[zOrdo.postman_collection.json](https://zstoimchev-81b4561d-543924.postman.co/workspace/Zhivko-Stoimchev's-Workspace~466c3165-9e94-4216-80e2-38628664c118/collection/51057785-df095b38-a113-43fb-b042-c463f3e83f6d?action=share&creator=51057785&active-environment=51057785-ec25bb3f-702f-4165-b710-85daeecd1972)

## 🔮 What’s Coming

* Smart task ordering
* AI-based scheduling suggestions
* Filtering & sorting
* User authentication (JWT)
* Analytics & productivity insights

## 💡 Why “zOrdo”?

“Ordo” means *order*. zOrdo is about bringing order to chaos — one task at a time.

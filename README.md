## Implementation Web API(task tracker)
---
### Toolset
- .Net Core
- PostgreSQL
- Swagger for automated API documentation
- EntityFramework
___
> Three-level project architecture (data access level, logic level, representation)

---
### Usage
- Update `appsettings.json` for appropriate database
    ```json
    "ConnectionStrings": {
        "ConnectionString": "Host=hostname;Database=dbname;Port=port;Username=username;Password=password"
    }
    ```
- Add migration
    ```
    dotnet ef --startup-project .\TaskTracker.PL\ -p .\TaskTracker.DAL\ migrations add Initial
    ```
- For updating database use this command:
    ``` 
    dotnet ef --startup-project .\TaskTracker.PL\ -p .\TaskTracker.DAL\ database update
    ```
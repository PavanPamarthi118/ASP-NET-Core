![image](https://github.com/user-attachments/assets/6bc149ce-ff07-443f-af8e-7e504652482f)
![image](https://github.com/user-attachments/assets/58327e9b-7768-47a5-b6ef-f6ea729f0c4c)
![image](https://github.com/user-attachments/assets/559d6bee-ff32-4bb3-8775-ad168d0d8696)
![image](https://github.com/user-attachments/assets/f60fa67b-eed4-4618-9d81-5f6e7b3df8d6)

✅ What is a Migration in EF Core / ASP.NET Core REST API?

Migration in Entity Framework Core (EF Core) is a way to keep your C# model classes (your domain/entities) in sync with the database schema.
It tracks changes you make to your model (like adding a property or a new class) and applies those changes to the database automatically through generated SQL scripts.

🧠 Think of it like this:
You’re working on a REST API with EF Core. You create a model:

![image](https://github.com/user-attachments/assets/458f3728-e9b2-4fd5-8b8a-1360536e1158)


Now you want to create a Games table in the database to match this class. Instead of writing SQL manually, you do this:

dotnet ef migrations add InitialCreate
dotnet ef database update

🛠️ How Migrations Work (Behind the Scenes)
dotnet ef migrations add <name>
EF Core looks at your model and generates a migration file with SQL operations (like CREATE TABLE, ALTER TABLE, etc.)

dotnet ef database update
Applies the migration to the database (creates or modifies tables, columns, indexes, etc.)

Migration File Example:

![image](https://github.com/user-attachments/assets/791b55d8-2e8a-44be-a4f1-380d6e2c608d)

🚀 Why Use Migrations in Your API Project?
1. No need to write raw SQL
2. Keeps database and code in sync
3. Makes team collaboration easier (migration files can be version-controlled)
4. Useful in CI/CD pipelines
5. You can roll back (revert) database changes easily

📦 Typical Commands You’ll Use:
Command	Purpose
dotnet ef migrations add InitialCreate	Generate a new migration
dotnet ef database update	Apply the latest migration to the DB
dotnet ef migrations remove	Remove the last migration
dotnet ef migrations list	List all applied migrations



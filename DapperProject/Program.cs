using System.Threading;
using System;
using Dapper;
using DapperProject.Models;
using Microsoft.Data.SqlClient;

namespace DapperProject
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=localhost, 1433;Database=balta;User ID=sa; Password=Dudu159123!";



            using (var connection = new SqlConnection(connectionString))
            {
                //ListCategories(connection);
                //CreateCategory(connection);
                //UpdateCategory(connection);
                //DeleteCategory(connection);
                //CreateManyCategory(connection);
                //ExecuteProcedure(connection);
                // ExecuteScalar(connection);
                // ExecuteReadProcedure(connection);
                // ReadView(connection);

            }
        }
        static void ListCategories(SqlConnection connection)
        {

            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }

        static void CreateCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Summary = "AWS Cloud";
            category.Url = "Amazon";
            category.Description = "Categoria destinada a serviço do AWS";
            category.Order = 8;
            category.Featured = false;

            var insertSQL = @"INSERT INTO [Category] 
                        VALUES(
                            NEWID(), 
                            @Title, 
                            @Url, 
                            @Summary, 
                            @Order, 
                            @Description, 
                            @Featured)";
            var rows = connection.Execute(insertSQL, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });
            Console.WriteLine($"{rows}Linhas Inseridas");
        }

        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = @"UPDATE [Category]
                                SET [Title] = @title
                                WhHERE [Id] = @id";
            var rows = connection.Execute(updateQuery, new
            {
                id = new Guid(""),
                Title = "Frontend 2021"
            });
            Console.WriteLine($"{rows} Registros Atualizados");

        }

        static void DeleteCategory(SqlConnection connection)
        {
            var updateQuery = @"DELETE [Category]
                                WhHERE [Id] = @id";
            var rows = connection.Execute(updateQuery, new
            {
                id = new Guid(""),
            });
            Console.WriteLine($"{rows} Registros Deletados");
        }

        static void CreateManyCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Summary = "AWS Cloud";
            category.Url = "Amazon";
            category.Description = "Categoria destinada a serviço do AWS";
            category.Order = 8;
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria Nova";
            category2.Summary = "Categoria Nova";
            category2.Url = "Categoria Nova";
            category2.Description = "Categoria nova";
            category2.Order = 9;
            category2.Featured = true;


            var insertSQL = @"INSERT INTO [Category] 
                        VALUES(
                            NEWID(), 
                            @Title, 
                            @Url, 
                            @Summary, 
                            @Order, 
                            @Description, 
                            @Featured)";
            var rows = connection.Execute(insertSQL, new[]{
            new {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            },
            new {
                category2.Id,
                category2.Title,
                category2.Url,
                category2.Summary,
                category2.Order,
                category2.Description,
                category2.Featured
            }});
            Console.WriteLine($"{rows}Linhas Inseridas");
        }

        static void ExecuteProcedure(SqlConnection connection)
        {
            var procedure = "spDeleteStudent";

            var parameters = new { StudentId = "" };
            var rows = connection.Execute(
                procedure,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure);
            Console.WriteLine($"{rows} Linhas afetadas");
        }
        static void ExecuteReadProcedure(SqlConnection connection)
        {
            var procedure = "spGetCoursesByCategory";

            var parameters = new { CategoryId = "" };
            var courses = connection.Query(
                procedure,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure);
            foreach (var course in courses)
            {
                Console.WriteLine(course.Id);

            }
        }
        static void ExecuteScalar(SqlConnection connection)
        {
            var category = new Category();
            category.Title = "Amazon AWS";
            category.Summary = "AWS Cloud";
            category.Url = "Amazon";
            category.Description = "Categoria destinada a serviço do AWS";
            category.Order = 8;
            category.Featured = false;

            var insertSQL = @"INSERT INTO [Category] 
                        OUTPUT inserted.[Id]
                        VALUES(
                            NEWID(), 
                            @Title, 
                            @Url, 
                            @Summary, 
                            @Order, 
                            @Description, 
                            @Featured) ";
            var id = connection.ExecuteScalar<Guid>(insertSQL, new
            {
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });
            Console.WriteLine($"O id inserido foi: {id}");
        }

        static void ReadView(SqlConnection connection)
        {
            var view = "SELECT * FROM [vmCourses]";
            var courses = connection.Query(view);
            foreach (var item in courses)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }

    }
}

using Bogus;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Constants;
using ProjectManagement.DbContext;
using ProjectManagement.Entities;
using ProjectManagement.Enums;

namespace ProjectManagement.Seeders
{
    public class DynamicProjectSeeder
    {
        public static async System.Threading.Tasks.Task SeedAsync(WebApplication app, int numberOfProjects)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ProjectManagmentDbContext>();
            await AddDummyProjectsAndTasks(dbContext, numberOfProjects);
        }

        private static async System.Threading.Tasks.Task AddDummyProjectsAndTasks(ProjectManagmentDbContext dbContext, int numberOfProjects)
        {

            var managerUserId = 1;


            var faker = new Faker();

            var projects = new List<Project>();

            for (int i = 0; i < numberOfProjects; i++)
            {
                var tasks = GenerateTasks(faker, faker.Random.Int(1, 5));

                var project = new Project
                {
                    Name = faker.Company.CompanyName() + $" {i + 1}",
                    Description = faker.Lorem.Random.Words(10),
                    StartDate = faker.Date.Past(1),
                    EndDate = faker.Date.Future(1),
                    Budget = faker.Random.Decimal(5000, 50000),
                    OwnerId = managerUserId,
                    Status = faker.PickRandom<ProjectStatusEnum>(),
                    Tasks = tasks
                };

                projects.Add(project);
            }
            await dbContext.Projects.AddRangeAsync(projects);
            await dbContext.SaveChangesAsync();
        }

        private static List<Entities.Task> GenerateTasks(Faker faker, int numberOfTasks)
        {
            var tasks = new List<Entities.Task>();

            for (int i = 0; i < numberOfTasks; i++)
            {
                var comments = GenerateComments(faker, faker.Random.Int(2, 3), faker.Random.Int(1, 3));

                var task = new Entities.Task
                {
                    Name = faker.Commerce.ProductName(),
                    Description = faker.Lorem.Sentence(),
                    StartDate = faker.Date.Past(),
                    EndDate = faker.Date.Future(faker.Random.Int(-3, 3)),
                    Priority = faker.PickRandom<PriorityEnum>(),
                    Status = faker.PickRandom<TaskStatusEnum>(),
                    AssignedToId = faker.Random.Int(2, 3),
                    Comments = comments
                };

                tasks.Add(task);
            }

            return tasks;
        }

        private static List<Comment> GenerateComments(Faker faker, int userId, int numberOfComments)
        {
            var comments = new List<Comment>();

            for (int i = 0; i < numberOfComments; i++)
            {
                var comment = new Comment
                {
                    Content = faker.Lorem.Paragraph(),
                    CreatedById = userId,
                    CreationDate = faker.Date.Recent()
                };
                comments.Add(comment);
            }
            return comments;
        }
    }
}

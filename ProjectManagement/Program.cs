using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.DbContext;
using ProjectManagement.Entities;
using ProjectManagement.Managers;
using ProjectManagement.Middlewares;
using ProjectManagement.Seeders;
using ProjectManagement.Services.CommentService;
using ProjectManagement.Services.ProjectService;
using ProjectManagement.Services.TaskService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectManagmentDbContext>(options => options.UseInMemoryDatabase("ProjectManagementDb"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddDefaultTokenProviders()
                .AddApiEndpoints()
                .AddEntityFrameworkStores<ProjectManagmentDbContext>();

InjectService();
    builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);

var app = builder.Build();

await Seed();

app.UseMiddleware<GlobalExceptionHandler>();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/api/Identity").MapIdentityApi<ApplicationUser>();

app.MapControllers();

app.Run();



void InjectService()
{
    builder.Services.AddScoped<IProjectService, ProjectService>();
    builder.Services.AddScoped<ICommentService, CommentService>();
    builder.Services.AddScoped<ITaskService, TaskService>();
    builder.Services.AddScoped<UserManager>();
    builder.Services.AddScoped<UserManager<ApplicationUser>>();
  
}

async System.Threading.Tasks.Task Seed()
{
  

    await IdentitySeeder.AddRolesAsync(app);
    await IdentitySeeder.AddManagerTestAccountAsync(app);
    await IdentitySeeder.AddEmployeeTestAccountsAsync(app);

    if(int.TryParse(builder.Configuration["NumberOfDummyProjects"],out int num) && num > 0)
        await DynamicProjectSeeder.SeedAsync(app,num);

}
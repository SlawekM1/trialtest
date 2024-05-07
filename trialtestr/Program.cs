using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers
builder.Services.AddControllers();

// Swagger setup for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();

// Define your ApplicationDbContext and other model classes here or in separate files
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSet properties for your entities
    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<TaskType> TaskTypes { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
}

// Assuming model classes are already defined, here's a quick setup for them
public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Deadline { get; set; }
}

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public int ProjectId { get; set; }
    public int TaskTypeId { get; set; }
    public int AssignedToId { get; set; }
    public int CreatorId { get; set; }
    public object Project { get; set; }
    public object TaskType { get; set; }
}

public class TaskType
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class TeamMember
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

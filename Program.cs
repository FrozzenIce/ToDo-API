using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.DTO;
using ToDo.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to generate Swagger JSON specs
builder.Services.AddEndpointsApiExplorer(); // Required for Minimal APIs
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddDbContext<AppDbContext>(options =>

{
    options.UseInMemoryDatabase("TasksDatabase");
});

var app = builder.Build();

// Enable middleware to serve generated Swagger JSON and UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Serves the interactive UI at /swagger
}

app.MapGet("/", () => "Hello World!");

app.MapGet("/tasks", async (ITaskService service) =>
{
    var tasks = await service.GetAllTasks();
    return Results.Ok(tasks);
});

app.MapGet("/tasks/{id}", async (int id, ITaskService service) =>
{
    var response = await service.GetTaskById(id);

    return response is null
        ? Results.NotFound()
        : Results.Ok(response);
});

app.MapPost("/tasks", async (TaskRequest request, ITaskService service) =>
{
    var response = await service.AddTask(request);

    return Results.Created($"/tasks/{response.Id}", response);
});

app.MapPut("/tasks/{id}", async (int id, TaskRequest request, ITaskService service) =>
{
    var task = await service.UpdateTask(id, request);

    return task is null
        ? Results.NotFound()
        : Results.Ok(task);
});

app.Run();

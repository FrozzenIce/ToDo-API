namespace ToDo.Service;

using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;
public class TaskService : ITaskService
{
    public readonly AppDbContext db;


    public TaskService(AppDbContext db)
    {
        this.db = db;
    }

    public async Task<Tasks?> UpdateTask(int id, TaskRequest request)
    {
        var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
            return null;

        task.Title = request.Title;
        task.IsCompleted = request.IsCompleted;

        await db.SaveChangesAsync();

        return task;
    }

    public async Task<Tasks> AddTask(TaskRequest request)
    {
        var task = new Tasks
        {
            Title = request.Title,
            IsCompleted = request.IsCompleted
        };

        await db.Tasks.AddAsync(task);
        await db.SaveChangesAsync();

        return task;
    }

    public async Task<Tasks?> GetTaskById(int id)
    {
        return await db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Tasks>> GetAllTasks()
    {
        return await db.Tasks.ToListAsync();
    }
}
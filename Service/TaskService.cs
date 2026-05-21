using ToDo.DTO;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Service;

public class TaskService : ITaskService
{
    public readonly AppDbContext db;


    public TaskService(AppDbContext db)
    {
        this.db = db;
    }

    public async Task<List<TaskResponse>> GetAllTasks()
    {
        return await db.Tasks
        .Select(task => new TaskResponse()
        {
            Id = task.Id,
            Title = task.Title,
            IsCompleted = task.IsCompleted
        }).ToListAsync();
    }

    public async Task<TaskResponse?> GetTaskById(int id)
    {
        Tasks? task = await db.Tasks
        .FirstOrDefaultAsync(t => t.Id == id);

        if(task == null)
        {
            return null;
        }
        return new TaskResponse()
        {
            Id = task.Id,
            Title = task.Title,
            IsCompleted = task.IsCompleted
        };
    }


    public async Task<TaskResponse> AddTask(TaskRequest request)
    {
        var task = new Tasks
        {
            Title = request.Title,
            IsCompleted = request.IsCompleted
        };

        await db.Tasks.AddAsync(task);
        await db.SaveChangesAsync();

        return new TaskResponse()
        {
            Id = task.Id,
            Title = task.Title,
            IsCompleted = task.IsCompleted
        };
    }

    public async Task<TaskResponse?> UpdateTask(int id, TaskRequest request)
    {
        var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
            return null;

        task.Title = request.Title;
        task.IsCompleted = request.IsCompleted;

        await db.SaveChangesAsync();

        return new TaskResponse()
        {
            Id = task.Id,
            Title = task.Title,
            IsCompleted = task.IsCompleted  
        };
    }

    public async Task<bool> DeleteTask(int id)
    {
        Tasks? task = await db.Tasks
        .FirstOrDefaultAsync(t => t.Id == id);
        if(task is null)
        {
            return false;
        }
        
        db.Tasks.Remove(task);

        await db.SaveChangesAsync();

        return true;
    }

    public async Task<int> CountTasks()
    {
        return await db.Tasks.CountAsync();
    }
}
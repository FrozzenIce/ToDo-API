using ToDo.Models;

namespace ToDo.Service;

public interface ITaskService
{
    Task<List<Tasks>> GetAllTasks();
    Task<Tasks?> GetTaskById(int id);
    Task<Tasks> AddTask(TaskRequest request);
    Task<Tasks?> UpdateTask(int id, TaskRequest request);
}
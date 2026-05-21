using ToDo.Models;
using ToDo.DTO;

namespace ToDo.Service;

public interface ITaskService
{
    Task<List<TaskResponse>> GetAllTasks();
    Task<TaskResponse?> GetTaskById(int id);
    Task<TaskResponse> AddTask(TaskRequest request);
    Task<TaskResponse?> UpdateTask(int id, TaskRequest request);
   Task<bool> DeleteTask(int id);
   Task<int> CountTasks();
}
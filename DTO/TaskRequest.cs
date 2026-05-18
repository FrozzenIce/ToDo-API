using System.ComponentModel;

namespace ToDo.DTO;

public class TaskRequest
{
    public string Title { get; set; } = string.Empty;

    [DefaultValue(false)] 
    public bool IsCompleted { get; set; } = false;
}

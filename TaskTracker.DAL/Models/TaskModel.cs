using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskTracker.DAL.Models;

public class TaskModel
{   
    [Key]
    public int Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public int Priority { get; set; }
    
    public TaskStatus Status { get; set; }
    
    public int? ProjectId { get; set; }
        
    [JsonIgnore]
    public ProjectModel? Project { get; set; }
    
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
    
}
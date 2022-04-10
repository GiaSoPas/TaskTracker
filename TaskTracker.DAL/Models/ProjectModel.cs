using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskTracker.DAL.Models;

public class ProjectModel
{   
    [Key]
    public int Id { get; set; }
    
    public string? Name { get; set; }
    
    public DateTime? StartDate { get; set; }
    
    public DateTime? CompletionDate { get; set; }
    
    public int Priority { get; set; }
    
    public ProjectStatus Status { get; set; }
    [JsonIgnore]
    public IList<TaskModel>? Tasks { get; set; }

    public enum ProjectStatus
    {
        NotStarted,
        Active,
        Completed
    }
}
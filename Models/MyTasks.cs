using System;
using System.ComponentModel.DataAnnotations;
using TaskManager.Models.Enum;
using TaskManager.Models.Validation;

namespace TaskManager.Models
{
    // Represents a single task in the Task Manager application
    public class MyTasks
    {
        // Unique identifier for the task (Primary Key)
        public int Id { get; set; }

        // Title of the task (required, max 100 characters)
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        // Optional description of the task (max 500 characters)
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; } // Nullable since it's optional

        // Indicates if the task is completed (defaults to false)
        public bool IsCompleted { get; set; } = false;

        // Date and time the task was created (defaults to now)
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Due date for the task (required, must be today or later)
        [Required(ErrorMessage = "Due date is required.")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(ValidateDueDate), nameof(ValidateDueDate.ValidateDate))]
        public DateTime DueDate { get; set; }

        // Priority level of the task (High, Medium, Low)
        [Required(ErrorMessage = "Priority is required.")]
        public PriorityLevel Priority { get; set; }

    
        
    }
}
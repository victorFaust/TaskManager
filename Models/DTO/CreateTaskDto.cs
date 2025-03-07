using System;
using System.ComponentModel.DataAnnotations;
using TaskManager.Models;
using TaskManager.Models.Enum;
using TaskManager.Models.Validation;

namespace TaskManager.Models.Dtos
{
    // DTO for creating a new task
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Due date is required.")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(ValidateDueDate), nameof(ValidateDueDate.ValidateDate))]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        public PriorityLevel Priority { get; set; }
    }

    // DTO for editing an existing task
    public class EditTaskDto
    {
        public int Id { get; set; } // Needed to identify the task to update

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; } // Allow toggling completion

        [Required(ErrorMessage = "Due date is required.")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(ValidateDueDate), nameof(ValidateDueDate.ValidateDate))]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        public PriorityLevel Priority { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_App.Domain.Entities;
public class Tag : BaseAuditableEntity
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();

    public Tag()
    {
        TodoItems = new List<TodoItem>();
    }
}

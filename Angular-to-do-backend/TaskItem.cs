public class TaskItem
{
    // Properties (attributes) of the TaskItem class
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    
    // Constructors (optional)
    public TaskItem()
    {
        // Default constructor
    }

    public TaskItem(int id, string title, string description, bool isCompleted)
    {
        // Constructor with parameters
        Id = id;
        Title = title;
        Description = description;
        IsCompleted = isCompleted;
    }

    // Methods (optional)
    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }

    // Additional properties and methods as needed...
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly string _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "tasks.json");

    [HttpGet]
    public IActionResult GetTasks()
    {
        var tasks = ReadTasksFromFile();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public IActionResult GetTaskById(int id)
    {
        var tasks = ReadTasksFromFile();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
            return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public IActionResult CreateTask([FromBody] TaskItem task)
    {
        var tasks = ReadTasksFromFile();
        task.Id = tasks.Count + 1; // Simulate auto-increment
        tasks.Add(task);
        WriteTasksToFile(tasks);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, [FromBody] TaskItem updatedTask)
    {
        var tasks = ReadTasksFromFile();
        var existingTask = tasks.FirstOrDefault(t => t.Id == id);
        if (existingTask == null)
            return NotFound();

        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
        existingTask.IsCompleted = updatedTask.IsCompleted;

        WriteTasksToFile(tasks);
        return Ok(existingTask);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var tasks = ReadTasksFromFile();
        var taskToRemove = tasks.FirstOrDefault(t => t.Id == id);
        if (taskToRemove == null)
            return NotFound();

        tasks.Remove(taskToRemove);
        WriteTasksToFile(tasks);
        return NoContent();
    }

    private List<TaskItem> ReadTasksFromFile()
    {
        using (StreamReader reader = new StreamReader(_jsonFilePath))
        {
            string json = reader.ReadToEnd();
            return JsonSerializer.Deserialize<List<TaskItem>>(json);
        }
    }

    private void WriteTasksToFile(List<TaskItem> tasks)
    {
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        using (StreamWriter writer = new StreamWriter(_jsonFilePath, false))
        {
            writer.Write(json);
        }
    }
}

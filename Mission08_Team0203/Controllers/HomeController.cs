using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0203.Models;

namespace Mission08_Team0203.Controllers;

public class HomeController : Controller
{
    private readonly TaskContext _context;

    public HomeController(TaskContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Quadrants()
    {
        var tasks = _context.Tasks
            .AsNoTracking()
            .Where(t => !t.Completed)
            .OrderBy(t => t.Quadrant)
            .ThenBy(t => t.DueDate)
            .ToList();

        return View(tasks);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View("Add", new Mission08_Team0203.Models.TaskItem());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(TaskItem newTask)
    {
        if (!ModelState.IsValid)
        {
            return View(newTask);
        }

        _context.Tasks.Add(newTask);
        _context.SaveChanges();

        return RedirectToAction(nameof(Quadrants));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return NotFound();
        }

        return View("Add", task);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Mission08_Team0203.Models.TaskItem updatedTask)
    {
        if (!ModelState.IsValid)
        {
            return View(updatedTask);
        }

        var existingTask = _context.Tasks.FirstOrDefault(t => t.Id == updatedTask.Id);

        if (existingTask == null)
        {
            return NotFound();
        }

        existingTask.Title = updatedTask.Title;
        existingTask.DueDate = updatedTask.DueDate;
        existingTask.Quadrant = updatedTask.Quadrant;
        existingTask.Category = updatedTask.Category;
        existingTask.Completed = updatedTask.Completed;

        _context.SaveChanges();

        return RedirectToAction(nameof(Quadrants));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        return RedirectToAction(nameof(Quadrants));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult MarkComplete(int id)
    {
        var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

        if (task != null)
        {
            task.Completed = true;
            _context.SaveChanges();
        }

        return RedirectToAction(nameof(Quadrants));
    }
}

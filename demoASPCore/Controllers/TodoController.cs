using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using demoASPCore.Models;


namespace demoASPCore.Controllers
{
    // Path
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context; //Aggiungo contesto db (InMemory in questo caso... vedi Startup.cs)

        public TodoController(TodoContext context)  // Costruttore dove inserisco dati mock
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null) return NotFound();

            return new ObjectResult(item);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QGeneratorASP.Models;

namespace QGeneratorASP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        private readonly GQ _context;
        public QuestController(GQ context)
        {
            _context = context;
            if (_context.Quest.Count() == 0)
            {

                _context.Quest.Add(new Quest
                {
                    Status = true,
                    Date = DateTime.Now,
                    Thematics = "no",
                    User = new User { UserName = "moderator", PasswordHash = "123123", AccessLevel = true },
                    Level_of_complexity = new Level_of_complexity { Name_level = "hard" }
                });
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Quest> GetAll()
        {
            return _context.Quest.Include(l => l.Level_of_complexity)
                .Include(u=>u.User)
                .Include(r=>r.QuestRiddle)
                      .ThenInclude(r=>r.Riddle).ThenInclude(a => a.Answer)
                      .ThenInclude(r => r.Riddle).ThenInclude(a => a.Level_of_complexity)
                      .ThenInclude(r => r.Riddle).ThenInclude(l => l.Type_of_question)
                      .ThenInclude(r => r.Riddle).ThenInclude(u => u.User);
        }
 
        public IEnumerable<Level_of_complexity> GetLevels()
        {
            return _context.Level_of_complexity;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuest([FromRoute] int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var q = await _context.Quest.Include(l => l.Level_of_complexity)
                .Include(u => u.User)
                .Include(r => r.QuestRiddle).ThenInclude(r => r.Riddle).FirstOrDefaultAsync(m => m.Id_quest == id);
            if (q == null) { return NotFound(); }
            return Ok(q);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Quest q)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            _context.Quest.Add(q);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetQuest", new { id = q.Id_quest }, q);
        }
        
        

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]  Quest q)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var item = _context.Quest.Find(id);
            if (item == null) { return NotFound(); }
           
            item.Number_of_questions = q.Number_of_questions;
            item.Date = q.Date;
            item.Thematics = q.Thematics;
            item.Level_of_complexity = _context.Level_of_complexity.Find(q.Id_level_Fk);
            item.User = _context.User.Find(q.Id_autor_Fk);//
            item.Status = item.User.AccessLevel;
            item.QuestRiddle = q.QuestRiddle;
            _context.Quest.Update(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var item = _context.Quest.Find(id);
            if (item == null) { return NotFound(); }
            _context.Quest.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
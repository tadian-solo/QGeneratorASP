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
    [Route("api/[controller]")]
    [ApiController]
    public class RiddleController : ControllerBase
    {
        private readonly GQ _context;
        public RiddleController(GQ context)
        {
            _context = context;
            if (_context.Riddle.Count() == 0)
            {
                
                if (_context.Quest.Count() == 0)
                {
                    Quest q = new Quest
                    {
                        Status = true,
                        Date = DateTime.Now,
                        Thematics = "no",
                        User = new User { Name = "moderator", Password = "123123" },
                        Level_of_complexity = new Level_of_complexity { Name_level = "hard" }

                    };
                    _context.Quest.Add(q);
                    _context.SaveChanges();
                }
                
                Riddle r1 = new Riddle
                {
                    Text = "something very great",
                    Description = "dfdfd",
                    Status = true,
                    Answer = new Answer { Object = "table" },
                    Level_of_complexity = new Level_of_complexity {  Name_level = "hard" },
                    Type_of_question = new Type_of_question {  Name = "212" },
                    User=new User { Name="admin", Password="123123"}
                };
                _context.Riddle.Add(r1);
                _context.SaveChanges();

                QuestRiddle q1 = new QuestRiddle
                {
                    Riddle=r1,
                    Quest=_context.Quest.Last()

                };
                _context.QuestRiddle.Add(q1);
                _context.SaveChanges();

            }
        }
        [HttpGet]
        public IEnumerable<Riddle> GetAll()
        {
            return _context.Riddle.Include(a=>a.Answer).Include(l=>l.Level_of_complexity).Include(l=>l.Type_of_question)
                .Include(u=>u.User)
                .Include(q=>q.QuestRiddle).ThenInclude(q=>q.Quest);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRiddle([FromRoute] int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var q = await _context.Riddle.Include(a => a.Answer).Include(l => l.Level_of_complexity).Include(l => l.Type_of_question)
                .Include(u => u.User)
                .Include(m => m.QuestRiddle).ThenInclude(m => m.Quest).FirstOrDefaultAsync(i=>i.Id_riddle==id);
            if (q == null) { return NotFound(); }
            return Ok(q);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Riddle q)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            _context.Riddle.Add(q);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRiddle", new { id = q.Id_riddle }, q);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]  Riddle q)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var item = _context.Riddle.Find(id);
            if (item == null) { return NotFound(); }
            item.Text = q.Text;
            item.Description = q.Description;
            item.Status = q.Status;
            item.Answer = q.Answer;
            item.Level_of_complexity = q.Level_of_complexity;
            item.Type_of_question = q.Type_of_question;
            item.User = q.User;
            item.QuestRiddle = q.QuestRiddle;
            _context.Riddle.Update(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var item = _context.Riddle.Find(id);
            if (item == null) { return NotFound(); }
            _context.Riddle.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
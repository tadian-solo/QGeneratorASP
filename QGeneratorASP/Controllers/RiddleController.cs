using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QGeneratorASP.Models;

namespace QGeneratorASP.Controllers
{
    [Route("api/[controller]/[action]")]
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
                      //  User = new User { UserName = "moderator", PasswordHash = "123123" },
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
                   // User=new User { Name="admin", Password="123123"}
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
        public IEnumerable<Level_of_complexity> GetLevels()
        {
            return _context.Level_of_complexity;
        }
        public IEnumerable<Type_of_question> GetTypes()
        {
            return _context.Type_of_question;
        }
        [HttpGet("{id}")]
        public List<Type_of_question> GetTypesForLevel([FromRoute] int id)
        {
            var ts = new List<Type_of_question>();
            var rs = _context.Riddle.Where(i => (i.Id_Level_FK == id));
            foreach (var p in rs)
            {
                if (!ts.Contains(p.Type_of_question)) ts.Add(_context.Type_of_question.Find(p.Id_Type_FK));
            }
            return ts;
        }
        [HttpGet]
        public List<Answer> GetAnswersForLevelAndType( int id,  int level)
        {
            
                List<Answer> answers = new List<Answer>();
                var rs = _context.Riddle.Where(i => (i.Id_Type_FK == id) && (i.Id_Level_FK == level));
                foreach (var p in rs)
                {
                    if (!answers.Contains(p.Answer)) answers.Add(_context.Answer.Find(p.Id_Answer_FK));
                }
                return answers;
           
        }
        [HttpGet]
        public async Task<IActionResult> GetRiddle(int id_level, int id_type, int id_answer)
        {

            var q= await _context.Riddle.Where(r => (r.Id_Level_FK == id_level) && (r.Id_Type_FK == id_type) && (r.Id_Answer_FK == id_answer)).FirstOrDefaultAsync();
            if (q == null) { return NotFound(); }
            return Ok(q); 
        }
        public IEnumerable<Answer> GetAnswers()
        {
            return _context.Answer;
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Riddle q)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            _context.Riddle.Add(q);
            await _context.SaveChangesAsync();
            Log.WriteLog("Riddle:Create", "Создана новая загадка");
            return CreatedAtAction("GetRiddle", new { id = q.Id_riddle }, q);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]  Riddle q)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var item = _context.Riddle.Find(id);
            if (item == null) { return NotFound(); }
            item.Text = q.Text;
            item.Description = q.Description;
            item.Answer = _context.Answer.Find(q.Id_Answer_FK);
            item.Level_of_complexity = _context.Level_of_complexity.Find(q.Id_Level_FK);
            item.Type_of_question = _context.Type_of_question.Find(q.Id_Type_FK);
            item.User = _context.User.Find(q.Id_Autor_FK);
            item.Status = item.User.AccessLevel;
            item.QuestRiddle = q.QuestRiddle;
            _context.Riddle.Update(item);
            Log.WriteLog("Riddle:Update", "Загадка №" + item.Id_riddle + "обовлена");
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var item = _context.Riddle.Find(id);
            if (item == null) { return NotFound(); }
            _context.Riddle.Remove(item);
            Log.WriteLog("Riddle:Delete", "Загадка №" + item.Id_riddle + "удалена");
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
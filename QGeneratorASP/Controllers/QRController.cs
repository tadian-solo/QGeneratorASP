﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QGeneratorASP.Models;

namespace QGeneratorASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRController : ControllerBase
    {
        private readonly GQ _context;
        public QRController(GQ context)
        {
            _context = context;
            
        }
        public class Ids
        {
            public int id_quest;
            public int id_riddle;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRQ([FromBody] Ids id/*int id_quest, int id_riddle*/)
        {
            if (_context.Riddle.Find(id.id_riddle)!=null&& _context.Quest.Find(id.id_quest)!=null)
            _context.QuestRiddle.Add(new QuestRiddle { Riddle = _context.Riddle.Find(id.id_riddle), Quest = _context.Quest.Find(id.id_quest) });
            Log.WriteLog("QR:CreateRQ", "Добавлена новая запись в таблицу QuestRiddle");
            await _context.SaveChangesAsync();
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Ids id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var item = _context.QuestRiddle.Find(id.id_quest, id.id_riddle);
            
            if (item == null) { Log.WriteLog("QR:DeleteRQ", "Удаление не удалось QuestRiddle");  return NotFound(); }
            _context.QuestRiddle.Remove(item);
            await _context.SaveChangesAsync();
            Log.WriteLog("QR:DeleteRQ", "Удаление QuestRiddle");
            return NoContent();
        }
    }
}
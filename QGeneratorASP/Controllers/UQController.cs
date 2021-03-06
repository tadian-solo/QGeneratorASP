﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QGeneratorASP.Models;

namespace QGeneratorASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UQController : ControllerBase
    {
        private readonly GQ _context;
        private readonly UserManager<User> _userManager;
        public UQController(GQ context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public class Ids//модель для передачи айди из тела запроса
        {
            public int id_quest;
            public int id_user;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUQ([FromBody] Ids id/*int id_quest, int id_riddle*/)//добавление квеста в Избранное
        {
            if (id.id_user == -1)// из клиента посылаем айди=-1, в методе заполняем его айди текущего пользователя 
            {
                User usr = await GetCurrentUserAsync();
                id.id_user = usr!=null?usr.Id:-1;
            }

            if (_context.User.Find(id.id_user) != null && _context.Quest.Find(id.id_quest) != null)// проверяем существуют ли такие пользователь и квест
                _context.UserQuest.Add(new UserQuest { User = _context.User.Find(id.id_user), Quest = _context.Quest.Find(id.id_quest) });
            await _context.SaveChangesAsync();
            return Ok();
        }
        private Task<User> GetCurrentUserAsync() =>
        _userManager.GetUserAsync(HttpContext.User);

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Ids id)//удаление квесте из избранного
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var item = _context.UserQuest.Find(id.id_quest, id.id_user);//

            if (item == null) { return NotFound(); }
            _context.UserQuest.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
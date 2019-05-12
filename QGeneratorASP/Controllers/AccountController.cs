using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QGeneratorASP.Models;

namespace QGeneratorASP.Controllers
{
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly GQ _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, GQ context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpPost]
        [Route("api/Account/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.Login, AccessLevel=model.AccessLevel};
                // Добавление нового пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");//
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    var msg = new
                    {
                        message = "Добавлен новый пользователь: " + user.UserName
                    };
                    return Ok(msg);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    var errorMsg = new
                    {
                        message = "Пользователь не добавлен.",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Ok(errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Неверные входные данные.",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                return Ok(errorMsg);
            }
        }

        [HttpPost]
        [Route("api/Account/Login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var msg = new
                    {
                        message = "Выполнен вход пользователем: " + model.Login
                    };
                    return Ok(msg);
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    var errorMsg = new
                    {
                        message = "Вход не выполнен.",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Ok(errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен.",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                return Ok(errorMsg);
            }
        }

        [HttpPost]
        [Route("api/account/logoff")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // Удаление куки
            await _signInManager.SignOutAsync();
            var msg = new
            {
                message = "Выполнен выход."
            };
            return Ok(msg);
        }
        [HttpPost]
        [Route("api/Account/GetUser")]
        public async Task<IActionResult> GetUser()
        {
            User usr = await GetCurrentUserAsync();
            if (usr == null) { return Ok(-1); }
            var user = await _context.User
                .Include(u => u.UserQuest)
                     .ThenInclude(u => u.Quest).ThenInclude(r=>r.Level_of_complexity)
                .Include(r=>r.UserQuest)
                     .ThenInclude(u => u.Quest).ThenInclude(r=>r.User)
                .Include(r => r.UserQuest)
                     .ThenInclude(u => u.Quest).ThenInclude(r=>r.QuestRiddle).ThenInclude(r=>r.Riddle).ThenInclude(a => a.Answer)
                .Include(r => r.UserQuest)
                     .ThenInclude(u => u.Quest).ThenInclude(r => r.QuestRiddle).ThenInclude(r => r.Riddle).ThenInclude(a => a.Level_of_complexity)
                 .Include(r => r.UserQuest)
                     .ThenInclude(u => u.Quest).ThenInclude(r => r.QuestRiddle).ThenInclude(r => r.Riddle).ThenInclude(a => a.Type_of_question)
                 .Include(r => r.UserQuest)
                     .ThenInclude(u => u.Quest).ThenInclude(r => r.QuestRiddle).ThenInclude(r => r.Riddle).ThenInclude(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == usr.Id);
            if (user == null) { return NotFound(); }
            return Ok(user);
        }
        [HttpPost]
        [Route("api/Account/isAuthenticated")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogisAuthenticatedOff()
        {
            User usr = await GetCurrentUserAsync();
            /* var message = usr == null ? "Вы Гость. Пожалуйста, выполните вход." : "Вы вошли как: " + usr.UserName;
             var msg = new
                 {
                     message
                 };
             return Ok(msg);*/
            if (usr == null) { return Ok(-1); }
            return Ok(usr.Id);
            // return Ok(usr == null ? false : true);
        }
        [HttpPost]
        [Route("api/Account/getUser")]
        //[ValidateAntiForgeryToken]
       /* public async Task<IActionResult> GetUser()
        {
            User usr = await GetCurrentUserAsync();
            if (usr == null) { return NotFound(); }
            return Ok(usr.Id);
           
        }*/
        private Task<User> GetCurrentUserAsync() =>
        _userManager.GetUserAsync(HttpContext.User);

    }
}
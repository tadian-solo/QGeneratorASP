using QGeneratorASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QGeneratorASP.Data
{
    public class DbInitializer
    { 
        
        public static void Initialize(GQ context)
        {
            context.Database.EnsureCreated();

            if (context.Quest.Any()&&context.Riddle.Any())
            {
                return;
            }
            var lvls = new Level_of_complexity[]
            {
                new Level_of_complexity{Name_level="легче легкого"},
                new Level_of_complexity{Name_level="легко"},
                new Level_of_complexity{Name_level="средне"},
                new Level_of_complexity{Name_level="сложно"},
                new Level_of_complexity{Name_level="очень сложно"},

            };
            foreach(var ls in lvls)
            {
                context.Level_of_complexity.Add(ls);
            }
            var answs = new Answer[]
            {
                new Answer{Object= "стол"},
                new Answer{Object= "окно"},
                new Answer{Object= "кресло"},
                new Answer{Object= "холодильник"},
                new Answer{Object= "балкон"},
              
            };
            foreach (var ls in answs)
            {
                context.Answer.Add(ls);
            }
            var tps = new Type_of_question[]
            {
                new Type_of_question{ Name="загадка"},
                new Type_of_question{ Name="анаграмма"},
                new Type_of_question{ Name="шифр"}
            };
            foreach (var ls in tps)
            {
                context.Type_of_question.Add(ls);
            }
            context.User.Add(new User { UserName = "admin", PasswordHash = "123", AccessLevel = true });
            context.SaveChanges();
            
            var quests = new Quest[]
            {
                new Quest
                {
                    Status = true,
                    Date = DateTime.Now,
                    Thematics = "новогодний квест",
                    User = context.User.FirstOrDefault(),
                    Level_of_complexity = lvls[0]
                },
                new Quest
                {
                    Status = true,
                    Date = DateTime.Now,
                    Thematics = "sci-fi",
                    User = context.User.FirstOrDefault(),
                    Level_of_complexity = lvls[1]
                },
                new Quest
                {
                    Status = true,
                    Date = DateTime.Now,
                    Thematics = "детектив в стиле Шерлока Холмса",
                    User = context.User.FirstOrDefault(),
                    Level_of_complexity = lvls[2]
                },
                 new Quest
                {
                    Status = true,
                    Date = DateTime.Now,
                    Thematics = "квест по мотивам славянской мифологии",
                    User = context.User.FirstOrDefault(),
                    Level_of_complexity = lvls[2]
                }

            };
            foreach (Quest b in quests)
            {
                context.Quest.Add(b);
            }
            context.SaveChanges();
            

             var riddle = new Riddle[]
             {
                new Riddle
                {
                    Text = "something very great",
                    Description = "dfdfd",
                    Status = true,
                    Answer =answs[0],
                    Level_of_complexity =lvls[0],
                    Type_of_question =tps[0],
                    User=context.User.FirstOrDefault()
                },
                new Riddle
                {
                    Text = "omeg eastvery thingr",
                    Description = "dfdfd",
                    Status = true,
                    Answer =answs[1],
                    Level_of_complexity =lvls[1],
                    Type_of_question =tps[1],
                    User=context.User.FirstOrDefault()
                },
                new Riddle
                {
                    Text = "so vemethg reingry at",
                    Description = "dfdfd",
                    Status = true,
                    Answer =answs[0],
                    Level_of_complexity =lvls[2],
                    Type_of_question =tps[1],
                    User=context.User.FirstOrDefault()
                }

             };
             foreach (Riddle p in riddle)
             {
                 context.Riddle.Add(p);
             }
             context.SaveChanges();

            context.QuestRiddle.Add(new QuestRiddle { Riddle = context.Riddle.Find(riddle[1].Id_riddle) , Quest = context.Quest.Find(quests[0].Id_quest) });
            context.QuestRiddle.Add(new QuestRiddle { Riddle = context.Riddle.Find(riddle[2].Id_riddle), Quest = context.Quest.Find(quests[1].Id_quest) });
            context.QuestRiddle.Add(new QuestRiddle { Riddle = context.Riddle.Find(riddle[1].Id_riddle), Quest = context.Quest.Find(quests[1].Id_quest) });
            context.UserQuest.Add(new UserQuest { Quest = context.Quest.Find(quests[1].Id_quest), User = context.User.FirstOrDefault() });
            context.SaveChanges();
        }
    }
}

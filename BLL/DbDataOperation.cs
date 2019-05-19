using DAL;
using GeneretorQuests.Models.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DbDataOperation
    {
        DBRepos db;
        public DbDataOperation(DBRepos rep)
        {
            db = rep;

        }
        ObservableCollection<Answer> a;
        ObservableCollection<Level_of_complexity> l;
        ObservableCollection<Quest> q;
        ObservableCollection<Riddle> r;
        ObservableCollection<Type> t;
        ObservableCollection<User> u;

        public void CreateAnswer(Answer item)
        {
            db.Answers.Create(item);
            db.Save();
           // GetAllAnswer();
        }

        public void DeleteAnswer(int id)
        {
            Answer k = db.Answers.GetItem(id);
            if (k != null) db.Answers.Delete(id);
        }

        public Answer GetAnswer(int id)
        {
            return db.Answers.GetItem(id);
        }

        public ObservableCollection<Answer> GetAllAnswer()
        {
            return db.Answers.GetList();
        }
        public ObservableCollection<Answer> GetListAnswerForType(int id, int level)
        {
            return db.Answers.GetListForType(id, level);
        }

       
        public void UpdateAnswer(Answer item)
        {
            Answer an = db.Answers.GetItem(item.Id_answer);
            db.Answers.Update(item);
        }

        public void CreateLevel(Level_of_complexity item)
        {
            db.Levels.Create(item);
            db.Save();

        }

        public void DeleteLevel(int id)
        {
            Level_of_complexity k = db.Levels.GetItem(id);
            if (k != null) db.Levels.Delete(k.Id_level);
        }

        public Level_of_complexity GetLevel(int id)
        {
            return db.Levels.GetItem(id);
        }

        public ObservableCollection<Level_of_complexity> GetAllLevel()
        {
            return db.Levels.GetList();
        }

        public void UpdateLevel(Level_of_complexity item)
        {
            Level_of_complexity l = db.Levels.GetItem(item.Id_level);
            db.Levels.Update(item);
        }
        public void CreateQuest(Quest item)
        {
            db.Quests.Create(item);
            db.Save();
        }

        public void DeleteQuest(int id)
        {
            Quest k = db.Quests.GetItem(id);
            if (k != null) db.Quests.Delete(k.Id_quest);

        }

        public Quest GetQuest(int id)
        {
            return db.Quests.GetItem(id);
        }

        public ObservableCollection<Quest> GetAllQuest()
        {
            return db.Quests.GetList();
        }

        public void UpdateQuest(Quest item)
        {
            //Quest q = db.Quests.GetItem(item.Id_quest);
            item.Number_of_questions = item.Riddle.Count;
            int sum=0;
            foreach (var r in item.Riddle) sum += r.Id_Level_FK;
            if(item.Riddle.Count!=0)
            {   
                item.Level_of_complexity = GetLevel(sum/ item.Riddle.Count);
                item.Id_Level_FK = item.Level_of_complexity.Id_level;
            }
            db.Quests.Update(item);
            
        }
        Random rand = new Random();
        List<string> descriptions = new List<string>() {
            "Ну-ка, попробуй отгадай!",
            "Куда лежит твой путь, узнаешь тут",
            "Молодец, ты почти у цели!",
            "Вперед, путник, найди сокровище!",
            "Подарок ждет тебя, забери его"};

        public void CreateRiddle(Riddle item)
        {

            if (String.IsNullOrWhiteSpace(item.Description)) item.Description = GenerDescription();
            db.Riddls.Create(item);
            db.Save();
        }

        public void DeleteRiddle(int id)
        {
            Riddle k = db.Riddls.GetItem(id);
            if (k != null) db.Riddls.Delete(k.Id_riddle);
            db.Save();
        }

        public Riddle GetRiddle(int id)
        {
            return db.Riddls.GetItem(id);
        }

        public ObservableCollection<Riddle> GetAllRiddle()
        {
            return db.Riddls.GetList();
        }

        public void UpdateRiddle(Riddle item)
        {
            if (String.IsNullOrWhiteSpace(item.Description)) item.Description = GenerDescription();
           // Riddle r = db.Riddls.GetItem(item.Id_riddle);
            db.Riddls.Update(item);
            db.Save();
        }

        public ObservableCollection<Riddle> GetListRiddleForQuest(int id)
        {

            return db.Riddls.GetListForQuest(id);
        }
        public string GenerDescription()
        {

            return descriptions[rand.Next(0, descriptions.Count)];

        }

        public void SaveRiddle(bool access_level, bool isNewChecked, Riddle r)
        {
            if (access_level && !isNewChecked) UpdateRiddle(r);
            else CreateRiddle(r);
        }
        public Type_of_question GetType(int id)
        {
            return db.Types.GetItem(id);
        }

        public ObservableCollection<Type_of_question> GetAllType()
        {
            return db.Types.GetList();
        }
        public ObservableCollection<Type_of_question> GetListTypeForLevel(int id)
        {

            return db.Types.GetListForLevel(id);
        }
        public User GetUser(int id)
        {
            return db.Users.GetItem(id);
        }
        public void CreateUser(User u)
        {
            db.Users.Create(u);
            db.Save();
        }
        public User GetTopUser()
        {
            return db.Users.GetTopUser();
        }
        public ObservableCollection<Riddle> GetTopRiddls()
        {
            return db.Riddls.GetTopRiddls();
        }
    }
}

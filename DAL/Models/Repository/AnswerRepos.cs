using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneretorQuests.Models.Repository
{
    public class AnswerRepos
    {
        private GQ db;
        public AnswerRepos(GQ dbContext)
        {
            db = dbContext;
            db.Answer.Load();
        }

        public void Create(Answer item)
        {
            db.Answer.Add(item);
            db.SaveChanges();
            Console.WriteLine("okey");
        }

        public void Delete(int id)
        {
            Answer k = db.Answer.Find(id);
            if (k != null) db.Answer.Remove(k);
        }

        public Answer GetItem(int id)
        {
            return db.Answer.Find(id);
        }

        public ObservableCollection<Answer> GetList()
        {
            return db.Answer.Local;
        }
        public ObservableCollection<Answer> GetListForType(int id, int level)
        {
            ObservableCollection<Answer> answers = new ObservableCollection<Answer>();
            var rs = db.Riddle.Where(i => (i.Id_Type_FK == id)&&(i.Id_Level_FK == level));
            foreach (var p in rs)
            {
                if (!answers.Contains(p.Answer)) answers.Add(p.Answer);
            }
            return answers;
        }

        public void Update(Answer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}

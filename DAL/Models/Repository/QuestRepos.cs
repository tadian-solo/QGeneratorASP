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
    public class QuestRepos
    {
        private GQ db;
        public QuestRepos(GQ dbContext)
        {
            db = dbContext;
            db.Quest.Include("Level_of_complexity").Include("Riddle").Include("User").Load();
        }

        public void Create(Quest item)
        {
            db.Quest.Add(item);
        }

        public void Delete(int id)
        {
            Quest k = db.Quest.Find(id);
            if (k != null) db.Quest.Remove(k);
            db.SaveChanges();
        }

        public Quest GetItem(int id)
        {
            return db.Quest.Find(id);
        }

        public ObservableCollection<Quest> GetList()
        {
            return db.Quest.Local;
        }

        public void Update(Quest item)
        {

         
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

        }

        
    }
}

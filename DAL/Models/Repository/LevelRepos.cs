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
    public class LevelRepos
    {
        private GQ db;
        public LevelRepos(GQ dbContext)
        {
            db = dbContext;
            db.Level_of_complexity.Load();
        }

        public void Create(Level_of_complexity item)
        {
            db.Level_of_complexity.Add(item);
        }

        public void Delete(int id)
        {
            Level_of_complexity k = db.Level_of_complexity.Find(id);
            if (k != null) db.Level_of_complexity.Remove(k);
        }

        public Level_of_complexity GetItem(int id)
        {
            return db.Level_of_complexity.Find(id);
        }

        public ObservableCollection<Level_of_complexity> GetList()
        {
            return db.Level_of_complexity.Local;
        }

        public void Update(Level_of_complexity item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}

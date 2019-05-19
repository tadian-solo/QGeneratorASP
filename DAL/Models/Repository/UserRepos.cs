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
    public class UserRepos
    {
        private GQ db;
        public UserRepos(GQ dbContext)
        {
            db = dbContext;
            db.User.Load();
        }

        public void Create(User item)
        {
            db.User.Add(item);
        }

        public void Delete(int id)
        {
            User k = db.User.Find(id);
            if (k != null) db.User.Remove(k);
        }

        public User GetItem(int id)
        {
            return db.User.Find(id);
        }
        public User GetItemForLogin(string login)
        {
            return db.User.FirstOrDefault(i=>i.Name.TrimEnd() == login);
        }

        public ObservableCollection<User> GetList()
        {
            return db.User.Local;
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public User GetTopUser()
        {
            return db.User.OrderByDescending(i => i.Quest.Count).FirstOrDefault();
        }
    }
}

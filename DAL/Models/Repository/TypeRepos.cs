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
    public class TypeRepos
    {
        private GQ db;
        public TypeRepos(GQ dbContext)
        {
            db = dbContext;
            db.Type_of_question.Load();
        }

        public void Create(Type_of_question item)
        {
            db.Type_of_question.Add(item);
        }

        public void Delete(int id)
        {
            Type_of_question k = db.Type_of_question.Find(id);
            if (k != null) db.Type_of_question.Remove(k);
        }

        public Type_of_question GetItem(int id)
        {
            return db.Type_of_question.Find(id);
        }

        public ObservableCollection<Type_of_question> GetList()
        {
            return db.Type_of_question.Local;
        }
       /* public ObservableCollection<Type_of_question> GetListForLevel(int id)
        {
            return db.Type_of_question.Local.Where(i=>i.Riddle.Id_Level_FK==);
        }*/

        public void Update(Type_of_question item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public ObservableCollection<Type_of_question> GetListForLevel(int id)
        {
            ObservableCollection<Type_of_question> types = new ObservableCollection<Type_of_question>();
            var rs = db.Riddle.Where(i => i.Id_Level_FK == id);
            foreach(var p in rs)
            {
                if (!types.Contains(p.Type_of_question)) types.Add(p.Type_of_question);
            }
            return types;
        }
            
    }
}

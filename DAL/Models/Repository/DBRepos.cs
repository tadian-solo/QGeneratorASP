using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneretorQuests.Models.Repository
{
    public class DBRepos
    {
        private GQ db;
        AnswerRepos answerRepos;
        LevelRepos levelRepos;
        QuestRepos questRepos;
        RiddleRepos riddleRepos;
        TypeRepos typeRepos;
        UserRepos userRepos;
        public DBRepos()
        {
            db = new GQ();

        }
        public AnswerRepos Answers
        {
            get
            {
                if (answerRepos == null)
                    answerRepos = new AnswerRepos(db);
                return answerRepos;
            }
        }
        public LevelRepos Levels
        {
            get
            {
                if (levelRepos == null)
                    levelRepos = new LevelRepos(db);
                return levelRepos;
            }
        }
        public QuestRepos Quests
        {
            get
            {
                if (questRepos == null)
                    questRepos = new QuestRepos(db);
                return questRepos;
            }
        }
        public RiddleRepos Riddls
        {
            get
            {
                if (riddleRepos == null)
                    riddleRepos = new RiddleRepos(db);
                return riddleRepos;
            }
        }
        public TypeRepos Types
        {
            get
            {
                if (typeRepos == null)
                    typeRepos = new TypeRepos(db);
                return typeRepos;
            }
        }
        public UserRepos Users
        {
            get
            {
                if (userRepos == null)
                    userRepos = new UserRepos(db);
                return userRepos;
            }
        }
        public int Save()
        {
            return db.SaveChanges();
        }

    }
}

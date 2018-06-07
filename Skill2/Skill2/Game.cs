using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skill2
{
    public class Role
    {
        public object name { get; set; }
        public object skills { get; set; }
        public object roleDescription { get; set; }
    }

    public class GameSkill
    {
        public string name { get; set; }
        public object description { get; set; }
    }

    public class Level
    {
        public string name { get; set; }
        public int rank { get; set; }
    }

    public class Game
    {
        public int _id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public IList<Role> roles { get; set; }
        public object gameType { get; set; }
        public string gameDescription { get; set; }
        public IList<GameSkill> gameSkills { get; set; }
        public IList<Level> levels { get; set; }
    }

    public class Games
    {
        public IList<Game> games { get; set; }

        public IList<Game> gamesSorted
        {
            get
            {
                IEnumerable<Game> sortedEnum = games.OrderBy(Game => Game.name);
                return sortedEnum.ToList<Game>();
            }
        }

        public Game GetGame(int _id)
        {
            foreach(Game game in games)
            {
                if(_id == game._id)
                {
                    return game;
                }
            }
            return null;
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Model
{
    public class FollowerPackage
    {
        public List<Follower> Followers = new List<Follower>();

        public string Name;
        public int Count
        {
            get
            {
                return Followers.Count;
            }
        }

        public void AddFollower(Follower f)
        {
            Followers.Add(f);
        }

        public Follower GetFollowerById(int id)
        {
            return Followers.Where(x => x.Id == id).First();
        }
    }
}

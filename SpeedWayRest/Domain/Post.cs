using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWayRest.Domain
{
    public class Post
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public Post(string id,string name)
        {
            this.Id = id;
            this.Name = name;
        }

    }
}

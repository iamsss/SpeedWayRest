using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWayRest.Domain
{
    public class Post
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Post(string name)
        {
            this.Name = name;
        }

    }
}

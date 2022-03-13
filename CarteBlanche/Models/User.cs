using System;
using System.Collections.Generic;

namespace CarteBlanche.Models
{
    public partial class User
    {
        public User()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Pwd { get; set; }
        public string? Cpwd { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}

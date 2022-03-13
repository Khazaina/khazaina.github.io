using System;
using System.Collections.Generic;

namespace CarteBlanche.Models
{
    public partial class Task
    {
        public int Tid { get; set; }
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Label { get; set; }
        public string? Priority { get; set; }

        public virtual User? IdNavigation { get; set; }
    }
}

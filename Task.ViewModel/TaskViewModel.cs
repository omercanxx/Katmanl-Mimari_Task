using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Task.Entity;

namespace Task.ViewModel
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Expression { get; set; }
        public string Status { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string UserId { get; set; }
        public virtual AspNetUsers User { get; set; }
        public string WhyRejected { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Entity;

namespace Task.DataAccess.Concrete.Ef
{
    public class MyModel
    {
        public Project Project { get; set; }
        public Task.Entity.Task Task { get; set; }
        public AspNetUsers User { get; set; }
    }
}

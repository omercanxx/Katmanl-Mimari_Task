using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.DataAccess.Concrete.Ef;
using Task.Entity;
using Task.ViewModel;

namespace Task.DataAccess.Abstract
{
    public interface IProject : IRepository<ProjectViewModel>
    {
        List<ProjectViewModel> GetActiveAll();
        List<ProjectViewModel> GetPasiveAll();
        bool Recovery(int id);
    }
}

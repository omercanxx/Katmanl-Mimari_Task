using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.DataAccess.Concrete.Ef;
using Task.ViewModel;

namespace Task.DataAccess.Abstract
{
    public interface ITask : IRepository<TaskViewModel>
    {
        List<TaskViewModel> ToDo(string id);
        List<TaskViewModel> Expected();
        List<TaskViewModel> Approved();
        bool Onayla(int id);
        bool Reddet(int id, string exp);
        bool Gonder(int id);
    }
}

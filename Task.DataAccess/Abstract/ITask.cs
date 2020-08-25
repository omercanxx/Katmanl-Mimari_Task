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
        List<MyModel> ToDo(string id);
        List<MyModel> Expected();
        List<MyModel> Approved();
        bool Approve(int id);
        bool Reject(int id, string explanation);
        bool Send(int id);
        string Show(int id);
    }
}

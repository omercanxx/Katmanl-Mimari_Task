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
        bool Onayla(int id);
        bool Reddet(int id, string exp);
        bool Gonder(int id);
        string Goster(int id);
    }
}

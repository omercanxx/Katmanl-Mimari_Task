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
    public interface IAppUser : IRepository<UserViewModel>
    {
        IEnumerable<UserRoleModel> GetPersonelAll();
        IEnumerable<UserRoleModel> GetManagerAll();
        bool SetTrue(string username);

    }
}

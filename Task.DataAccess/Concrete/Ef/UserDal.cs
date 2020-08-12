using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.DataAccess.Abstract;
using Task.ViewModel;
using Task.Entity;
using System.Data.Entity.Validation;

namespace Task.DataAccess.Concrete.Ef
{
    public class UserDal : IRepository<UserViewModel>, IAppUser
    {
        readonly TaskEntities db = new TaskEntities();
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> GetAll()
        {
            List<UserViewModel> userModel = new List<UserViewModel>();
            foreach (Entity.AspNetUsers user in db.AspNetUsers)
            {
                UserViewModel model = new UserViewModel();
                model.Id = user.Id;
                model.Email = user.Email;
                model.PasswordChanged = user.PasswordChanged;
                model.PasswordHash = user.PasswordHash;
                model.UserName = user.UserName;
                userModel.Add(model);
            }
            return userModel;
        }


        public AspNetUsers GetById(int id)
        {
            throw new NotImplementedException();
        }
        public bool SetTrue(string id)
        {
            Entity.AspNetUsers user = db.AspNetUsers.Find(id);
            if (user == null)
                return false;
            else
            {
                user.PasswordChanged = true;
                db.SaveChanges();
                return true;
            }

        }
        public IEnumerable<UserRoleModel> GetManagerAll()
        {
            var usersWithRoles = (from user in db.AspNetUsers
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      RoleNames = (from userRole in user.AspNetRoles
                                                   join role in db.AspNetRoles on userRole.Id
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserRoleModel()

                                  {
                                      UserID = p.UserId,
                                      Username = p.Username,
                                      Role = string.Join(",", p.RoleNames)
                                  });
            var managerList = usersWithRoles.Where(m => m.Role == "yonetici");
            return managerList;
        }

        public IEnumerable<UserRoleModel> GetPersonelAll()
        {
            var usersWithRoles = (from user in db.AspNetUsers
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      RoleNames = (from userRole in user.AspNetRoles
                                                   join role in db.AspNetRoles on userRole.Id
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserRoleModel()

                                  {
                                      UserID = p.UserId,
                                      Username = p.Username,
                                      Role = string.Join(",", p.RoleNames)
                                  });
            var personelList = usersWithRoles.Where(m => m.Role == "personel");
            return personelList;
        }

        public bool Insert(UserViewModel model)
        {
            throw new NotImplementedException();
        }

        public bool Update(UserViewModel model)
        {
            throw new NotImplementedException();
        }

        UserViewModel IRepository<UserViewModel>.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

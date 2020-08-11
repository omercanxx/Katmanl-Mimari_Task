using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.DataAccess.Abstract;
using Task.Entity;
using Task.ViewModel;

namespace Task.DataAccess.Concrete.Ef
{
    public class ProjectDal : IRepository<ProjectViewModel>, IProject
    {
        readonly TaskEntities db = new TaskEntities();

        List<MyModel> IProject.GetActiveAll()
        {
            var query = from prj in db.Project
                        join user in db.AspNetUsers on prj.UserId equals user.Id
                        where prj.IsVisible == true
                        select new MyModel
                        {
                            Project = prj,
                            User = user
                        };
            return query.ToList();
        }
        List<MyModel> IProject.GetPasiveAll()
        {
            var query = from prj in db.Project
                        join user in db.AspNetUsers on prj.UserId equals user.Id
                        where prj.IsVisible == false
                        select new MyModel
                        {
                            Project = prj,
                            User = user
                        };
            return query.ToList();
        }

        public bool Delete(int id)
        {
            Project prj = db.Project.Find(id);
            if (prj == null)
                return false;
            else
            {
                prj.IsVisible = false;
                db.SaveChanges();
                return true;
            }
        }
        public bool Recovery(int id)
        {
            Project prj = db.Project.Find(id);
            if (prj == null)
                return false;
            else
            {
                prj.IsVisible = true;
                db.SaveChanges();
                return true;
            }
        }

        public List<ProjectViewModel> GetAll()
        {
            List<ProjectViewModel> prjModel = new List<ProjectViewModel>();
            foreach (Project prj in db.Project)
            {
                ProjectViewModel model = new ProjectViewModel();
                model.Id = prj.Id;
                model.Name = prj.Name;
                model.Status = prj.Status;
                model.UserId = prj.UserId;
                model.CreatedDate = prj.CreatedDate;
                prjModel.Add(model);

            }
            return prjModel;
        }

        public ProjectViewModel GetById(int id)
        {
            Project prj = db.Project.Find(id);
            if (prj == null)
                return null;
            else
            {
                ProjectViewModel model = new ProjectViewModel();
                model.Id = prj.Id;
                model.Name = prj.Name;
                model.Status = prj.Status;
                model.UserId = prj.UserId;
                return model;
            }
        }

        public bool Insert(ProjectViewModel model)
        {
            Project prj = new Project();
            prj.Id = model.Id;
            prj.Name = model.Name;
            prj.Status = model.Status;
            prj.UserId = model.UserId;
            prj.IsVisible = true;
            prj.CreatedDate = DateTime.Now;
            db.Project.Add(prj);
            db.SaveChanges();
            return true;
        }

        public bool Update(ProjectViewModel model)
        {
            try
            {
                Project prj = new Project();
                prj.Id = model.Id;
                prj.Name = model.Name;
                prj.Status = model.Status;
                prj.UserId = model.UserId;
                prj.IsVisible = model.IsVisible;
                prj.CreatedDate = model.CreatedDate;
                db.Entry<Project>(prj).State =
                    System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

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

        List<ProjectViewModel> IProject.GetActiveAll()
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
                if (prj.IsVisible == true)
                {
                    prjModel.Add(model);
                }
            }
            return prjModel;
        }
        List<ProjectViewModel> IProject.GetPasiveAll()
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
                if (prj.IsVisible == false)
                {
                    prjModel.Add(model);
                }
            }
            return prjModel;
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

        List<ProjectViewModel> IRepository<ProjectViewModel>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

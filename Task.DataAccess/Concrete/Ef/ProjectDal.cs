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
            var query = from project in db.Project
                        join user in db.AspNetUsers on project.UserId equals user.Id
                        where project.IsVisible == true && project.Status == "active"
                        select new MyModel
                        {
                            Project = project,
                            User = user
                        };
            return query.ToList();
        }
        List<MyModel> IProject.GetPasiveAll()
        {
            var query = from project in db.Project
                        join user in db.AspNetUsers on project.UserId equals user.Id
                        where project.IsVisible == true && project.Status == "passive"
                        select new MyModel
                        {
                            Project = project,
                            User = user
                        };
            return query.ToList();
        }
        List<MyModel> IProject.GetDeletedProject()
        {
            var query = from project in db.Project
                        join user in db.AspNetUsers on project.UserId equals user.Id
                        where project.IsVisible == false
                        select new MyModel
                        {
                            Project = project,
                            User = user
                        };
            return query.ToList();
        }

        public bool Delete(int id)
        {
            Project project = db.Project.Find(id);
            if (project == null)
                return false;
            else
            {
                project.IsVisible = false;
                db.SaveChanges();
                return true;
            }
        }
        public bool Recovery(int id)
        {
            Project project = db.Project.Find(id);
            if (project == null)
                return false;
            else
            {
                project.IsVisible = true;
                db.SaveChanges();
                return true;
            }
        }

        public List<ProjectViewModel> GetAll()
        {
            List<ProjectViewModel> projectModel = new List<ProjectViewModel>();
            foreach (Project project in db.Project)
            {
                ProjectViewModel model = new ProjectViewModel();
                model.Id = project.Id;
                model.Name = project.Name;
                model.Status = project.Status;
                model.UserId = project.UserId;
                model.CreatedDate = project.CreatedDate;
                projectModel.Add(model);

            }
            return projectModel;
        }
        public List<ProjectViewModel> GetProjects()
        {
            List<ProjectViewModel> projectModel = new List<ProjectViewModel>();
            foreach (Project project in db.Project)
            {
                if (project.IsVisible == true)
                {
                    ProjectViewModel model = new ProjectViewModel();
                    model.Id = project.Id;
                    model.Name = project.Name;
                    model.Status = project.Status;
                    model.UserId = project.UserId;
                    model.CreatedDate = project.CreatedDate;
                    projectModel.Add(model);
                }
            }
            return projectModel;
        }
        public ProjectViewModel GetById(int id)
        {
            Project project = db.Project.Find(id);
            if (project == null)
                return null;
            else
            {
                ProjectViewModel model = new ProjectViewModel();
                model.Id = project.Id;
                model.Name = project.Name;
                model.Status = project.Status;
                model.UserId = project.UserId;
                return model;
            }
        }

        public bool Insert(ProjectViewModel model)
        {
            Project project = new Project();
            project.Id = model.Id;
            project.Name = model.Name;
            project.Status = model.Status;
            project.UserId = model.UserId;
            project.IsVisible = true;
            project.CreatedDate = DateTime.Now;
            db.Project.Add(project);
            db.SaveChanges();
            return true;
        }

        public bool Update(ProjectViewModel model)
        {
            try
            {
                Project project = new Project();
                project.Id = model.Id;
                project.Name = model.Name;
                project.Status = model.Status;
                project.UserId = model.UserId;
                project.IsVisible = model.IsVisible;
                project.CreatedDate = model.CreatedDate;
                db.Entry<Project>(project).State =
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

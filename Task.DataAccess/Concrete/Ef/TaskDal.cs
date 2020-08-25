using Microsoft.VisualBasic.ApplicationServices;
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
    public class TaskDal : IRepository<TaskViewModel>, ITask
    {
        readonly TaskEntities db = new TaskEntities();

        public List<TaskViewModel> GetAll()
        {
            List<TaskViewModel> taskModel = new List<TaskViewModel>();
            foreach (Entity.Task task in db.Task)
            {
                TaskViewModel model = new TaskViewModel();
                model.Id = task.Id;
                model.Name = task.Name;
                model.Status = task.Status;
                model.Expression = task.Expression;
                model.UserId = task.UserId;
                model.ProjectId = task.ProjectId;
                model.WhyRejected = task.WhyRejected;
                taskModel.Add(model);
            }
            return taskModel;
        }

        public TaskViewModel GetById(int id)
        {
            Entity.Task task = db.Task.Find(id);
            if (task == null)
                return null;
            else
            {
                TaskViewModel model = new TaskViewModel();
                model.Id = task.Id;
                model.Name = task.Name;
                model.Status = task.Status;
                model.Expression = task.Expression;
                model.ProjectId = task.ProjectId;
                model.UserId = task.UserId;
                model.WhyRejected = task.WhyRejected;
                return model;
            }
        }

        public List<MyModel> Approved()
        {
            var query = from task in db.Task
                        join project in db.Project on task.ProjectId equals project.Id
                        join user in db.AspNetUsers on task.UserId equals user.Id
                        where task.Status == "approved"
                        select new MyModel
                        {
                            Project = project,
                            Task = task,
                            User = user
                        };
            return query.ToList();
        }
        public List<MyModel> ToDo(string id)
        {
            var query = from task in db.Task
                        join project in db.Project on task.ProjectId equals project.Id
                        join user in db.AspNetUsers on task.UserId equals user.Id
                        where task.Status == "to do" && task.UserId == id
                        select new MyModel
                        {
                            Project = project,
                            Task = task,
                            User = user
                        };
            return query.ToList();
        }
        public List<MyModel> Expected()
        {
            var query = from task in db.Task
                        join project in db.Project on task.ProjectId equals project.Id
                        join user in db.AspNetUsers on task.UserId equals user.Id
                        where task.Status == "expecting"
                        select new MyModel
                        {
                            Project = project,
                            Task = task,
                            User = user
                        };
            return query.ToList();
        }
        public bool Delete(int id)
        {
            Entity.Task task = db.Task.Find(id);
            if (task == null)
                return false;
            else
            {
                db.Task.Remove(task);
                db.SaveChanges();
                return true;
            }
        }

        public bool Insert(TaskViewModel model)
        {
            Entity.Task task = new Entity.Task();
            task.Id = model.Id;
            task.Name = model.Name;
            task.Status = "to do";
            task.Expression = model.Expression;
            task.ProjectId = model.ProjectId;
            task.UserId = model.UserId;
            db.Task.Add(task);
            db.SaveChanges();
            return true;
        }
        public bool Update(TaskViewModel model)
        {
            try
            {
                Entity.Task task = new Entity.Task();
                task.Id = model.Id;
                task.Name = model.Name;
                task.Status = model.Status;
                task.Expression = model.Expression;
                task.ProjectId = model.ProjectId;
                task.UserId = model.UserId;
                task.WhyRejected = model.WhyRejected;
                db.Entry<Entity.Task>(task).State =
                    System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Send(int id)
        {
            Entity.Task task = db.Task.Find(id);
            if (task == null)
                return false;
            else
            {
                task.Status = "expecting";
                db.SaveChanges();
                return true;
            }
        }

        public string Show(int id)
        {
            Entity.Task task = db.Task.Find(id);
            string explanation = task.WhyRejected;
            return explanation;
        }
        public bool Approve(int id)
        {
            Entity.Task task = db.Task.Find(id);
            if (task == null)
                return false;
            else
            {
                task.Status = "approved";
                db.SaveChanges();
                return true;
            }
        }

        public bool Reject(int id, string explanation)
        {
            Entity.Task task = db.Task.Find(id);
            if (task == null)
                return false;
            else
            {
                task.Status = "to do";
                task.WhyRejected = explanation;
                db.SaveChanges();
                return true;
            }
        }


    }
}

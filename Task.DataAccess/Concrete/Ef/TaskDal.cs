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
            List<TaskViewModel> tskModel = new List<TaskViewModel>();
            foreach (Entity.Task tsk in db.Task)
            {
                TaskViewModel model = new TaskViewModel();
                model.Id = tsk.Id;
                model.Name = tsk.Name;
                model.Status = tsk.Status;
                model.Expression = tsk.Expression;
                model.UserId = tsk.UserId;
                model.ProjectId = tsk.ProjectId;
                tsk.WhyRejected = model.WhyRejected;
                tskModel.Add(model);
            }
            return tskModel;
        }

        public TaskViewModel GetById(int id)
        {
            Entity.Task tsk = db.Task.Find(id);
            if (tsk == null)
                return null;
            else
            {
                TaskViewModel model = new TaskViewModel();
                model.Id = tsk.Id;
                model.Name = tsk.Name;
                model.Status = tsk.Status;
                model.Expression = tsk.Expression;
                model.ProjectId = tsk.ProjectId;
                model.UserId = tsk.UserId;
                tsk.WhyRejected = model.WhyRejected;
                return model;
            }
        }

        public List<MyModel> Approved()
        {
            /*List<MyModel> tskModel = new List<MyModel>();
            foreach (Entity.Task tsk in db.Task)
            {
                MyModel model = new MyModel();
                model.Task.Id = tsk.Id;
                model.Task.Name = tsk.Name;
                model.Task.Status = tsk.Status;
                model.Task.Expression = tsk.Expression;
                model.Task.UserId = tsk.UserId;
                model.Task.ProjectId = tsk.ProjectId;
                model.Task.WhyRejected = tsk.WhyRejected;
                if(model.Task.Status == "onaylandı")
                {
                    tskModel.Add(model);
                }
            }
            return tskModel;*/
            var query = from tsk in db.Task
                        join prj in db.Project on tsk.ProjectId equals prj.Id
                        join user in db.AspNetUsers on tsk.UserId equals user.Id
                        where tsk.Status == "onaylandı"
                        select new MyModel
                        {
                            Project = prj,
                            Task = tsk,
                            User = user
                        };
            return query.ToList();
        }
        public List<MyModel> ToDo(string id)
        {
            var query = from tsk in db.Task
                        join prj in db.Project on tsk.ProjectId equals prj.Id
                        join user in db.AspNetUsers on tsk.UserId equals user.Id
                        where tsk.Status == "yapılmadı" && tsk.UserId == id
                        select new MyModel
                        {
                            Project = prj,
                            Task = tsk,
                            User = user
                        };
            return query.ToList();
        }
        public List<MyModel> Expected()
        {
            var query = from tsk in db.Task
                        join prj in db.Project on tsk.ProjectId equals prj.Id
                        join user in db.AspNetUsers on tsk.UserId equals user.Id
                        where tsk.Status == "onay bekliyor"
                        select new MyModel
                        {
                            Project = prj,
                            Task = tsk,
                            User = user
                        };
            return query.ToList();
        }
        public bool Delete(int id)
        {
            Entity.Task tsk = db.Task.Find(id);
            if (tsk == null)
                return false;
            else
            {
                db.Task.Remove(tsk);
                db.SaveChanges();
                return true;
            }
        }

        public bool Insert(TaskViewModel model)
        {
            Entity.Task tsk = new Entity.Task();
            tsk.Id = model.Id;
            tsk.Name = model.Name;
            tsk.Status = "yapılmadı";
            tsk.Expression = model.Expression;
            tsk.ProjectId = model.ProjectId;
            tsk.UserId = model.UserId;
            db.Task.Add(tsk);
            db.SaveChanges();
            return true;
        }
        public bool Update(TaskViewModel model)
        {
            try
            {
                Entity.Task tsk = new Entity.Task();
                tsk.Id = model.Id;
                tsk.Name = model.Name;
                tsk.Status = model.Status;
                tsk.Expression = model.Expression;
                tsk.ProjectId = model.ProjectId;
                tsk.UserId = model.UserId;
                tsk.WhyRejected = model.WhyRejected;
                db.Entry<Entity.Task>(tsk).State =
                    System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Gonder(int id)
        {
            Entity.Task tsk = db.Task.Find(id);
            if (tsk == null)
                return false;
            else
            {
                tsk.Status = "onay bekliyor";
                db.SaveChanges();
                return true;
            }
        }

        public string Goster(int id)
        {
            Entity.Task tsk = db.Task.Find(id);
            string exp = tsk.WhyRejected;
            return exp;
        }
        public bool Onayla(int id)
        {
            Entity.Task tsk = db.Task.Find(id);
            if (tsk == null)
                return false;
            else
            {
                tsk.Status = "onaylandı";
                db.SaveChanges();
                return true;
            }
        }

        public bool Reddet(int id,string exp)
        {
            Entity.Task tsk = db.Task.Find(id);
            if (tsk == null)
                return false;
            else
            {
                tsk.Status = "yapılmadı";
                tsk.WhyRejected = exp;
                db.SaveChanges();
                return true;
            }
        }


    }
}

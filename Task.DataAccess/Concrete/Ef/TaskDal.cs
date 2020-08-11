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
        public List<TaskViewModel> Task()
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
                model.WhyRejected = tsk.WhyRejected;
                if (model.Status == "onaylandı")
                {
                    tskModel.Add(model);
                }
            }
            return tskModel;
        }
        public List<TaskViewModel> Approved()
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
                model.WhyRejected = tsk.WhyRejected;
                if(model.Status == "onaylandı")
                {
                    tskModel.Add(model);
                }
            }
            return tskModel;
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

        public List<TaskViewModel> Expected()
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
                model.WhyRejected = tsk.WhyRejected;
                if (model.Status == "onay bekliyor")
                {
                    tskModel.Add(model);
                }
            }
            return tskModel;
        }

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
                db.SaveChanges();
                return true;
            }
        }

        public List<TaskViewModel> ToDo(string id)
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
                model.WhyRejected = tsk.WhyRejected;
                if (model.Status == "yapılmadı" && model.UserId == id)
                {
                    tskModel.Add(model);
                }
            }
            return tskModel;
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
    }
}

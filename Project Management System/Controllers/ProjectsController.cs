using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Data;
using Project_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_System.Controllers
{
    public class ProjectsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectsController
        public ActionResult Index()
        {


            // Check if the user is in the user role.
            if (User.IsInRole("User"))
            {
                // get all the projects that belong to the currently signed in user.
                var user = _context.Users.First(u => u.Email == User.Identity.Name);
                var projectList = _context.projects.Where(u => u.UserID == user.Id).Include(pt => pt.Tasks).ThenInclude(t => t.Status).ToList();
               
                
                // return the view as well as all the projects the user has.
                return View(projectList);
                
            }
            else {
                return View();
            }
        }

        // GET: ProjectsController/ProjectView/5
        public ActionResult ProjectView(int? id)
        {
            if (!User.IsInRole("User")) {
                return RedirectToAction("PleaseLoginToView");
            }
            if (id != null)
            {
                var project = new Project();
                var user = _context.Users.First(u => u.Email == User.Identity.Name);
                var projectTasks = new List<ProjTask>();
                try
                {
                    var projects = _context.projects.Where(p => p.ID == id).Include(pt => pt.Tasks).ThenInclude(pt => pt.Status).Include(pt => pt.Tasks).ThenInclude(pt => pt.SubTasks).ThenInclude(s => s.Status).ToList();
                    project = projects.First(p => p.ID == id);
                   
                    
                    if (project.UserID == user.Id)
                    {
                       
                        return View(project);
                    }
                    else {
                        return RedirectToAction("UnnauthorizedAccess");
                    }
                }
                catch {
                    return RedirectToAction("ProjectNotFound");
                }
               
            }
            else
            {
                return RedirectToAction("Index");
                
            }
        }

        // GET: ProjectsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {

                Microsoft.Extensions.Primitives.StringValues nameOutput;
               
                collection.TryGetValue("Name", out nameOutput);
                
                var u =_context.Users.First(u => u.Email == User.Identity.Name);

                Project project = new Project { Name = nameOutput, UserID = u.Id };
                
                _context.projects.Add(project);
                _context.SaveChanges();
                //_context.projects.Add()
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProjectsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProjectsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // UnnauthorizedAccess ActionResult, used when a user is trying to access something they shouldnt.
        public ActionResult UnnauthorizedAccess() {
            return View();
        }

        // PleaseLoginToView ActionResult, used when a non logged in user is trying to access something that requires authentication, through being signed in.
        public ActionResult PleaseLoginToView() {
            return View();
        }

        

        //Create Task Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTask(int projectID) {
            ProjectTaskModel projectTaskModel = new ProjectTaskModel();
            projectTaskModel.ProjectID = projectID;

            return View(projectTaskModel);
        }


       
        // Add SubTasks to a ProjTask
        public ActionResult CreateSubtask() {
            return View();
        }


        //"/Projects/DeleteProjTask"
        [HttpPost]
        public JsonResult DeleteProjTask(int projTaskID) {
            var projTask = _context.projTasks.First(p => p.ID == projTaskID);
            _context.projTasks.Remove(projTask);
            _context.SaveChanges();
            return Json("Suceeded");
        }

        // POST: ProjectsController/UpdateProjTaskStatus/name
        [HttpPost]
        
        public JsonResult UpdateProjTaskStatus(int id, string status, int projectId)
        {

            var statuses = _context.taskStatuses;

            var project = _context.projects.Where(p => p.ID == projectId).Include(pt => pt.Tasks).ThenInclude(pt => pt.Status).ToList();

            var stat = statuses.First(s => s.Name.ToLower() == status.ToLower());

            project.First().Tasks.First(t => t.ID == id).Status = stat;
            _context.SaveChanges();
           
            return Json("Suceeded");
        
        }

        //Create Task API Call
        [HttpPost]
        public JsonResult ApiCreateTask(int projectID, string name, string description)
        {

            ProjTask projTask = new ProjTask();

            projTask.Name = name;
            projTask.Description = description;
            projTask.Status = _context.taskStatuses.First(s => s.Name == "Incomplete");

            //Add the newly created projTask to the database
            _context.projTasks.Add(projTask);

            //Add the projTask to the project within the database
            _context.projects.First(p => p.ID == projectID).Tasks.Add(projTask);

            _context.SaveChanges();

            return Json("Suceeded");

        }

        // Creates the task within the selected project.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTaskDB(int projectID, string name, string description)
        {

            ProjTask projTask = new ProjTask();

            projTask.Name = name;
            projTask.Description = description;
            projTask.Status = _context.taskStatuses.First(s => s.Name == "Incomplete");

            //Add the newly created projTask to the database
            _context.projTasks.Add(projTask);

            //Add the projTask to the project within the database
            _context.projects.First(p => p.ID == projectID).Tasks.Add(projTask);
            //_context.projTasks.Add(projTask);

            _context.SaveChanges();


            return RedirectToAction("Index");


        }


        //Create SubTask API Call
        [HttpGet]
        public JsonResult ApiCreateSubTask(int taskID, string name, string description)
        {

            SubTasks subTask = new SubTasks();

            subTask.Name = name;
            subTask.Description = description;
            subTask.Status = _context.taskStatuses.First(s => s.Name == "Incomplete");


            var project = _context.projects.Where(p => p.Tasks.First(t => t.ID == taskID).ID == taskID).Include(pt => pt.Tasks).ThenInclude(pt => pt.SubTasks);
            //Add the newly created projTask to the database
            _context.subTasks.Add(subTask);

            //Add the projTask to the project within the database
            project.First().Tasks.First(st => st.ID == taskID).SubTasks.Add(subTask);

            _context.SaveChanges();

            var data = new
            {
                stID = subTask.ID
            };


            return Json(data);

        }



        //Create SubTask API Call
        [HttpPost]
        public JsonResult ApiChangeSubTaskStatus(int subTaskID, bool completed)
        {
            var subTask = _context.subTasks.First(st => st.ID == subTaskID);
            if (completed == true)
            {
                subTask.Status = _context.taskStatuses.First(ts => ts.Name == "Complete");
            }
            else {
                subTask.Status = _context.taskStatuses.First(ts => ts.Name == "Incomplete");
            }
            _context.SaveChanges();


            return Json("Suceeded");
        }


        //get the progress details of a task and the subtasks
        [HttpGet]
        public JsonResult ApiGetSubTaskProgressDetails(int taskID) {

            var task = _context.projTasks.Where(st => st.ID == taskID).Include(st => st.SubTasks).ThenInclude(st => st.Status).Include(st => st.Status).First();

            var data = new
            {
                totalSubTasks = task.SubTasks.Count(),
                totalCompleteTasks = task.SubTasks.Where(st => st.Status.Name == "Complete").Count(),
                taskStatus = task.Status.Name.ToLower()
            };
            


            return Json(data);
        }


        // get a subtasks status 
        [HttpGet]
        public JsonResult ApiGetSubTaskStatus(int subTaskID) {
            var subTask = _context.subTasks.Where(st => st.ID == subTaskID).Include(st => st.Status).First();

            var status = subTask.Status;
            return Json(status);
        }



        // delete a subtask from the projectTask
        //"/Projects/DeleteProjTask"
        [HttpPost]
        public JsonResult APIDeleteSubTask(int subTaskID)
        {
            var subTask = _context.subTasks.First(st => st.ID == subTaskID);

            _context.subTasks.Remove(subTask);
            _context.SaveChanges();
            return Json("Suceeded");
        }



        // create a new task and save it to the database
        // "Projects/APICreateNewTask"
        [HttpPost]
        public JsonResult ApiCreateNewTask(string name, string description, int projectId)
        {
            //create the new task object
            var task = new Data.ProjTask();

            // fill it with all the needed information
            task.Name = name;
            task.Description = description;
            task.Status = _context.taskStatuses.ToList().Where(s => s.Name == "Incomplete").First();
            _context.projects.Where(p => p.ID == projectId).First().Tasks.Add(task);
            // add and save the task to the database
            _context.projTasks.Add(task);
            _context.SaveChanges();

            return Json("Suceeded");
        }
    }
}

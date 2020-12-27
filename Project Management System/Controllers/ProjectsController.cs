using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
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
                var projectList = _context.projects.ToList();
                var user = _context.Users.First(u => u.Email == User.Identity.Name);
                
                // return the view as well as all the projects the user has.
                return View(projectList.FindAll(u => u.UserID == user.Id));
                
            }
            else {
                return View();
            }
        }

        // GET: ProjectsController/Details/5
        public ActionResult ProjectView(int? id)
        {
            if (!User.IsInRole("User")) {
                return RedirectToAction("PleaseLoginToView");
            }
            if (id != null)
            {
                var project = new Project();
                var user = _context.Users.First(u => u.Email == User.Identity.Name);
                try
                {
                    project = _context.projects.First(p => p.ID == id);
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



        // Creates the task within the selected project.
        public ActionResult CreateTask(int projectID, string name, string description) {

            ProjTask projTask = new ProjTask();
            
            projTask.Name = name;
            projTask.Description = description;
            projTask.Status = _context.taskStatuses.First(s => s.Name == "Incomplete");

            //Add the newly created projTask to the database
            _context.projTasks.Add(projTask);

            //Add the projTask to the project within the database
            _context.projects.First(p => p.ID == projectID).Tasks.Add(projTask);

            _context.SaveChangesAsync();
            
            return View();

        }

        // Add SubTasks to a ProjTask
        public ActionResult CreateSubtask() {
            return View();
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.ViewModels;

namespace ProjectManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;

        public class EditTaskModel
        {
            public Models.Status Status { get; set; }
            [DataType(DataType.Date)]
            public DateTime Deadline { get; set; }
            public string Description { get; set; }
            public int Progress { get; set; }
            public string AssigneeId { get; set; }
        }


        public TasksController(ApplicationDbContext context, UserManager<Models.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Developer"))
            {
                return View(await _context.Task.Include(t => t.Project).Include(t => t.Status).Where(t => t.Assignee.UserName.Equals(User.Identity.Name)).ToListAsync());
            }
            else
            {
                return View(await _context.Task.Include(t => t.Project).Include(t => t.Status).ToListAsync());
            }
            
        }

        [Authorize]
        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task.Include(t => t.Project).Include(t => t.Status).Include(t => t.Assignee)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [Authorize(Roles = "Administrator,ProjectManager")]
        // GET: Tasks/Create
        public IActionResult Create()
        {
            Models.Task model = new();
            ViewBag.Users = _context.Users.ToList();
            ViewBag.Projects = _context.Project.ToList();
            ViewBag.Statuses = _context.Status.Select(s => s.Name);
            return View(model);
        }

        [Authorize(Roles = "Administrator,ProjectManager")]
        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Task model)
        {
            if(model.Progress < 0 || model.Progress > 100)
            {
                TempData["NotCreatedMessage"] = "Progress has to be between 0 and 100";
                return Create();
            }

            if (ModelState.IsValid)
            {
                var role = "";

                var project = _context.Project.Find(model.Project.ProjectCode);
                model.Project = project;
                var status = _context.Status.Where(s => s.Name == model.Status.Name).FirstOrDefault();
                model.Status = status;
                if (model.Assignee != null && model.Assignee.Id != null)
                {
                    var assignee = _userManager.FindByIdAsync(model.Assignee.Id).Result;
                    model.Assignee = assignee;

                    var roleId = _context.UserRoles.AsNoTracking()
                            .Where(ur => ur.UserId.Equals(assignee.Id)).FirstOrDefault().RoleId;

                    role = _context.Roles
                        .Where(r => r.Id.Equals(roleId)).FirstOrDefault().Name;
                }
                else
                {
                    model.Assignee = null;
                }

                int count = _context.Task.Include("Assignee").Where(t => t.Assignee.Id.Equals(model.Assignee.Id) && !t.TaskId.Equals(model.TaskId)).Count();
                if ((role.Equals("Developer") && count < 3) || !role.Equals("Developer"))
                {
                    var user = await _userManager.FindByIdAsync(model.Assignee.Id);
                    model.Assignee = user;
                }
                else
                {
                    TempData["NotCreatedMessage"] = "You cannot assign more than 3 tasks to a Developer.";
                    return Create();
                }

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        [Authorize]
        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task.FirstOrDefaultAsync(t => t.TaskId.Equals(id));
            if (task == null)
            {
                return NotFound();
            }

            ViewBag.Users = _context.Users.ToList();
            ViewBag.Projects = _context.Project.ToList();
            ViewBag.Statuses = _context.Status;

            EditTaskModel model = new()
            {
                Status = task.Status,
                Description = task.Description,
                Deadline = task.Deadline,
                Progress = task.Progress,
                AssigneeId = task.Assignee == null? "unassign" : task.Assignee.Id
            };
            ViewBag.BelongsToUser = task.Assignee == null ? false : task.Assignee.UserName.Equals(User.Identity.Name);
            return View(model);
        }

        [Authorize]
        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTaskModel model)
        {
            var selectedUserId = "";
            var role = "";

            var previousValue = _context.Task.AsNoTracking().Include("Assignee").Include("Project").Where(t => t.TaskId.Equals(id)).FirstOrDefault();

            if (!User.IsInRole("Developer"))
            {
                if(model.AssigneeId != null && !model.AssigneeId.Equals("unassign"))
                {
                    selectedUserId = _context.Users.AsNoTracking().Where(u => u.Id == model.AssigneeId).FirstOrDefault().Id;
                    var roleId = _context.UserRoles.AsNoTracking()
                            .Where(ur => ur.UserId.Equals(selectedUserId)).FirstOrDefault().RoleId;

                    role = _context.Roles
                        .Where(r => r.Id.Equals(roleId)).FirstOrDefault().Name;
                }
            }
            
            if (User.IsInRole("Developer") && previousValue.Assignee != null && !previousValue.Assignee.UserName.Equals(User.Identity.Name))
            {
                return RedirectToAction(nameof(Index));
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    Models.User user = null;
                    if (!User.IsInRole("Developer"))
                    {
                        if (model.AssigneeId != null && model.AssigneeId != "" && !model.AssigneeId.Equals("unassign"))
                        {
                            int count = _context.Task.Include("Assignee").Where(t => t.Assignee.Id.Equals(selectedUserId) && !t.TaskId.Equals(id)).Count();
                            if ((role.Equals("Developer") && count < 3) || !role.Equals("Developer"))
                            {
                                user = await _userManager.FindByIdAsync(model.AssigneeId);
                                previousValue.Assignee = user;
                            }
                            else
                            {
                                throw new Exception("You cannot assign more than 3 tasks to a Developer.");
                            }
                        }
                    }
                        Models.Task task = _context.Task.Include("Assignee").Where(t => t.TaskId == id).FirstOrDefault();

                    if (User.IsInRole("Administrator")) {
                        task.Progress = model.Progress;
                        task.Deadline = model.Deadline;
                        task.Description = model.Description;
                        if (model.Status != null)
                        {
                            task.Status = await _context.Status.Where(s => s.Id.Equals(model.Status.Id)).FirstOrDefaultAsync();
                        }
                        task.Assignee = user;
                    }
                    else if ((User.IsInRole("ProjectManager") && previousValue.Assignee != null)) {
                        if (previousValue.Assignee.UserName.Equals(User.Identity.Name))
                        {
                            task.Progress = model.Progress;
                            task.Deadline = model.Deadline;
                            task.Description = model.Description;
                            if (model.Status != null)
                            {
                                task.Status = await _context.Status.Where(s => s.Id.Equals(model.Status.Id)).FirstOrDefaultAsync();
                            }
                        }
                        task.Assignee = user;
                    }
                    else if (User.IsInRole("Developer"))
                    {
                        task.Progress = model.Progress;
                        task.Description = model.Description;
                        if (model.Status != null)
                        {
                            task.Status = await _context.Status.Where(s => s.Id.Equals(model.Status.Id)).FirstOrDefaultAsync();
                        }
                    } else
                    {
                        task.Assignee = user;
                    }



                    _context.Update(task);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["NotEditedMessage"] = "Error editing task with id: " + id;
                        return await Edit(id);
                    }
                }
                catch (Exception e)
                {
                    if (e.Message.Equals("You cannot assign more than 3 tasks to a Developer."))
                    {
                        TempData["NotEditedMessage"] = e.Message;
                        return await Edit(id);
                    }
                    else
                    {
                        TempData["NotEditedMessage"] = "Error editing task";
                        return await Edit(id);
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task.Include(t => t.Project).Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                ViewData["ErrorMessage"] = "Task was not successfully deleted";
                return View(nameof(Delete));
            }

            return View(task);
        }

        [Authorize(Roles = "Administrator")]
        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Task.FindAsync(id);
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.TaskId == id);
        }

        [Authorize(Roles = "Administrator,ProjectManager")]
        public async Task<IActionResult> AssignedTasks()
        {
            var user = await _userManager.GetUserAsync(User);
            var tasks = _context.Task
                .Include(t => t.Assignee)
                .Include(t => t.Project)
                .Include(t => t.Status)
                .Where(t => t.Assignee == user);
            return View(tasks);
        }


    }
}

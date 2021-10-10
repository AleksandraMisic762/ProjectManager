using System;
using System.Collections.Generic;
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

namespace ProjectManager
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;

        public TasksController(ApplicationDbContext context, UserManager<Models.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Administrator"))
            {
                return View(await _context.Task.Include(t => t.Project).Include(t => t.Status).ToListAsync());
            }
            else
            {
                return View(await _context.Task.Include(t => t.Project).Include(t => t.Status).Where(t => t.Assignee.UserName.Equals(User.Identity.Name)).ToListAsync());
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
            TaskProjectStatusViewModel model = new TaskProjectStatusViewModel
            {
                Projects = _context.Project.ToList(),
                Statuses = _context.Status.Select(s => s.Name),
                Task = new Models.Task()
            };
            ViewBag.Users = _context.Users.ToList();
            return View(model);
        }

        [Authorize(Roles = "Administrator,ProjectManager")]
        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Task")]TaskProjectStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = _context.Project.Find(model.Task.Project.ProjectCode);
                model.Task.Project = project;
                var status = _context.Status.Where(s => s.Name == model.Task.Status.Name).FirstOrDefault();
                model.Task.Status = status;
                if(model.Task.Assignee != null && model.Task.Assignee.Id != null)
                {
                    var assignee = _userManager.FindByIdAsync(model.Task.Assignee.Id).Result;
                    model.Task.Assignee = assignee;
                } else
                {
                    model.Task.Assignee = null;
                }
                _context.Add(model.Task);
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

            var task = await _context.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            TaskProjectStatusUserViewModel model = new TaskProjectStatusUserViewModel
            {
                Users = _userManager.GetUsersInRoleAsync("Administrator").Result
                .Concat(_userManager.GetUsersInRoleAsync("ProjectManager").Result)
                .Concat(_userManager.GetUsersInRoleAsync("Developer").Result),
                Statuses = _context.Status.Select(s => s.Name),
                Task = task
            };
            
            return View(model);
        }

        [Authorize]
        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskProjectStatusUserViewModel model)
        {
            if (id != model.Task.TaskId)
            {
                return NotFound();
            }
            
            var selectedUserId = "";
            var role = "";

            if (!User.IsInRole("Developer"))
            {
                if(model.Task.Assignee.Id == null || model.Task.Assignee.Id == "")
                {
                    model.Task.Assignee = null;
                } else
                {
                    selectedUserId = _context.Users.AsNoTracking().Where(u => u.Id == model.Task.Assignee.Id).FirstOrDefault().Id;
                    var roleId = _context.UserRoles.AsNoTracking()
                            .Where(ur => ur.UserId.Equals(selectedUserId)).FirstOrDefault().RoleId;

                    role = _context.Roles
                        .Where(r => r.Id.Equals(roleId)).FirstOrDefault().Name;
                }
            }
            var previousValue = _context.Task.AsNoTracking().Include("Assignee").Include("Project").Where(t => t.TaskId.Equals(id)).FirstOrDefault();

            if (!previousValue.Assignee.UserName.Equals(User.Identity.Name))
            {
                return RedirectToAction(nameof(Index));
            }

            if (User.IsInRole("Developer"))
            {
                //status, progress, description
                model.Task.Project = previousValue.Project;
                model.Task.Deadline = previousValue.Deadline;
                model.Task.Assignee = previousValue.Assignee;
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    var status = _context.Status.Where(s => s.Name == model.Task.Status.Name).FirstOrDefault();
                    model.Task.Status = status;
                    if (!User.IsInRole("Developer"))
                    {
                        if (model.Task.Assignee != null && model.Task.Assignee.Id != null && model.Task.Assignee.Id != "")
                        {
                            int count = _context.Task.Include("Assignee").Where(t => t.Assignee.Id.Equals(selectedUserId) && !t.TaskId.Equals(id)).Count();
                            if ((role.Equals("Developer") && count < 3) || !role.Equals("Developer"))
                            {
                                var user = await _userManager.FindByIdAsync(model.Task.Assignee.Id);
                                model.Task.Assignee = user;
                            }
                            else
                            {
                                throw new Exception("You cannot assign more than 3 tasks to a Developer.");
                            }
                        }
                        else
                        {
                            model.Task.Assignee = null;

                        }

                        _context.Update(model.Task);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(model.Task.TaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception e)
                {
                    if (e.Message.Equals("You cannot assign more than 3 tasks to a Developer."))
                    {
                        ViewData["NotEditedMessage"] = e.Message;
                        return await Edit(model.Task.TaskId);
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
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
                return NotFound();
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

        [Authorize(Roles = "Administrator")]
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

using System;
using System.Collections.Generic;
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
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;

        public ProjectsController(ApplicationDbContext context, 
            UserManager<Models.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator,ProjectManager")]
        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Project.ToListAsync());
        }

        [Authorize(Roles = "Administrator,ProjectManager")]
        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _context.Project.Include(p => p.Manager).Where(p => p.ProjectCode == id).FirstOrDefault();

            if (project == null)
            {
                return NotFound();
            }

            var tasks = _context.Task.Include("Status").Include("Assignee").Include("Project").Where(t => t.Project == project);

            ProjectTasksViewModel model = new ProjectTasksViewModel
            {
                Project = project,
                Tasks = tasks,
                ProjectProgress = !tasks.Any()? 0 : tasks.Select(t => t.Progress).Sum() / tasks.Select(t => t.Progress).Count()
            };

            ViewData["Statuses"] = Enum.GetNames(typeof(Models.StatusEnum));

            return View(model);
        }

        [Authorize(Roles = "Administrator,ProjectManager")]
        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewBag.ProjectManagers = _userManager.GetUsersInRoleAsync("ProjectManager").Result;
            return View();
        }

        [Authorize(Roles = "Administrator,ProjectManager")]
        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectCode,ProjectName,Manager")] ProjectManager.Models.Project project)
        {
            if (ModelState.IsValid)
            { 
                 var projectManager = _userManager.FindByIdAsync(project.Manager.Id).Result;
                    project.Manager = projectManager;
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewBag.Users = _userManager.GetUsersInRoleAsync("ProjectManager").Result;
            return View(project);
        }

        [Authorize(Roles = "Administrator")]
        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectCode,ProjectName,Manager")] ProjectManager.Models.Project project)
        {
            if (id != project.ProjectCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (project.Manager != null)
                    {
                        var projectManager = _userManager.FindByNameAsync(project.Manager.UserName).Result;
                        project.Manager = projectManager;
                    }
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.ProjectCode == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [Authorize(Roles = "Administrator")]
        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectCode == id);
        }
    }
}

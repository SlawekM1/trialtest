using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace trialtestr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // DELETE: api/projects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound(new { message = "Project not found" });
            }

            var tasks = await _context.Tasks.Where(t => t.ProjectId == id).ToListAsync();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Tasks.RemoveRange(tasks);
                    _context.Projects.Remove(project);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return NoContent();
                }
                catch
                {
                    // Error handling
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { message = "Error deleting project and tasks" });
                }
            }
        }
    }
}
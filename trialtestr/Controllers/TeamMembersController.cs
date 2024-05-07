using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace trialtestr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamMembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeamMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/team_members/{id}/tasks
        [HttpGet("{id}/tasks")]
        public async Task<ActionResult> GetTeamMemberTasks(int id)
        {
            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound(new { message = "Team member not found" });
            }

            var tasks = await _context.Tasks
                .Where(t => t.AssignedToId == id || t.CreatorId == id)
                .Include(t => t.Project)
                .Include(t => t.TaskType)
                .OrderByDescending(t => t.Deadline)
                .ToListAsync();

            return Ok(new { TeamMember = teamMember, Tasks = tasks });
        }
    }
}
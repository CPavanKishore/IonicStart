using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using IonicStart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IonicStart.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {
        private readonly ionicContext _context;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<IndexController> _logger;

        public IndexController(ILogger<IndexController> logger, ionicContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> Get()
        {
            return await _context.Registration.ToListAsync();
        }

        [HttpGet("byEmail")]
        public async Task<ActionResult<IEnumerable>> Get(string email, string password)
        {
            return await _context.Registration.Where(o => o.Email == email & o.Password == password).ToListAsync();
        }

        [HttpPost]
        public async Task<Registration> Post([FromBody]Registration reg)
        {
            _context.Registration.Add(reg);
            await _context.SaveChangesAsync();
            return reg;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuebecAdventures.Domain.Entities;
using QuebecAdventures.Infrastructure.Data.DbContext;

namespace QuebecAdventures.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
		private readonly QuebecAdventuresDbContext _db;

		public ActivitiesController(QuebecAdventuresDbContext db)
		{
			_db = db;
		}

		[HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetAllAsync()
        {
			var activities = await _db.Activities.ToListAsync();
			return Ok(activities);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Activity>> GetById(string id)
		{
			var activity = await _db.Activities.FindAsync(id);
			if (activity == null) return NotFound();
			return Ok(activity);
		}
	}
}

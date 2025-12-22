using Microsoft.AspNetCore.Mvc;
using QuebecAdventures.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace QuebecAdventures.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        // Pour la démo, on utilise une liste statique. À remplacer par un service/injection de dépendances.
        private static List<Activity> activities = new List<Activity>();

        [HttpGet]
        public ActionResult<IEnumerable<Activity>> GetAll()
        {
            return Ok(activities);
        }

        [HttpGet("{id}")]
        public ActionResult<Activity> GetById(string id)
        {
            var activity = activities.FirstOrDefault(a => a.Id == id);
            if (activity == null) return NotFound();
            return Ok(activity);
        }

        [HttpPost]
        public ActionResult<Activity> Create(Activity activity)
        {
            activity.Id = System.Guid.NewGuid().ToString();
            activities.Add(activity);
            return CreatedAtAction(nameof(GetById), new { id = activity.Id }, activity);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Activity updated)
        {
            var activity = activities.FirstOrDefault(a => a.Id == id);
            if (activity == null) return NotFound();
            activities.Remove(activity);
            updated.Id = id;
            activities.Add(updated);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var activity = activities.FirstOrDefault(a => a.Id == id);
            if (activity == null) return NotFound();
            activities.Remove(activity);
            return NoContent();
        }
    }
}

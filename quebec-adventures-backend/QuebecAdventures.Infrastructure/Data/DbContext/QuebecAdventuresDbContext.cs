using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuebecAdventures.Domain.Entities;

namespace QuebecAdventures.Infrastructure.Data.DbContext
{
	public class QuebecAdventuresDbContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public QuebecAdventuresDbContext(DbContextOptions<QuebecAdventuresDbContext> options) : base(options) { }

		public DbSet<Activity> Activities => Set<Activity>();
	}
}

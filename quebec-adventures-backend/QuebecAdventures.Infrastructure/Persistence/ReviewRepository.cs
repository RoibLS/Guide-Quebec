using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using QuebecAdventures.Domain.Entities;
using QuebecAdventures.Domain.Interfaces;

namespace QuebecAdventures.Infrastructure.Persistence
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context) { }
        public async Task AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using QuebecAdventures.Application.Dto;
using QuebecAdventures.Domain.Entities;

namespace QuebecAdventures.Application.Interfaces
{
    public interface IReviewService
    {
        public Task<Review> AddReviewAsync(Guid id, CreateReviewDto reviewDto);
    }
}

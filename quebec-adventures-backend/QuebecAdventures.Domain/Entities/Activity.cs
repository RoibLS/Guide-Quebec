using System;
using System.Collections.Generic;

namespace QuebecAdventures.Domain.Entities
{
    public class Activity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public List<string> Season { get; set; }
        public string Duration { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public int DistanceFromMontreal { get; set; }
        public double Rating { get; set; }
        public bool IsFavorite { get; set; }
        public List<string> Reviews { get; set; }
        public List<string> Images { get; set; }
        public string CoverImage { get; set; }
        public string PriceRange { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public List<string> Tags { get; set; }
        public string? Difficulty { get; set; }
    }
}

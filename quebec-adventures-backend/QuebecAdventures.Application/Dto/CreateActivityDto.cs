using System.ComponentModel.DataAnnotations;
using QuebecAdventures.Domain.Enums;

namespace QuebecAdventures.Application.Dto;

public class CreateActivityDto
{
	[Required] public string Title { get; set; } = string.Empty!;
	public string Description { get; set; } = string.Empty;

	public ActivityType Type { get; set; }
	public Region Region { get; set; }
	public PriceRange? PriceRange { get; set; }
	public Difficulty? Difficulty { get; set; }

	public string City { get; set; } = string.Empty;
	public int DistanceFromMontreal { get; set; }


	public List<string> Season { get; set; } = [];
	public string Duration { get; set; } = string.Empty;

	public double Rating { get; set; }
	public string CoverImage { get; set; } = string.Empty;
	public List<string> Images { get; set; } = [];
	public List<string> Tags { get; set; } = [];

	public string? Website { get; set; }
}

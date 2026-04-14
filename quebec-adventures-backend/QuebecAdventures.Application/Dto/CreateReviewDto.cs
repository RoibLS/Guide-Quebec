using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuebecAdventures.Application.Dto
{
	public class CreateReviewDto
	{
		[Required]
        public string UserId { get; set; } = string.Empty;

        [Range(1.0, 10.0)]
        public double Rating { get; set; }

		[Required]
		public string Comment { get; set; } = string.Empty;
	}
}

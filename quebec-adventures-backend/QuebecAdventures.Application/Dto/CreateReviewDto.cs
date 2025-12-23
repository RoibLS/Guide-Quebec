using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuebecAdventures.Application.Dto
{
	public class CreateReviewDto
	{
		[Required]
		public string UserName { get; set; } = string.Empty;

		[Range(1, 10)]
		public int Rating { get; set; }

		[Required]
		public string Comment { get; set; } = string.Empty;
	}
}

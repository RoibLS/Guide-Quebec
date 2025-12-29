using System;
using System.Collections.Generic;
using System.Text;

namespace QuebecAdventures.Application.Interfaces
{
	public interface IImageService
	{
		Task<string> UploadImageAsync(IFormFile file, string folderName);
		void DeleteImage(string imageUrl);

	}
}

using Microsoft.AspNetCore.Http;

namespace QuebecAdventures.Application.Interfaces
{
	public interface IImageService
	{
		Task<string> UploadImageAsync(IFormFile file, string folderName);
		void DeleteImage(string imageUrl);
	}
}

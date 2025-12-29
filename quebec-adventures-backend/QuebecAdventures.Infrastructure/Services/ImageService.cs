using Microsoft.AspNetCore.Http;
using QuebecAdventures.Application.Interfaces;

namespace QuebecAdventures.Infrastructure.Services
{
	// QuebecAdventures.Infrastructure/Services/ImageService.cs
	public class ImageService : IImageService
	{
		private readonly IWebHostEnvironment _environment;

		public ImageService(IWebHostEnvironment environment)
		{
			_environment = environment;
		}

		public async Task<string> UploadImageAsync(IFormFile file, string folderName)
		{
			if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Fichier invalide");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
			var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

			if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Format de fichier non supporté");
            }

            //Préparation des dossiers
            var uploadsRoot = Path.Combine(_environment.WebRootPath, "images", folderName);
			if (!Directory.Exists(uploadsRoot))
				Directory.CreateDirectory(uploadsRoot);

			// 3. Génération nom unique
			var fileName = $"{Guid.NewGuid()}{extension}";
			var filePath = Path.Combine(uploadsRoot, fileName);

			// 4. Sauvegarde
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			// 5. Retourne l'URL relative
			return $"/images/{folderName}/{fileName}";
		}

		public void DeleteImage(string imageUrl)
		{
			// Logique pour supprimer l'ancien fichier si besoin
			// ...
		}
	}

}

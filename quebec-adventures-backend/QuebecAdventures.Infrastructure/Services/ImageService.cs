using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using QuebecAdventures.Application.Interfaces;

namespace QuebecAdventures.Infrastructure.Services
{
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
                throw new ArgumentException("Le fichier est vide ou null.");

            // 1. Définir le chemin racine (ex: wwwroot/images/activities)
            var uploadsRoot = Path.Combine(_environment.WebRootPath, "images", folderName);

            // 2. Créer le dossier s'il n'existe pas
            if (!Directory.Exists(uploadsRoot))
            {
                Directory.CreateDirectory(uploadsRoot);
            }

            // 3. Générer un nom unique
            var extension = Path.GetExtension(file.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsRoot, uniqueFileName);

            // 4. Sauvegarder
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 5. Retourner le chemin relatif pour l'URL
            // Note: On utilise des slashes '/' pour les URL web
            return $"/images/{folderName}/{uniqueFileName}";
        }

        public void DeleteImage(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return;

            // Conversion URL relative -> Chemin physique
            // ex: /images/activities/abc.jpg -> C:\...\wwwroot\images\activities\abc.jpg
            var filePath = Path.Combine(_environment.WebRootPath, imageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}

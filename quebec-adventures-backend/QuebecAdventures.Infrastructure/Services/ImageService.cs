using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using QuebecAdventures.Application.Interfaces;

namespace QuebecAdventures.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        // Plus besoin de IWebHostEnvironment car on ne stocke pas sur disque
        public ImageService()
        {
        }

        public async Task<(byte[] Content, string ContentType)> ProcessImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Le fichier est vide ou null.");

            // Validation basique du type
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };
            if (!allowedTypes.Contains(file.ContentType))
                throw new ArgumentException($"Type de fichier non supporté: {file.ContentType}");

            // Conversion en tableau d'octets (byte[])
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return (memoryStream.ToArray(), file.ContentType);
            }
        }
    }
}

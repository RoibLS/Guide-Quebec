using Microsoft.AspNetCore.Http;

namespace QuebecAdventures.Application.Interfaces
{
    public interface IImageService
    {
        // Maintenant on retourne un tuple (bytes, contentType)
        // Plus besoin de "folderName" car on ne stocke pas sur disque
        Task<(byte[] Content, string ContentType)> ProcessImageAsync(IFormFile file);
    }
}

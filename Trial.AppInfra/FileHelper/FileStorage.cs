using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Trial.DomainLogic.ResponsesSec;

namespace Trial.AppInfra.FileHelper;

public class FileStorage : IFileStorage
{
    private readonly AzureSetting _azureOption;

    public FileStorage(IOptions<AzureSetting> azureOption)
    {
        _azureOption = azureOption.Value;
    }

    //Para Manejo de Imagenes en AZURE Containers

    public async Task RemoveFileAsync(string path, string containerName)
    {
        var client = new BlobContainerClient(_azureOption.AzureStorage, containerName);
        await client.CreateIfNotExistsAsync();
        var fileName = Path.GetFileName(path);
        var blob = client.GetBlobClient(fileName);
        await blob.DeleteIfExistsAsync();
    }

    public async Task<string> SaveFileAsync(byte[] content, string fileName, string containerName)
    {
        var client = new BlobContainerClient(_azureOption.AzureStorage, containerName);
        await client.CreateIfNotExistsAsync();
        //client.SetAccessPolicy(PublicAccessType.Blob);
        await client.SetAccessPolicyAsync(PublicAccessType.None);

        var blob = client.GetBlobClient(fileName);

        using (var ms = new MemoryStream(content))
        {
            await blob.UploadAsync(ms, overwrite: true);
        }
        //Es para obtener la url completa junto con el archivo
        //return blob.Uri.ToString();
        return fileName;
    }

    public async Task<string> SaveWordFileAsync(byte[] content, string fileName, string containerName)
    {
        // Validación por extensión
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        var validExts = new[] { ".doc", ".docx" };

        if (!validExts.Contains(ext))
            throw new InvalidOperationException("Solo se permiten archivos Word (.doc, .docx)");

        var client = new BlobContainerClient(_azureOption.AzureStorage, containerName);
        await client.CreateIfNotExistsAsync();
        await client.SetAccessPolicyAsync(PublicAccessType.None); //Privado

        var blob = client.GetBlobClient(fileName);

        using (var ms = new MemoryStream(content))
        {
            await blob.UploadAsync(ms, overwrite: true);
        }

        return fileName;
    }

    //Para poder leer el Blobs que esta en Privado
    public async Task<string?> GetImageBase64Async(string? fileName, string containerName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return null;

        var client = new BlobContainerClient(_azureOption.AzureStorage, containerName);
        var blob = client.GetBlobClient(fileName);

        if (!await blob.ExistsAsync())
            return null;

        var download = await blob.DownloadContentAsync();
        var bytes = download.Value.Content.ToArray();
        var mime = GetMimeType(fileName);

        return $"data:{mime};base64,{Convert.ToBase64String(bytes)}";
    }

    private static string GetMimeType(string fileName)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return ext switch
        {
            ".png" => "image/png",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            _ => "application/octet-stream"
        };
    }




    //Para Guardado de Imagenes en Disco
    //Solo pra alamcenamiento local o uso de IFormFile
    public async Task<string> UploadImage(IFormFile imageFile, string ruta, string guid)
    {
        var file = guid;
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            ruta,
            file);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return $"{file}";
    }

    public async Task<string> UploadImage(byte[] imageFile, string ruta, string guid)
    {
        var file = guid;
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            ruta,
            file);

        var NIformFile = new MemoryStream(imageFile);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await NIformFile.CopyToAsync(stream);
        }

        return $"{file}";
    }

    public bool DeleteImage(string ruta, string guid)
    {
        string path;
        path = Path.Combine(
            Directory.GetCurrentDirectory(),
            ruta,
            guid);

        File.Delete(path);

        return true;
    }
}
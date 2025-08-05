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
        client.SetAccessPolicy(PublicAccessType.Blob);

        var blob = client.GetBlobClient(fileName);

        using (var ms = new MemoryStream(content))
        {
            await blob.UploadAsync(ms, overwrite: true);
        }
        //Es para obtener la url completa junto con el archivo
        //return blob.Uri.ToString();
        return fileName;
    }

    //Para Guardado de Imagenes en Disco

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
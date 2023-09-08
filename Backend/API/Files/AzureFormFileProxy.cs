using Azure.Storage.Blobs;
using Core.IUtils;
using static Infrastructure.Enums.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace API.Files
{
    public class AzureFormFileProxy : IFileProxy
    {
        const int MaxFileSize = 5242880;
        const string allowedExt = ".png.jpg.jpeg";
        const string _connectionString = "DefaultEndpointsProtocol=https;AccountName=watten;AccountKey=QoBfH4xb55M6geeaUlYplZl8dA+HY0SUjSAxRw1m/oO7Lg01bYSWqlbcbZVxQi92F2YsZyTEeQgg+AStzLZB9A==;EndpointSuffix=core.windows.net";
        const string _containerName = "watten";
        const string _baseURL = "https://azure.com";
        private IFormFile _file;
        private string _ext;


        public AzureFormFileProxy(IFormFile file)
        {
            _file = file;
            _ext = Path.GetExtension(_file.FileName);
            Validate();
        }


        private void Validate()
        {
            long fileSize = _file.Length;

            if (fileSize > MaxFileSize || !allowedExt.Contains(_ext))
                throw new Exception("Invalid File");
        }


        async Task<string> UploadAsync(string fileName)
        {
            BlobServiceClient client = new BlobServiceClient(_connectionString);

            // Get a reference to the container
            BlobContainerClient containerClient = client.GetBlobContainerClient(_containerName);

            //Get a reference to the blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            await using (Stream? data = _file.OpenReadStream())
            {
                await blobClient.UploadAsync(data, true);
                return blobClient.Uri.AbsoluteUri;
            }
        }


        public async Task<string> SaveFile(string fileName, Folder folder = Folder.Default)
        {
            string uri = await UploadAsync(fileName + _ext);
            return uri;
        }
    }
}

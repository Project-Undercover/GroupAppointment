using Azure.Storage.Blobs;
using Core.IUtils;
using static Infrastructure.Enums.Enums;

namespace API.Files
{
    public class AzureFormFileProxy : IFileProxy
    {
        private IFormFile _file;
        const int MaxFileSize = 5242880;
        const string allowedExt = ".png.jpg.jpeg";
        const string _connectionString = "";
        const string _containerName = "upl";
        const string _baseURL = "https://azure.com";


        public AzureFormFileProxy(IFormFile file)
        {
            _file = file;
            Validate();
        }


        private void Validate()
        {
            string ext = Path.GetExtension(_file.FileName);
            long fileSize = _file.Length;

            if (fileSize > MaxFileSize || !allowedExt.Contains(ext))
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
            string uri = await UploadAsync(fileName);
            return uri;
        }
    }
}

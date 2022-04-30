using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PostImagenesAzure.Services
{
    public class ServiceStorageBlobs
    {
        private BlobServiceClient client;
        protected string Pokemon = "pokemon";

        public ServiceStorageBlobs(BlobServiceClient client)
        {
            this.client = client;
        }

        public async Task DeleteBlobAsync(string containerName, string blobName)
        {
            BlobContainerClient folder = this.client.GetBlobContainerClient(containerName);
            await folder.DeleteBlobAsync(blobName);
        }

        public async Task<List<string>> GetBlobsAsync(string containerName)
        {
            BlobContainerClient folder = this.client.GetBlobContainerClient(containerName);
            List<string> imagenes = new List<string>();
            await foreach (BlobItem images in folder.GetBlobsAsync())
            {
                imagenes.Add(images.Name);
            }
            return imagenes;
        }

        public async Task UploadBlobAsync(string containerName, string blobName, Stream stream)
        {
            BlobContainerClient containerClient = this.client.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(stream, overwrite: true);
        }
    }
}

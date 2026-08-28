using Oci.Common.Auth;
using Oci.Common;
using Oci.ObjectstorageService.Models;
using Oci.ObjectstorageService.Requests;
using Oci.ObjectstorageService.Responses;
using Oci.ObjectstorageService;
using System.Threading.Tasks;
using System.IO;
using System;

namespace diof_api
{
    public class ObjectStorage
    {
        private ObjectStorageClient Client { get; set; }

        public ObjectStorage()
        {
            var config = Path.Combine(Environment.CurrentDirectory, "oci/config");

            var provider = new ConfigFileAuthenticationDetailsProvider(config, "DEFAULT");

            var configuration = new ClientConfiguration
            {
                ClientUserAgent = "sai.imap.org.br",
                //TimeoutMillis = 1000,
            };

            Client = new ObjectStorageClient(provider, configuration);
        }

        public Task<GetObjectResponse> GetObject(string bucket_name, string @namespace, string object_name)
        {
            var request = new GetObjectRequest()
            {
                BucketName = bucket_name,
                NamespaceName = @namespace,
                ObjectName = object_name
            };

            return Client.GetObject(request);
        }

        public PutObjectResponse PutObject(string bucket_name, string @namespace, string object_name, byte[] file)
        {
            var body = new MemoryStream(file);

            var request = new PutObjectRequest()
            {
                BucketName = bucket_name,
                NamespaceName = @namespace,
                ObjectName = object_name,
                PutObjectBody = body,
                StorageTier = StorageTier.Standard
            };

            var response = Client.PutObject(request);

            return response.Result;
        }
    }
}

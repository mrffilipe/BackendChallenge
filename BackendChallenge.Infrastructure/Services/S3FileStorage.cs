using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using BackendChallenge.Application.Services;
using Microsoft.Extensions.Configuration;

namespace BackendChallenge.Infrastructure.Services
{
    public sealed class S3FileStorage : IFileStorage
    {
        private readonly IAmazonS3 _s3;
        private readonly string _bucket;
        private readonly string _prefix;

        public S3FileStorage(IConfiguration cfg)
        {
            var region = RegionEndpoint.GetBySystemName(cfg["AWS:Region"] ?? cfg["AWS_REGION"]);
            var serviceUrl = cfg["Storage:S3:ServiceURL"] ?? cfg["S3_SERVICE_URL"];
            var forcePath = bool.TryParse(cfg["Storage:S3:ForcePathStyle"] ?? cfg["S3_FORCE_PATH_STYLE"], out var fps) && fps;

            var s3Config = new AmazonS3Config { RegionEndpoint = region };
            if (!string.IsNullOrWhiteSpace(serviceUrl)) { s3Config.ServiceURL = serviceUrl; s3Config.ForcePathStyle = forcePath; }

            _s3 = new AmazonS3Client(
                cfg["AWS:AccessKeyId"] ?? cfg["AWS_ACCESS_KEY_ID"],
                cfg["AWS:SecretAccessKey"] ?? cfg["AWS_SECRET_ACCESS_KEY"],
                s3Config);

            _bucket = cfg["Storage:S3:BucketName"] ?? cfg["S3_BUCKET_NAME"]!;
            _prefix = cfg["Storage:S3:Prefix"] ?? cfg["S3_BUCKET_PREFIX"] ?? string.Empty;
        }

        public async Task<string> PutAsync(string objectKey, Stream content, string contentType, CancellationToken ct = default)
        {
            var key = string.IsNullOrEmpty(_prefix) ? objectKey : $"{_prefix.TrimEnd('/')}/{objectKey}";

            var req = new PutObjectRequest
            {
                BucketName = _bucket,
                Key = key,
                InputStream = content,
                ContentType = contentType
            };

            var resp = await _s3.PutObjectAsync(req, ct);
            return key;
        }
    }
}

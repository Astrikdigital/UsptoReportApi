  
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;



namespace Converge.Shared.Helper
{
    public static class Helper
    {
        public static string GenerateEntityId(int EntityId)
        {
            var year = DateTime.Now.Year % 100;
            var randomDigits = new Random().Next(1000, 9999);
            return EntityId == (int)EnumHelper.Role.Faculty
                ? $"CF{year}{randomDigits}"
                : EntityId == (int)EnumHelper.Role.Student
                ? $"CS{year}{randomDigits}"
                : "";
        }

        //public static async Task<dynamic> AttachFileToS3Async(string filepath, IConfiguration configuration, string pdfFileName, string BrandCode, int BrandId)
        //{
        //    if (filepath == null || filepath.Length == 0)
        //        return (null);

        //    try
        //    {
        //        var bucketName = configuration["AAWS:BucketName"];
        //        var accessKey = configuration["AAWS:AccessKey"];
        //        var secretKey = configuration["AAWS:SecretKey"];
        //        var region = configuration["AAWS:Region"];
        //        using var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast1);
        //        using var transferUtility = new TransferUtility(s3Client);


        //        using (var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        //        {
        //            var uploadRequest = new TransferUtilityUploadRequest
        //            {
        //                InputStream = fileStream,
        //                BucketName = bucketName,
        //                Key = $"Pdf/{BrandCode + "-" + BrandId}/{pdfFileName}",
        //                ContentType = "application/pdf"
        //            };
        //            await transferUtility.UploadAsync(uploadRequest);
        //        }
        //        var fileUrl = $"https://{bucketName}.s3.{region}.amazonaws.com/Pdf/{BrandCode + "-" + BrandId}/{pdfFileName}";
        //        return (fileUrl);
        //    }
        //    catch (Exception ex)
        //    {
        //        return (null);
        //    }
        //}

    }
}

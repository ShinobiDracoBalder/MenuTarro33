namespace MenuTarro33.Web.Helpers
{
    public interface IImageVideoHelper
    {
        Task<byte[]> UploadImageArrayAsync(IFormFile imageFile);
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
        string UploadImage(byte[] pictureArray, string folder);
        Task<string> UploadVideoAsync(IFormFile imageFile, string folder);
    }
}

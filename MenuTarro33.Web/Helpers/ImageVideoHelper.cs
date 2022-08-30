namespace MenuTarro33.Web.Helpers
{
    public class ImageVideoHelper : IImageVideoHelper
    {
        public string UploadImage(byte[] pictureArray, string folder)
        {
            MemoryStream stream = new MemoryStream(pictureArray);
            string guid = Guid.NewGuid().ToString();
            string file = $"{guid}.png";

            try
            {
                stream.Position = 0;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{folder}", file);
                File.WriteAllBytes(path, stream.ToArray());
            }
            catch
            {
                return string.Empty;
            }

            return $"~/images/{folder}/{file}";
        }

        public async Task<byte[]> UploadImageArrayAsync(IFormFile imageFile)
        {
            MemoryStream ms = new MemoryStream();
            await imageFile.OpenReadStream().CopyToAsync(ms);
            byte[] bytes = ms.ToArray();
            return bytes;
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile, string folder)
        {
            string guid = Guid.NewGuid().ToString();
            string file = $"{guid}.png";
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot\\Images\\{folder}",
                file);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"~/Images/{folder}/{file}";
        }

        public async Task<string> UploadVideoAsync(IFormFile imageFile, string folder)
        {
            string guid = Guid.NewGuid().ToString();
            string file = $"{guid}.mp4";
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot\\videos\\{folder}",
                file);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"~/videos/{folder}/{file}";
        }
    }
}

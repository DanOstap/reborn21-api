namespace Reborn.Services
{

    public interface IFileService
    {
        string Upload(IFormFile file);
        void Get(string filename);
    }

    public class FileService : IFileService
    {
        public string Upload(IFormFile file)
        {
            List<string> validExtentions = new List<string>() { ".jpg", ".png", ".webp" };

            string extention = Path.GetExtension(file.FileName);

            if (!validExtentions.Contains(extention))
            {
                throw new Exception("Extention is not valid ");
            }

            string fileName = Guid.NewGuid().ToString() + extention;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/");

            FileStream stream = new FileStream(path + fileName, FileMode.Create);
            file.CopyTo(stream);
            stream.Dispose();
            stream.Close();

            return fileName;
        }

        public void Get(string filename)
        {
            
        }
    }
}

namespace API.Helpers;

public class FileUploader
{
    public static async Task<string> UploadAsync(IFormFile file,string relativePath, string path)
    {
        string filePath;
        string result;

        if (file != null && file.Length > 0)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid()+ fileExtension;
            result = relativePath + "/" + fileName;
            filePath = Path.Combine(path, fileName);
            var pa = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

            using (var fileStream = new FileStream(pa, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return result;
        }

        return null;
    }
}

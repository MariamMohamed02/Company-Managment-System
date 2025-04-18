namespace Company.Project.PresentationLayer.Helpers
{
    public class DocumentSettings
    {
       // 1. upload
       // 2. delete

        public static string UploadFile(IFormFile file, string foldername)
        {
            //1. Get folder location
            
            var folderPath= Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", foldername);

            //2. File name  (make sure it is unique thereofre make it unique)

            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            var filePath= Path.Combine(folderPath, fileName);
            var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            var filePath=Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName, fileName);
            if (File.Exists(filePath)) { 
                File.Delete(filePath);
            }

        }

    }
}

namespace Company.MVC.Demo.Helpers
{
    public class DocumentSettings
    {
        // 1. Upload
        public static string UploadFile (IFormFile myFile, string myFolderName) // target saving location we created
        {
            // 1. Get the path for saving location
            // string folderPath = Directory.GetCurrentDirectory() + @"wwwroot\files\" + folderName;
            string myFolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files\", myFolderName);

            // 2. Get file name and make it unique
            // the generated Guid will ensure unique name
            string myFileName = $"{Guid.NewGuid()}{myFile.FileName}";

            // 3. Combine both folderName + file Name = filePath, sequence is relevant
            string myFilePath = Path.Combine(myFolderPath, myFileName);

            // 4. Save the file
            // We use what's called a stream = data + time
            // using to open and close the connection
            using var fileStream = new FileStream(myFilePath, FileMode.Create);
            myFile.CopyTo(fileStream); // save the file to the stream

            return myFileName;
        }

        // 2. Delete
        public static void DeleteFile(string myFileName, string myFolderName)
        {
            string myFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files\", myFolderName, myFileName);
            if (File.Exists(myFilePath))
            {
                File.Delete(myFilePath);
            }
        }
    }
}

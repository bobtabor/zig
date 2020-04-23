using System;
using System.IO;
using System.Text;

public class FileGenerator {
    IHtmlBuilder _htmlBuilder;

    public FileGenerator(IHtmlBuilder htmlBuilder){
        this._htmlBuilder = htmlBuilder;
    }

    public void DissectContent(string contentPath, string outputDirectory){
            
            var files = Directory.GetFiles(contentPath);      
            var directories = Directory.GetDirectories(contentPath);   
            var parentFolderSplit = contentPath.Split(Path.DirectorySeparatorChar);
            var parentFolderName = parentFolderSplit[parentFolderSplit.Length-1];

            foreach (var file in files){
                if (!file.EndsWith(".zig")){
                    continue;
                }
                var parentDirectory = Directory.GetParent(file);
                var destination = $"{outputDirectory}/{parentFolderName}";
                var fileName = $"{destination}/{Path.GetFileNameWithoutExtension(file)}.html";
                var newFileContent = _htmlBuilder.BuildHtmlFromConfigFile(file);
                Directory.CreateDirectory(destination);
                using (FileStream fs = File.Create(fileName))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(newFileContent);
                    fs.Write(info, 0, info.Length);
                }
                Console.WriteLine($"Generated: {fileName}");
            }

            foreach (var directory in directories){             
                DissectContent(directory, outputDirectory);
            }
        }
}
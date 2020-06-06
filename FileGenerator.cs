using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
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

        // Using Selenium's API to open chrome and take a screenshot.
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("headless");
        options.AddArgument("--window-size=1920,1080");
        var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);

        foreach (var file in files)
        {
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

            // Generate screenshot
            driver.Navigate().GoToUrl(fileName);
            var screenshot = (driver as ITakesScreenshot).GetScreenshot();
            screenshot.SaveAsFile($"{fileName}.screenshot.png");

        }

        driver.Close();
        driver.Quit();

        foreach (var directory in directories){             
                DissectContent(directory, outputDirectory);
            }
        }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class HtmlFileBuilder{
    public static string BuildHtmlFromConfigFile(string filePath){
        var content = File.ReadAllText(filePath);

        var slideInfoParsed = content.Split("-----");
        var slides = new List<SlideContent>();
        int i = 0;
        foreach (var slideInfo in slideInfoParsed) {                                
            switch (GetSlideName(slideInfo))
            {
                case "title" :
                    slides.Add(new TitleSlide(slideInfo, i));
                    i++;
                    break;
                case "links" :
                    slides.Add(new LinksSlide(slideInfo, i));
                    i++;
                    break;
                case "bullets" :
                    slides.Add(new BulletsSlide(slideInfo, i));
                    i++;
                    break;
                case "code" :
                    slides.Add(new CodeSlide(slideInfo, i));
                    i++;
                    break;
                default:
                    break;
            }                
        }

        var htmlBuilder = new StringBuilder();

        foreach (var slide in slides.OrderBy(x => x.SlideOrder))
        {
            htmlBuilder.AppendLine(slide.PrintSlideHtml());
        }
        return htmlBuilder.ToString();
    }

    private static string GetSlideName(string slideInfo){
            if (String.IsNullOrEmpty(slideInfo))
                return "";
            var lineArray = slideInfo.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return lineArray[0].Trim();
        }  
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public class BulletsSlide : SlideContent
{
    public string Title { get; set; }
    public List<string> Points { get; set; }

    public BulletsSlide(){
        this.Points = new List<string>();
    }

    public BulletsSlide(string title, List<string> points){
        this.Title = title;
        this.Points = points;
    }

    public BulletsSlide(string slideContent, int slideOrder){
        this.SlideOrder = slideOrder;
        var lineArray = slideContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < lineArray.Length; i++) {
            if (lineArray[i].Trim() == "" || lineArray[i].Trim() == "bullets")
                continue;            
            else {
                var property = Regex.Split(lineArray[i], @":\s");
                if (property[0].Trim() == "title")
                    Title = property[1].Trim();
                if (property[0].Trim() == "points" || property[0].Trim() == "points:"){
                    Points = new List<string>();
                    var j = i;
                    var outOfPoints = false;
                    j++;
                    while (!outOfPoints && j < lineArray.Length) {                        
                        var point = Regex.Split(lineArray[j], @"-:\s");                        
                        outOfPoints = point.Length == 1;
                        if (!outOfPoints){
                            Points.Add(point[1]);
                        }
                        j++;
                    }
                    i = j;
                }
            }
        }
    }

    public override string PrintSlideHtml()
    {
        var sb = new StringBuilder();
        sb.AppendLine(@"<section>
					<h2>Fragments</h2>
					<p><small>&nbsp;</small></p>");
        foreach (var point in Points){
            sb.AppendLine($"<p class=\"fragment\">{point}</p>");
        }
        sb.AppendLine("</section>");
        
        return sb.ToString();
    }
}
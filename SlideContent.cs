using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public abstract class SlideContent{
    // public string Name { get; set; }
    // public Dictionary<string, string> Content { get; set; }
    public abstract string PrintSlideHtml();
    public int SlideOrder { get; set; }
    // public SlideContent(string initailContent){
    //     Content = new Dictionary<string, string>();

    //     var lineArray = initailContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
    //     for (int i = 0; i < lineArray.Length; i++) {
    //         if (lineArray[i].Trim() == "")
    //             continue;
    //         if (i == 0){
    //             Name = lineArray[i];
    //         }
    //         else {
    //             var property = Regex.Split(lineArray[i], @":\s");
    //             Console.WriteLine(property[0] + "   " + property[1]);
    //         }
    //     }
    // }
}
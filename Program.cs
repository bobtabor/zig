using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.CommandLineUtils;

namespace zig
{
    class Program
    {
        static void Main(string[] args)
        {
            // var app = new CommandLineApplication();

            //     var directory = Directory.GetCurrentDirectory();
            //     var optionCommand = app.Option("-z|--zigcommand <ZIGCOMMAND>", "The command you want zig to execute", CommandOptionType.SingleValue);

            //     app.OnExecute(() =>
            //     {
            //         var command = optionCommand.HasValue()
            //             ? optionCommand.Value()
            //             : "";                    
            //         return 0;
            //     });

            //  app.Execute(args);
            var content = File.ReadAllText("C:/Users/c2tab/zig/example.zig");

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
            foreach (var slide in slides.OrderBy(x => x.SlideOrder))
            {
                Console.WriteLine(slide.PrintSlideHtml());
            }
            Console.ReadLine();
        }

        private static string GetSlideName(string slideInfo){
            if (String.IsNullOrEmpty(slideInfo))
                return "";
            var lineArray = slideInfo.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return lineArray[0].Trim();
        }        
    }
}

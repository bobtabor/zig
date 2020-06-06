using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class HtmlFileBuilder : IHtmlBuilder {

    public string BuildHtmlFromConfigFile(string filePath){

        Console.WriteLine($"Working on: {filePath}");

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
        
        var dynamicContent = htmlBuilder.ToString();

        return $@"
<!doctype html>
<html>
	<head>
		<meta charset=""utf-8"">
		<meta name=""viewport"" content=""width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"">

		<title>{Path.GetFileNameWithoutExtension(filePath)}</title>

		<link rel=""stylesheet"" href=""../css/reset.css"">
		<link rel=""stylesheet"" href=""../css/reveal.css"">
		<link rel=""stylesheet"" href=""../css/theme/bobtabor.css"">

		<!-- Theme used for syntax highlighting of code -->
		<link rel=""stylesheet"" href=""../lib/css/monokai.css"">

	</head>
	<body>
		<div class=""reveal"">

			<div class=""slides"">
            { dynamicContent }
            </div>
		</div>

		<script src=""../js/reveal.js""></script>

		<script>
			Reveal.initialize({{
				hash: true,
				dependencies: [
					{{ src: '../plugin/markdown/marked.js' }},
					{{ src: '../plugin/markdown/markdown.js' }},
					{{ src: '../plugin/highlight/highlight.js' }},
					{{ src: '../plugin/notes/notes.js', async: true }}
				],
				width: '80%',
				center: false
			}});
		</script>
	</body>
</html>";
    }

    private static string GetSlideName(string slideInfo){
            if (String.IsNullOrEmpty(slideInfo))
                return "";
            var lineArray = slideInfo.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return lineArray[0].Trim();
        }  
}
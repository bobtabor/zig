using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class CodeSlide : SlideContent
{
    public string Title { get; set; }
    public string Highlights { get; set; }
    public string Code { get; set; }

    public CodeSlide() {}

    public CodeSlide(string title, string highlights, string code){
        this.Title = title;
        this.Highlights = highlights;
        this.Code = code;
    }
    
    public CodeSlide(string slideContent, int slideOrder){
        this.SlideOrder = slideOrder;
        var inCode = false;
        var codeBuilder = new StringBuilder();
        var codeSectionCounter = 0;
        var lineArray = slideContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < lineArray.Length; i++) {
            if (lineArray[i].Trim() == "" || lineArray[i].Trim() == "code")
                continue;
            else {
                var property = Regex.Split(lineArray[i], @":\s");
                if (property[0].Trim() == "title")
                    Title = property[1].Trim();
                if (property[0].Trim() == "highlights")
                    Highlights = property[1].Trim();
                if (property[0].Trim() == "code" || property[0].Trim() == "code:"){
                    inCode = true;                                            
                }
                if (inCode){
                    if (lineArray[i] == "```"){
                        codeSectionCounter++;
                    }
                    else {
                        codeBuilder.AppendLine(lineArray[i]);
                    }

                    if (codeSectionCounter == 2)
                        inCode = false;
                }
                    
            }
        }
        Code = codeBuilder.ToString();
    }

    public override string PrintSlideHtml()
    {
        return $@"<section>

					<h2>{Title}</h2>
					<p><small>&nbsp;</small></p>
					<pre><code class=""hljs"" data-trim data-line-numbers=""{Highlights}"">
                    {Code}
					</code></pre>					
				</section>";
    }
}
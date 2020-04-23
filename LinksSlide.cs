using System;
using System.Text.RegularExpressions;

public class LinksSlide : SlideContent
{
    public string CodeUrl { get; set; }
    public string CaptionsUrl { get; set; }

    public LinksSlide() {}

    public LinksSlide(string codeUrl, string captionsUrl){
        this.CodeUrl = codeUrl;
        this.CaptionsUrl = captionsUrl;
    }

    public LinksSlide(string slideContent, int slideOrder){
        this.SlideOrder = slideOrder;
        var lineArray = slideContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < lineArray.Length; i++) {
            if (lineArray[i].Trim() == "" || lineArray[i].Trim() == "links")
                continue;
            else {
                var property = Regex.Split(lineArray[i], @":\s");
                if (property[0].Trim() == "code-url")
                    CodeUrl = property[1].Trim();
                if (property[0].Trim() == "captions-url")
                    CaptionsUrl = property[1].Trim();
            }
        }
    }

    public override string PrintSlideHtml()
    {
        return $@"<section>					
					<h2>Before we get started</h2>
					<p><small>&nbsp;</small></p>
					
					<h4>Clone or download the code:</h4>
					<p class=""indent"">{CodeUrl}</p>

					<p class=""indent""><small>How do I get the code from GitHub?  See: <a href="""">https://bobtabor.com/git</a></small></p>

					<p><small>&nbsp;</small></p>
					<h4>Help me improve closed captions:</h4>
					<p class=""indent"">{CaptionsUrl}</p>
				</section>";
    }
}
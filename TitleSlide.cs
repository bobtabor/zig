using System;
using System.Text.RegularExpressions;

public class TitleSlide : SlideContent
{
    public string CourseTitle { get; set; }
    public string VideoTitle { get; set; }

    public TitleSlide() {}

    public TitleSlide(string courseTitle, string videoTitle){
        this.CourseTitle = courseTitle;
        this.VideoTitle = videoTitle;
    }

    public TitleSlide(string slideContent, int slideOrder){
        this.SlideOrder = slideOrder;
        var lineArray = slideContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < lineArray.Length; i++) {
            if (lineArray[i].Trim() == "" || lineArray[i].Trim() == "title")
                continue;
            else {
                var property = Regex.Split(lineArray[i], @":\s");
                if (property[0].Trim() == "course-title")
                    CourseTitle = property[1].Trim();
                if (property[0].Trim() == "video-title")
                    VideoTitle = property[1].Trim();
            }
        }
    }

    public override string PrintSlideHtml()
    {
        return $@"<section class=""center"">
					<p><small>{CourseTitle}</small></p>
					<h1 style=""line-height: 5rem;"">{VideoTitle}</h1>					
				</section>";
    }
}
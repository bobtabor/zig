using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public abstract class SlideContent{
    public abstract string PrintSlideHtml();
    public int SlideOrder { get; set; }
}
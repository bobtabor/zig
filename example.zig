-----title 
course-title: C# Level 1 - Syntax Basics
video-title: Branch code execution with the if statement

-----links
code-url: http://github.com/bobtabor/csharp-if-elseif-else
captions-url: http://github.com/bobtabor/captions/csharp-if-elseif-else

-----bullets
title: Fragments
points:
- Point 1
- Point 2
- Point 3

-----code 
title: Pretty code
highlights: 4|3,5-6
code:
```
static void Main(string[] args)
{
	Console.WriteLine("Hello Bob!");
	var random = new Random();
	var value = random.Next(10);
	Console.WriteLine(value);
}
```
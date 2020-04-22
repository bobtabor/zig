using System;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;

namespace zig
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();

                var directory = Directory.GetCurrentDirectory();
                // TODO: Parse through and get all file paths of zig files
                // TODO: Generate the final HTML files and put them in the correct locations
                var optionCommand = app.Option("-z|--zigcommand <ZIGCOMMAND>", "The command you want zig to execute", CommandOptionType.SingleValue);

                app.OnExecute(() =>
                {
                    var command = optionCommand.HasValue()
                        ? optionCommand.Value()
                        : "";                    
                    return 0;
                });

             app.Execute(args);
            Console.WriteLine(HtmlFileBuilder.BuildHtmlFromConfigFile("C:/Users/c2tab/zig/example.zig"));
        }              
    }
}

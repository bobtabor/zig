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
            var optionCommand = app.Option("-z|--zigcommand <ZIGCOMMAND>", "The command you want zig to execute", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                var command = optionCommand.HasValue()
                    ? optionCommand.Value()
                    : "";      
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\\---ZIG---\\");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("/---ZAG---/");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\\---GO!---\\");
                Console.ForegroundColor = ConsoleColor.Cyan;
                var directory = Directory.GetCurrentDirectory();
                var directorySource = directory + "/source";
                var fileGenerator = new FileGenerator(new HtmlFileBuilder());
                fileGenerator.DissectContent(directorySource, directory);
                Console.ForegroundColor = ConsoleColor.White;
                return 0;
            });

            app.Execute(args);
        }          
    }
}

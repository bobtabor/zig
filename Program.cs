using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Extensions.CommandLineUtils;

namespace zig
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();            
            var sourceCommand = app.Option("-s|--source <SOURCEDIRECTORY>", "The input directory where you want to read .zig files from", CommandOptionType.SingleValue);
            var destinationCommand = app.Option("-d|--desitination <DESTINATIONDIRECTORY>", "The output directory where you want slides to be saved", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                var sourceDirectory = sourceCommand.HasValue()
                    ? sourceCommand.Value()
                    : "";

                var destinationDirectory = destinationCommand.HasValue()
                    ? destinationCommand.Value()
                    : "";
                

                var directorySource = "";
                var pathFound = false;

                // Check to see if there's a path passed in via the args.
                // If so, use that.  If not, try the current directory.
                // If not, warn and exit.

                if (sourceDirectory.Length > 0)
                {
                    
                    directorySource = sourceDirectory + "\\source";
                    if (Directory.Exists(directorySource))
                    {
                        pathFound = true;
                    }
                }
                
                if (!pathFound)
                {
                    
                    directorySource = Directory.GetCurrentDirectory() + "\\source";
                    if (Directory.Exists(directorySource))
                    {
                        pathFound = true;
                    }
                }

                if (!pathFound)
                {
                    Console.WriteLine($"The directory `{directorySource}` does not exist. Execute zig in a directory containing a \\source sub-directory filled with .zig files.");
                    return 0;
                }

                if (destinationDirectory.Length == 0)
                {
                    // Just use the source directory's `output` directory.
                    destinationDirectory = directorySource + "\\output";
                }

                var fileGenerator = new FileGenerator(new HtmlFileBuilder());

                try
                {
                    fileGenerator.DissectContent(directorySource, destinationDirectory);
                }
                catch (Exception ex)
                {
                    // TODO: Do something with this exception!
                    throw ex;
                }
                
                return 0;
            });

            app.Execute(args);
        }  

    }
}

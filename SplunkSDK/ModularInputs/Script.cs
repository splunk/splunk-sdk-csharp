using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splunk.ModularInputs
{
    public abstract class Script
    {
        public abstract Scheme Scheme {get;}

        public static int Run<T>(string[] args)
            where T : Script, new()
        {
            try
            {
                 var utf8 = new UTF8Encoding();

                // Console default is OEM text encoding, which is not handled by Splunk,
                // resulting in loss of chars such as O with double acute (\u0150)
                // Splunk's default is UTF8.

                // Avoid setting InputEncoding unnecessarily because 
                // it will cause a reset of Console.In 
                // (which should be a System.Console bug), 
                // losing the redirection unit tests depend on.
                if (!(Console.InputEncoding is UTF8Encoding))
                {
                    Console.InputEncoding = utf8;
                }

                // Below will set both stdout and stderr.
                Console.OutputEncoding = utf8;

                var script = new T();

                if (args.Length == 0)
                {
                    Log("Reading input definition");
                    var inputDefinition = InputConfiguration.Read(Console.In);
                    Log("Calling StreamEvents");
                    script.StreamEvents(inputDefinition);
                    return 0;
                }

                if (args[0].ToLower().Equals("--scheme"))
                {
                    if (script.Scheme != null)
                    {
                        Log("Writing introspection streme");
                        Console.WriteLine(script.Scheme.Serialize());
                    }
                    return 0;
                }

                if (args[0].ToLower().Equals("--validate-arguments"))
                {
                    Log("Reading validation items");
                    var validationItems = ValidationItems.Read(Console.In);
                    Log("Calling Validate");
                    script.Validate(validationItems);
                    return 0;
                }

                return 0;
            }
            catch (Exception e)
            {
                Log(string.Format(
                            "Unhandled exception: {0}",
                            e),
                    LogLevel.Fatal);
            }

            // Return code indicating a failure.
            return 1;
        }

        private static void Log(string msg, LogLevel level = LogLevel.Info)
        {
            SystemLogger.Write(level, "Script.Run: " + msg);
        }
        
        public abstract void StreamEvents(InputConfiguration inputConfiguration);

        public virtual void Validate(ValidationItems validationItems)
        {
            throw new NotSupportedException(
                "The override of Module.Validate must be implemented to support Modular Input External Validation.");
        }   
    }
}

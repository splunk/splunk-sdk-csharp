using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splunk.ModularInputs
{
    public abstract class Module
    {
        private string logPrefix;

        public int Dispatch(
            string[] args,
            Scheme scheme)
        {
            this.logPrefix = "Module.Dispatch: ";
          
            try
            {
                if (args.Length == 0)
                {
                    Log("Reading input definition");
                    var inputDefinition = InputConfiguration.Read(Console.In);
                    Log("Calling StreamEvents");
                    StreamEvents(inputDefinition);
                    return 0;
                }

                if (args[0].ToLower().Equals("--scheme"))
                {
                    if (scheme != null)
                    {
                        Log("Writing introspection streme");
                        Console.WriteLine(scheme.Serialize());
                    }
                    return 0;
                }

                if (args[0].ToLower().Equals("--validate-arguments"))
                {
                    Log("Reading validation items");
                    var validationItems = ValidationItems.Read(Console.In);
                    Log("Calling Validate");
                    Validate(validationItems);
                    return 0;
                }

                return 0;
            }
            catch (Exception e)
            {
                Log(string.Format(
                            "Exception raised: {0}",
                            e));
            }

            // Return code indicating a failure.
            return 1;
        }

        private void Log(string msg)
        {
            SystemLogger.Write(logPrefix + msg);
        }

        public abstract void StreamEvents(InputConfiguration inputConfiguration);

        public virtual void Validate(ValidationItems validationItems)
        {
            throw new NotSupportedException(
                "The override of Module.Validate must be implemented to support Modular Input External Validation.");
        }   
    }
}

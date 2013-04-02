using System;
using System.Collections.Generic;

namespace Splunk.ModularInputs.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].ToLower().Equals("--scheme"))
            {
                // Create the Modular Input Scheme
                Logger.SystemLogger(LogLevel.INFO, "Creating new Scheme object");

                var s = new Scheme
                {
                    Title = "Test Example",
                    Description = "This is a test modular input that handles all the appropriate functionality",
                    StreamingMode = Scheme.StreamingModeEnum.Xml,
                    Endpoint =
                    {
                        Arguments = new List<Argument>
                                {
                                    new Argument
                                        {
                                            Name = "interval",
                                            Description = "Polling Interval",
                                            DataType = Argument.DataTypeEnum.Number,
                                            // Must be a positive integer.
                                            Validation = "is_pos_int('interval')"
                                        },

                                    new Argument
                                        {
                                            Name = "username",
                                            Description = "Admin Username",
                                            DataType = Argument.DataTypeEnum.String,
                                        },

                                    new Argument
                                        {
                                            Name = "password",
                                            Description = "Admin Password",
                                            DataType = Argument.DataTypeEnum.String,
                                        }
                                }
                    }
                };

                // Write out the XML
                Logger.SystemLogger(LogLevel.INFO, "Dumping Scheme object to STDOUT");
                Console.WriteLine(s.Serialize());
                Environment.Exit(0);
            }
            else
            {
                Logger.SystemLogger("Reading InputDefinition File");
                InputDefinition id = InputDefinition.ReadInputDefinition(Console.In);

                Logger.SystemLogger(String.Format("Server Host: {0}", id.ServerHost));
                Logger.SystemLogger(String.Format("Server URI : {0}", id.ServerUri));
                Logger.SystemLogger(String.Format("Session Key: {0}", id.SessionKey));
                Logger.SystemLogger(String.Format("Check  Dir : {0}", id.CheckpointDirectory));
                Logger.SystemLogger(String.Format("Stanzas: {0}", id.Stanzas.Count));
                for (int i = 0; i < id.Stanzas.Count; i++)
                {
                    Logger.SystemLogger(String.Format("--- Stanza#{0}: {1} ---", i, id.Stanzas[i].Name));
                    for (int j = 0; j < id.Stanzas[i].Parameters.Count; j++)
                    {
                        Logger.SystemLogger(String.Format(
                            "Param {0}={1}",
                            id.Stanzas[i].Parameters[j].Name,
                            id.Stanzas[i].Parameters[j].Value));
                    }
                }
                Logger.SystemLogger(String.Format("End of Stanzas Dump"));
                Console.WriteLine("SDK Modular Input example.");
                Environment.Exit(0);
            }
        }
    }
}
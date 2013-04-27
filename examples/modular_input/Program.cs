using System;
using System.Collections.Generic;
using System.Threading;
using Splunk.ModularInputs;

namespace Splunk.Examples.ModularInputs
{
    /// <summary>
    ///     A Splunk modular input executable.
    ///     To install it, do the following:
    ///     1. Copy the inputs.conf.spec file (included in this project)
    ///     into $SPLUNK_HOME\etc\apps\[app_folder_name]\README\
    ///     2. Copy modular_input.exe (built by this project), and SplunkSDK.dll
    ///     into $SPLUNK_HOME\etc\apps\[app_folder_name]\bin\
    ///     3. Restart Splunk.
    ///     4. Goto Manager>>Data inputs. You should see
    ///     "C# SDK Example: System Environment Variable Monitor" listed there.
    ///     <para>
    ///         Additionally, you can add per modular input (i.e. per scheme)
    ///         default configuration, such as default host name and default index,
    ///         by adding an inputs.conf file into $SPLUNK_HOME\etc\apps\[app_folder_name]\default\
    ///     </para>
    ///     <para>
    ///         To debug, goto Program Files\Splunk\bin, run
    ///         splunk cmd splunkd print-modinput-config modular_input modular_input://[input_name]
    ///         | ..\etc\apps\[app_folder_name]\bin\modular_input
    ///     </para>
    /// </summary>
    internal class Program : Script
    {
        /// <summary>
        ///     Name of the input argument
        /// </summary>
        private const string PollingInterval = "polling_interval";

        /// <summary>
        ///     Return the scheme of this input type.
        /// </summary>
        public override Scheme Scheme
        {
            get
            {
                return new Scheme
                    {
                        Title = "C# SDK Example: System Environment Variable Monitor",
                        Description =
                            "Monitor changes to a system environment variable. When a change is detected, log the new value.",
                        StreamingMode = StreamingMode.Xml,
                        Endpoint =
                            {
                                Arguments = new List<Argument>
                                    {
                                        new Argument
                                            {
                                                // 'name' is a built in var. Only its description can be
                                                // customized.
                                                Name = "name",
                                                Description = "Name of the environment variable to monitor",
                                            },
                                        new Argument
                                            {
                                                Name = PollingInterval,
                                                Description =
                                                    "Number of milliseconds to wait before the next check of the environment variable for change. Default is 1000.",
                                                DataType = DataType.Number,
                                                Validation = "is_pos_int('polling_interval')",
                                                RequiredOnCreate = false
                                            },
                                    }
                            }
                    };
            }
        }

        /// <summary>
        ///     The executable entry point.
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>Exit code</returns>
        public static int Main(string[] args)
        {
            return Run<Program>(args);
        }

        /// <summary>
        ///     Stream events into stdout
        /// </summary>
        /// <param name="inputConfiguration">Input configuration from Splunk</param>
        public override void StreamEvents(InputDefinition inputConfiguration)
        {
            string lastVarValue = null;
            var writer = new EventStreamWriter();

            var stanza = inputConfiguration.Stanza;

            // Gets input name. It is also the env var name.
            const string Seperator = @"://";
            var indexInputName = stanza.Name.IndexOf(Seperator) + Seperator.Length;
            var varName = stanza.Name.Substring(indexInputName);

            SystemLogger.Write(
                string.Format(
                    "Name of the var to monitor is : {0}",
                    varName));

            var interval = 1000;

            string intervalParam;
            if (stanza.SingleValueParameters.TryGetValue(
                PollingInterval,
                out intervalParam))
            {
                interval = int.Parse(intervalParam);
            }

            SystemLogger.Write(
                string.Format(
                    "Polling interval is : {0}",
                    interval));

            while (true)
            {
                var varValue = Environment.GetEnvironmentVariable(
                    varName,
                    EnvironmentVariableTarget.Machine);

                // Event data can't be null for real events.  
                varValue = varValue ?? "(not exist)";

                // Splunk does not record lines with only white spaces.
                varValue = string.IsNullOrWhiteSpace(varValue)
                               ? "(white space)"
                               : varValue;

                if (varValue != lastVarValue)
                {
                    writer.Write(
                        new EventElement
                            {
                                Source = varName,
                                Data = varValue,
                            });
                    lastVarValue = varValue;
                }
                Thread.Sleep(interval);
            }
        }

        /// <summary>
        ///     Perform external validation
        /// </summary>
        /// <param name="validationItems">Configuration data to validate</param>
        /// <param name="errorMessage">Message to display in UI when validation fails</param>
        /// <returns>Whether the validation succeeded</returns>
        public override bool Validate(ValidationItems validationItems, out string errorMessage)
        {
            var varName = validationItems.Item.Name;

            var varValue = Environment.GetEnvironmentVariable(
                varName,
                EnvironmentVariableTarget.Machine);

            if (varValue == null)
            {
                errorMessage = string.Format(
                    "Environment variable '{0}' is not defined",
                    varName);

                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
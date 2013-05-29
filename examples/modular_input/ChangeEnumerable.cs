using System;
using System.Collections.Generic;
using System.Threading;

internal partial class Program
{
    internal static IEnumerable<string> GetEnvironmentVariableChanges(string varName)
    {
        string lastVarValue = null;

        while (true)
        {
            var varValue = Environment.GetEnvironmentVariable(
                varName,
                EnvironmentVariableTarget.Machine);

            if (varValue != lastVarValue)
            {
                yield return varValue;
                lastVarValue = varValue;
            }
            Thread.Sleep(200);
        }
    }
}

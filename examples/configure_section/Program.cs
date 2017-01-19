using Splunk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace configure_section
{
    public class Program
    {
		public static void Main(string[] argv)
		{
			SplunkConfigurationSection splunkConfigurationSection = SplunkConfigurationSection.GetSettings();
			Console.WriteLine("Host: " + splunkConfigurationSection.Host);
			Console.WriteLine("Password: " + splunkConfigurationSection.Password);
			Console.WriteLine("Port: " + splunkConfigurationSection.Port);
			Console.WriteLine("UseHttpsScheme: " + splunkConfigurationSection.UseHttpsScheme);
			Console.WriteLine("UserName: " + splunkConfigurationSection.UserName);
		}
    }
}

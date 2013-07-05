using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Splunk
{
	/// <summary>
	/// Define a section to configure Splunk information previous defined on splunkrc file.
	/// </summary>
	public class SplunkConfigurationSection : ConfigurationSection
	{
		private const string SECTION_NAME = "splunk";

		private const string DEFAULT_HOST = "localhost";
		private const int DEFAULT_PORT = 8089;
		private const string DEFAULT_USERNAME = "admin";
		private const string DEFAULT_PASSWORD = "changeme";
		private const bool DEFAULT_USE_HTTPS_SCHEME = true;

		private static SplunkConfigurationSection splunkConfigurationSection = null;

		public static SplunkConfigurationSection Default { get; private set; }

		static SplunkConfigurationSection()
		{
			Default = new SplunkConfigurationSection() 
			{
				Host = DEFAULT_HOST,
				Password = DEFAULT_PASSWORD,
				Port = DEFAULT_PORT,
				UserName = DEFAULT_USERNAME,
				UseHttpsScheme = DEFAULT_USE_HTTPS_SCHEME
			};
		}

		public static SplunkConfigurationSection GetSettings() 
		{
			if(splunkConfigurationSection == null){
			SplunkConfigurationSection configurationSection = ConfigurationManager.GetSection(SECTION_NAME) as SplunkConfigurationSection;
			if (configurationSection != null)
				splunkConfigurationSection = configurationSection;
			else
				splunkConfigurationSection = SplunkConfigurationSection.Default;
			}
			return splunkConfigurationSection;
		}

		/// <summary>
		/// Get or Set Splunk host. 
		/// </summary>
		[ConfigurationProperty("host", IsRequired = false, DefaultValue = DEFAULT_HOST)]
		public string Host
		{
			get
			{
				return this["host"].ToString();
			}
			set
			{
				this["host"] = value;
			}
		}

		/// <summary>
		/// Get or Set Splunk host port. 
		/// </summary>
		[ConfigurationProperty("port", IsRequired = false, DefaultValue = DEFAULT_PORT)]
		public int Port
		{
			get
			{
				return (int)this["port"];
			}
			set
			{
				this["port"] = value;
			}
		}

		/// <summary>
		/// Get or Set user name to be used on during access to Splunk. 
		/// </summary>
		[ConfigurationProperty("username", IsRequired = false, DefaultValue = DEFAULT_USERNAME)]
		public string UserName
		{
			get
			{
				return this["username"].ToString();
			}
			set
			{
				this["username"] = value;
			}
		}

		/// <summary>
		/// Get or Set user password to be used on during access to Splunk. 
		/// </summary>
		[ConfigurationProperty("password", IsRequired = false, DefaultValue = DEFAULT_PASSWORD)]
		public string Password
		{
			get
			{
				return this["password"].ToString();
			}
			set
			{
				this["password"] = value;
			}
		}

		/// <summary>
		/// Get or Set http access schema. Http or Https. 
		/// </summary>
		[ConfigurationProperty("https", IsRequired = false, DefaultValue = DEFAULT_USE_HTTPS_SCHEME)]
		public bool UseHttpsScheme
		{
			get
			{
				return (bool)this["https"];
			}
			set
			{
				this["https"] = value;
			}
		}
	}
}

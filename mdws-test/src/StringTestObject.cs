using System;
using System.Collections.Generic;
using System.Text;

namespace gov.va.medora
{
	public class StringTestObject
	{
		public System.Collections.Generic.IDictionary<string, string> properties;

		public void set(string key, string value)
		{
			properties[key] = value;
		}

		public string get(string key)
		{
			try
			{
				return (string)properties[key];
			}
			catch
			{
				// fail silently
			}
			return null;
		}

	}
}

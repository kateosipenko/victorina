using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveSDK.Models
{
	[DataContract]
	public class Parent
	{
		[DataMember(Name = Config.ApiParameters.Kind)]
		public string Kind { get; set; }

		[DataMember(Name = Config.ApiParameters.Id)]
		public string Id { get; set; }

	}
}

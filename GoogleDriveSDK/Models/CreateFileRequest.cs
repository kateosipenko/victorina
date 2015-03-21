using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveSDK.Models
{
	[DataContract]
	class CreateFileRequest
	{
		[DataMember(Name = Config.ApiParameters.Title)]
		public string Title { get; set; }

		[DataMember(Name = Config.ApiParameters.MimeType)]
		public string MimeType { get; set; }

		[DataMember(Name = Config.ApiParameters.Parents)]
		public List<Parent> Parents { get; set; }
	}
}

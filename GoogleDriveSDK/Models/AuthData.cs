using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveSDK.Models
{
	[DataContract]
	public class AuthData
	{
		private string accessToken = "ya29.PQEvZeqwyrE3GHmYQGfz1F0S3Ni3MsPoI4WTht9KdF3ohx8-DwHk3CxyTyZtOGA5rPUezb6rjT9vBg";
		private string tokenType = "Bearer";
		private int expiresIn = 3599;		

		[DataMember(Name = "access_token")]
		public string AccessToken
		{
			get { return accessToken; }
			set { accessToken = value; }
		}

		[DataMember(Name = "token_type")]
		public string TokenType
		{
			get { return tokenType; }
			set { tokenType = value; }
		}

		[DataMember(Name = "expires_in")]
		public int ExpiresIn
		{
			get { return expiresIn; }
			set { expiresIn = value; }
		}
	}
}

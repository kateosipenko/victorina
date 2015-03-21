using GoogleDriveSDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveSDK
{
	/// <summary>
	/// Read info about authorization flow here http://stackoverflow.com/questions/13455382/google-drive-api-authentication
	/// </summary>
	public class Config
	{
		public const string RefreshToken = "1/uGwpsPi1I80KuYCubu9UanPQeC1KkelxN63v44CdIOM";
		public const string VictorinaFolderId = "0B_xogXKXOx3yfnVoY25xQW95X2xNRlVwUlEyM2M5TDY4OGlFcVBPM0tTbWtZUmhuSGJ1dHM";

		private static AuthData authData = new AuthData();

		public static AuthData AuthData { 
			get { return authData; }
			set { authData = value; }
		}

		public class ApiData
		{
			public const string ClientSecret = "OLRHz4RMGO4hx5dpPzaKiZZR";
			public const string ClientId = "119903802280-ei8q7k9nfca753dse6hb0fmkg0ejioiv.apps.googleusercontent.com";
			public const string Scope = "https://www.googleapis.com/auth/drive.file";
			public const string CodeOrderUri = "https://accounts.google.com/o/oauth2/auth?scope=https://www.googleapis.com/auth/drive.file&redirect_uri=http://localhost&response_type=code&client_id=119903802280-ei8q7k9nfca753dse6hb0fmkg0ejioiv.apps.googleusercontent.com&access_type=offline";

			public const string AccessTokenOrderUri = "https://accounts.google.com/o/oauth2/token?code=4/"
				+ "iL1Fn9ShqQLptxFU2AidRW2fsdCvhw_XGs1MletV1a0.gkDD4CH-Cf0RYFZr95uygvXXn6d6mAI&"
			 + "client_id=119903802280-"
			 + "ei8q7k9nfca753dse6hb0fmkg0ejioiv.apps.googleusercontent.com&"
			 + "client_secret=OLRHz4RMGO4hx5dpPzaKiZZR&grant_type=authorization_code&redirect_uri=http://localhost";
		}

		public class ApiFunctions
		{
			public const string CreateFile = "https://www.googleapis.com/upload/drive/v2/files?uploadType=multipart";
			public const string RefreshTokenUri = "https://accounts.google.com/o/oauth2/token?refresh_token=" 
				+ RefreshToken 
				+ "client_id=" + ApiData.ClientId
				+ "client_secret=" + ApiData.ClientSecret
				+ "grant_type=refresh_token";
		}

		public class ApiParameters
		{
			public const string Authorization = "Authorization";
			public const string Title = "title";
			public const string MimeType = "mimeType";
			public const string Parents = "parents";
			public const string Kind = "kind";
			public const string Id = "id";

		}
	}
}

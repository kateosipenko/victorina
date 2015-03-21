using GoogleDriveSDK.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace GoogleDriveSDK
{
	public class GoogleDriveManager
	{
		private const string Boundary = "request_simple_boundary";

		public async static void CreateFile(string fileName)
		{
			string authHeader = Config.AuthData.TokenType + " " + Config.AuthData.AccessToken;
			StringBuilder requestContent = await GetCrateFileRequestContent(fileName);
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add(Config.ApiParameters.Authorization, authHeader);

				StringContent content = new StringContent(requestContent.ToString());
				content.Headers.Remove("Content-Type");
				content.Headers.TryAddWithoutValidation("Content-Type", "multipart/related; boundary=\"" + Boundary + "\"");

				HttpResponseMessage response = await client.PostAsync(new Uri(Config.ApiFunctions.CreateFile),
					content, new CancellationToken());
				if (response.StatusCode != System.Net.HttpStatusCode.OK)
				{
					await RefreshToken();
					CreateFile(fileName);
				}
			}
		}

		private async static Task<StringBuilder> GetCrateFileRequestContent(string fileName)
		{
			CreateFileRequest request = new CreateFileRequest
			{
				Title = fileName,
				MimeType = "text/plain",
				Parents = new List<Parent>() { new Parent { Id = Config.VictorinaFolderId } }
			};

			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.NullValueHandling = NullValueHandling.Ignore;
			string fileData = await JsonConvert.SerializeObjectAsync(request, Formatting.None, settings);
			StringBuilder requestContent = new StringBuilder();
			requestContent.Append("--" + Boundary + Environment.NewLine);
			requestContent.Append("Content-Type: " + "application/json" + Environment.NewLine);
			requestContent.Append(Environment.NewLine);
			requestContent.Append(fileData + Environment.NewLine);
			requestContent.Append("--" + Boundary + Environment.NewLine);
			requestContent.Append(Environment.NewLine);
			requestContent.Append(Environment.NewLine);
			requestContent.Append(fileName + Environment.NewLine);
			requestContent.Append("--" + Boundary + "--" + Environment.NewLine);
			requestContent.Append(Environment.NewLine);

			return requestContent;
		}

		public async static Task<AuthData> RefreshToken()
		{
			AuthData result = new AuthData();
			using (var client = new HttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(Config.ApiFunctions.RefreshTokenUri);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					string responseContent = await response.Content.ReadAsStringAsync();
					result = JsonConvert.DeserializeObject<AuthData>(responseContent);
					Config.AuthData = result;
				}
			}

			return result;
		}

		public static bool HasInternetConnection()
		{
			ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
			bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
			return internet;
		}
	}
}

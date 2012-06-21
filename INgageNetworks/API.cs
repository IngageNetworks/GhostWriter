using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using RestSharp;

namespace INgageNetworks
{
	public class Api
	{
		private const string BaseUrl = "http://developer.ingagenetworks.com/v1/";
		private const string BaseSecureUrl = "https://api.ingagenetworks.com/v1/authentication";
		private readonly string _userName;
		private readonly string _password;
		private IRestClient _client;
		private string _accessToken;
		private readonly string _apiKey;

		public Api(string userName, string password, string apiKey)
		{
			_userName = userName;
			_password = password;
			_apiKey = apiKey;
			Authenticate();
		}

		public void Authenticate()
		{
			var request = new RestRequest("/", Method.GET) { RequestFormat = DataFormat.Json };
			request.AddHeader("api-key", _apiKey);
			var stsClient = new RestClient(BaseSecureUrl)
			{
				Authenticator = new HttpBasicAuthenticator(_userName, _password)
			};

			stsClient.GetAsync<Token>(request,
									  (response, handle) =>
									  {
										  if (response.StatusCode == HttpStatusCode.OK)
											  if (response.Data != null) _accessToken = response.Data.AccessToken;
											  else if (response.ErrorException != null)
												  throw response.ErrorException;
									  }
				);
		}

		#region Containers

		public Container CreateContainer(string name, Status status)
		{
			var request = new RestRequest("containers/", Method.POST)
			{
				RootElement = "Container"
			};
			request.AddParameter("Name", name);
			request.AddParameter("Status", status);
			PrepareCall(request);

			var container = new Container();
			_client.PostAsync<Container>(request,
				(response, restRequestAsyncHandle) =>
				{
					if (response.StatusCode == HttpStatusCode.OK)
						if (response.Data != null) container = response.Data;
						else
							if (response.ErrorException != null)
								throw response.ErrorException;
				}
			);
			return container;
		}

		public Container ReadContainer(int containerId)
		{
			var request = new RestRequest("containers/{containerid}", Method.GET)
			{
				RootElement = "Container"
			};
			request.AddUrlSegment("containerid", containerId.ToString(CultureInfo.InvariantCulture));
			PrepareCall(request);
			var container = new Container();
			_client.GetAsync<Container>(request,
				(response, restRequestAsyncHandle) =>
				{
					if (response.StatusCode == HttpStatusCode.OK)
						if (response.Data != null) container = response.Data;
						else
							if (response.ErrorException != null)
								throw response.ErrorException;
				}
			);
			return container;
		}

		public ContainerCollection ReadContainers(int? parentId, int limit, int offset, int order, Status[] statuses)
		{
			var request = new RestRequest("containers/", Method.GET)
			{
				RootElement = "Containers",
			};
			if (parentId != null)
				request.AddParameter("parentId", parentId, ParameterType.GetOrPost);
			request.AddParameter("limit", limit, ParameterType.GetOrPost);
			request.AddParameter("offset", offset, ParameterType.GetOrPost);
			request.AddParameter("order", order, ParameterType.GetOrPost);
			if (statuses.Length != 0)
				request.AddParameter("statuses", string.Join(",", statuses), ParameterType.GetOrPost);
			PrepareCall(request);

			var containers = new ContainerCollection();
			_client.GetAsync<ContainerCollection>(request,
				(response, restRequestAsyncHandle) =>
				{
					if (response.StatusCode == HttpStatusCode.OK)
						if (response.Data != null) containers = response.Data;
						else
							if (response.ErrorException != null)
								throw response.ErrorException;
				}
			);
			return containers;
		}

		public Container UpdateContainer(int containerId, string name)
		{
			var request = new RestRequest("containers/{containerid}", Method.PUT)
			{
				RootElement = "Container"
			};
			request.AddUrlSegment("containerid", containerId.ToString(CultureInfo.InvariantCulture));
			request.AddParameter("Name", name);
			PrepareCall(request);

			var container = new Container();
			_client.PutAsync<Container>(request,
				(response, restRequestAsyncHandle) =>
				{
					if (response.StatusCode == HttpStatusCode.OK)
						if (response.Data != null) container = response.Data;
						else
							if (response.ErrorException != null)
								throw response.ErrorException;
				}
			);
			return container;
		}

		#region Status Changes

		public Boolean ArchiveContainer(int containerId)
		{
			return ContainerStatus(Status.Archived, containerId);
		}

		public Boolean DraftContainer(int containerId)
		{
			return ContainerStatus(Status.Drafted, containerId);
		}

		public Boolean PublishContainer(int containerId)
		{
			return ContainerStatus(Status.Published, containerId);
		}

		public Boolean ReviewContainer(int containerId)
		{
			return ContainerStatus(Status.ForReview, containerId);
		}

		public Boolean DeleteContainer(int containerId)
		{
			var request = new RestRequest("containers/{containerid}/", Method.DELETE)
			{
				RootElement = "Success"
			};
			request.AddUrlSegment("containerid", containerId.ToString(CultureInfo.InvariantCulture));
			PrepareCall(request);

			var result = false;
			_client.DeleteAsync<Boolean>(request,
				(response, restRequestAsyncHandle) =>
				{
					if (response.StatusCode == HttpStatusCode.OK)
						result = response.Data;
					else
						if (response.ErrorException != null)
							throw response.ErrorException;
				}
			);
			return result;
		}

		private Boolean ContainerStatus(Status status, int containerId)
		{
			var request = new RestRequest("containers/{containerid}/" + status.ToString().ToLower(), Method.PUT)
			{
				RootElement = "Success"
			};

			request.AddUrlSegment("containerid", containerId.ToString(CultureInfo.InvariantCulture));
			PrepareCall(request);

			var result = false;
			_client.PutAsync<Boolean>(request,
				(response, restRequestAsyncHandle) =>
				{
					if (response.StatusCode == HttpStatusCode.OK)
						result = response.Data;
					else
						if (response.ErrorException != null)
							throw response.ErrorException;
				}
			);
			return result;
		}

		#endregion Status Changes

		#endregion Containers

		#region Blogs

		public List<Blog> Blogs { get; set; }

		#endregion Blogs

		#region BlogPosts

		public List<BlogPost> BlogPosts { get; set; }

		#endregion BlogPosts

		#region Comments

		public List<Comment> Comments { get; set; }

		#endregion Comments

		#region User

		public User User { get; set; }

		#endregion User

		public void PrepareCall(IRestRequest request)
		{
			if (_accessToken == null) Authenticate();
			_client = new RestClient(BaseUrl);

			request.RequestFormat = DataFormat.Json;
			request.AddHeader("X-STS-AccessToken", _accessToken);
			request.AddHeader("APIKey", _apiKey);
		}
	}
}
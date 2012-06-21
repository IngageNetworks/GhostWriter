using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace INgageNetworks
{
	public class User
	{
		public int Id { get; set; }

		public string UserName { get; set; }

		public UserProfile Profile { get; set; }

		public Audit Audit { get; set; }
	}
}
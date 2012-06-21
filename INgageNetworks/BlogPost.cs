using System.Collections.Generic;

namespace INgageNetworks
{
	public class BlogPost : Blog
	{
		public string Title { get; set; }

		public long BlogId { get; set; }

		public string Body { get; set; }

		public string BodyPreview { get; set; }
	}

	public class BlogPostCollection
	{
		public List<BlogPost> BlogPosts { get; set; }
	}
}
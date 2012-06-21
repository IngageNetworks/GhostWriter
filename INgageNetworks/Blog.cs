using System.Collections.Generic;

namespace INgageNetworks
{
	public class Blog : EntityBase
	{
		public long Author { get; set; }

		public long Owner { get; set; }
	}

	public class BlogCollection
	{
		public List<Blog> Blogs { get; set; }
	}
}
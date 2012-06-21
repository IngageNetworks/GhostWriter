using System.Collections.Generic;

namespace INgageNetworks
{
	public class Comment : EntityBase
	{
		public string Body { get; set; }

		public long NumReplies { get; set; }

		public long ParentId { get; set; }
	}

	public class CommentCollection
	{
		public List<Comment> Comments { get; set; }
	}
}
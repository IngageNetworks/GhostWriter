namespace INgageNetworks
{
	public abstract class EntityBase : Audit
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public Status Status { get; set; }
	}

	public enum Status
	{
		Drafted = 1,
		Published = 2,
		Archived = 3,
		Deleted = 4,
		ForReview = 5
	}
}
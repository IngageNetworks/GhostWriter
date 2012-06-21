using INgageNetworks.Helpers;

namespace INgageNetworks
{
	public abstract class Audit
	{
		public long CreatedBy { get; set; }

		public DateTimeField CreatedOn { get; set; }

		public long ModifiedBy { get; set; }

		public DateTimeField ModifiedOn { get; set; }
	}
}
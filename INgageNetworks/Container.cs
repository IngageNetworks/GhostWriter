using System.Collections.Generic;

namespace INgageNetworks
{
	public class Container : EntityBase
	{
		public long ParentContainerId { get; set; }

		public List<Container> Children { get; set; }
	}

	public class ContainerCollection
	{
		public List<Container> Containers { get; set; }
	}
}
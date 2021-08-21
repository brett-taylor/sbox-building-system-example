using System.Collections.Generic;

namespace ThatTycoonGame.Building.Types
{
	public interface IBuildingType
	{
		public string Model { get; }
		public PlacementType PlacementType { get; }
		public IEnumerable<BuildingTag> Tags { get; }
		public bool DoPlotBoundsCheck { get; }
		public Rotation DefaultRotation { get; }
		public float Width { get; }
		public float Height { get; }
		public float Depth { get; }
		public float MaxBuildDistance { get; }
		public string LongName { get; }
		public string ShortName { get; }
	}
}

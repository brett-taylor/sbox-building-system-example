using System.Collections.Generic;
using Sandbox;
using ThatTycoonGame.Utils;

namespace ThatTycoonGame.Building.Types.Generic
{
	[Library("tg_generic_floor")]
	public class GenericFloor : IBuildingType
	{
		public string Model => "models/tg_buildings/tg_floor.vmdl";
		public PlacementType PlacementType => PlacementType.FLOOR;
		public IEnumerable<BuildingTag> Tags { get; } = ListUtils.Of( BuildingTag.BUILDING_TAG_FLOOR );
		public bool DoPlotBoundsCheck => false;
		public Rotation DefaultRotation => Rotation.Identity;
		public float Width => 120f;
		public float Height => 10f;
		public float Depth => 120f;
		public float MaxBuildDistance => 300f;
		public string LongName => "Generic Floor";
		public string ShortName => "Floor";

		public GenericFloor()
		{
		}
	}
}

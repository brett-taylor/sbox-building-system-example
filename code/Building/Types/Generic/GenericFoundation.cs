using System.Collections.Generic;
using Sandbox;
using ThatTycoonGame.Utils;

namespace ThatTycoonGame.Building.Types.Generic
{
	[Library( "tg_generic_foundation" )]
	public class GenericFoundation : IBuildingType
	{
		public string Model => "models/tg_buildings/tg_foundation.vmdl";
		public PlacementType PlacementType => PlacementType.FOUNDATION;
		public IEnumerable<BuildingTag> Tags { get; } = ListUtils.Of( BuildingTag.BUILDING_TAG_FOUNDATION );
		public bool DoPlotBoundsCheck => true;
		public Rotation DefaultRotation => Rotation.Identity;
		public float Width => 120f;
		public float Height => 30f;
		public float Depth => 120f;
		public float MaxBuildDistance => 700f;
		public string LongName => "Generic Foundation";
		public string ShortName => "Foundation";

		public GenericFoundation()
		{
		}
	}
}

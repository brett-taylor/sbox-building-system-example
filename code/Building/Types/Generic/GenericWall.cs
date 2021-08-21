using System.Collections.Generic;
using Sandbox;
using ThatTycoonGame.Utils;

namespace ThatTycoonGame.Building.Types.Generic
{
	[Library( "tg_generic_wall" )]
	public partial class GenericWall : IBuildingType
	{
		public string Model => "models/tg_buildings/tg_wall.vmdl";
		public PlacementType PlacementType => PlacementType.WALL;
		public IEnumerable<BuildingTag> Tags { get; } = ListUtils.Of( BuildingTag.BUILDING_TAG_WALL );
		public bool DoPlotBoundsCheck => false;
		public Rotation DefaultRotation => Rotation.FromYaw( 90f );
		public float Width => 120f;
		public float Height => 100f;
		public float Depth => 5f;
		public float MaxBuildDistance => 300f;
		public string LongName => "Generic Wall";
		public string ShortName => "Wall";

		public GenericWall()
		{
		}
	}
}

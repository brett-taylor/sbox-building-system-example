using System.Collections.Generic;
using Sandbox;
using ThatTycoonGame.Utils;

namespace ThatTycoonGame.Building.Types.Generic
{
	[Library( "tg_generic_stairs" )]
	public partial class GenericStairs : IBuildingType
	{
		public string Model => "models/tg_buildings/tg_stairs.vmdl";
		public PlacementType PlacementType => PlacementType.FLOOR_CENTER;
		public IEnumerable<BuildingTag> Tags { get; } = ListUtils.Empty<BuildingTag>();
		public bool DoPlotBoundsCheck => false;
		public Rotation DefaultRotation => Rotation.FromYaw( 90f );
		public float Width => 95f;
		public float Height => 95f;
		public float Depth => 95f;
		public float MaxBuildDistance => 300f;
		public string LongName => "Generic Stairs";
		public string ShortName => "Stairs";

		public GenericStairs()
		{
		}
	}
}

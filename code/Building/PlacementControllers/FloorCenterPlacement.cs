using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ThatTycoonGame.Building.Types;
using ThatTycoonGame.Entities.Player;
using ThatTycoonGame.Utils;

namespace ThatTycoonGame.Building.PlacementControllers
{
	public class FloorCenterPlacement : RaycastFromCenterPlacement
	{
		private static readonly float SNAP_POINT_SEARCH_RADIUS = 15f;
		private static readonly string SNAP_POINT_PREFIX = "snap_point_center";
		private readonly float ROTATION_SNAP = 90f;

		private static readonly IEnumerable<BuildingTag> VALID_TAGS = ListUtils.Of(
			BuildingTag.BUILDING_TAG_FOUNDATION,
			BuildingTag.BUILDING_TAG_FLOOR
		);

		protected override bool PlacementUpdate( TycoonPlayer player, IBuildingType type, ModelEntity buildingGhost, TraceResult trHit )
		{
			var floors = Physics.GetEntitiesInSphere( trHit.EndPos, SNAP_POINT_SEARCH_RADIUS )
				.Where( e => e.Tags.HasAtLeastOneOf( VALID_TAGS ) )
				.OrderBy( be => be.Position.Distance( trHit.EndPos ) )
				.ToList();

			if ( Input.MouseWheel != 0 )
				buildingGhost.Rotation *= Rotation.FromYaw( Input.MouseWheel * ROTATION_SNAP );
			
			if ( floors.Any() )
				return DoCenterSnap( floors.First() as BuildingEntity, buildingGhost, trHit.EndPos, type, player );

			buildingGhost.Position = trHit.EndPos;
			return false;
		}

		private static bool DoCenterSnap( BuildingEntity floor, Entity buildingGhost, Vector3 rayEndPosition, IBuildingType type, Entity player )
		{
			var orderedSnapPoints = floor.GetAllSnapPointTransforms( SNAP_POINT_PREFIX )
				.Select( transform => floor.Transform.ToWorld( transform ) )
				.OrderBy( transform => transform.Position.Distance( rayEndPosition ) )
				.ToList();
			
			if ( !orderedSnapPoints.Any() )
			{
				buildingGhost.Position = rayEndPosition;
				return false;
			}

			buildingGhost.Position = orderedSnapPoints.First().Position;
			return true;
		}
	}
}

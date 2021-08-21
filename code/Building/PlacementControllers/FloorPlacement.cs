using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ThatTycoonGame.Building.Types;
using ThatTycoonGame.Entities.Player;
using ThatTycoonGame.Utils;

namespace ThatTycoonGame.Building.PlacementControllers
{
	public class FloorPlacement : RaycastFromCenterPlacement
	{
		private static readonly float SNAP_POINT_SEARCH_RADIUS = 15f;
		private static readonly string SNAP_POINT_PREFIX = "snap_point_floor";

		private static readonly IEnumerable<BuildingTag> VALID_TAGS = ListUtils.Of(
			BuildingTag.BUILDING_TAG_WALL,
			BuildingTag.BUILDING_TAG_FLOOR
		);

		protected override bool PlacementUpdate( TycoonPlayer player, IBuildingType type, ModelEntity buildingGhost, TraceResult trHit )
		{
			var walls = Physics.GetEntitiesInSphere( trHit.EndPos, SNAP_POINT_SEARCH_RADIUS )
				.Where( e => e.Tags.HasAtLeastOneOf( VALID_TAGS ) )
				.OrderBy( be => be.Position.Distance( trHit.EndPos ) )
				.ToList();

			if ( walls.Any() )
				return DoWallSnap( walls.First() as BuildingEntity, buildingGhost, trHit.EndPos, type, player );

			buildingGhost.Position = trHit.EndPos;
			return false;
		}

		private static bool DoWallSnap( BuildingEntity wall, Entity buildingGhost, Vector3 rayEndPosition, IBuildingType type, Entity player )
		{
			var orderedSnapPointsByDistance = wall.GetAllSnapPointTransforms( SNAP_POINT_PREFIX )
				.Select( transform => wall.Transform.ToWorld( transform ) )
				.OrderBy( transform => transform.Position.Distance( rayEndPosition ) )
				.ToList();

			var lowestDistance = orderedSnapPointsByDistance.Min( sp => sp.Position.Distance( rayEndPosition ) );
			var playerLookInverse = player.Rotation * Rotation.FromYaw(180);
			var orderedSnapPointsByClosestRotation = orderedSnapPointsByDistance
				.Where( sp => sp.Position.Distance( rayEndPosition ) == lowestDistance )
				.OrderBy( sp => sp.Rotation.Distance( playerLookInverse ) )
				.ToList();

			if ( !orderedSnapPointsByClosestRotation.Any() )
			{
				buildingGhost.Position = rayEndPosition;
				return false;
			}

			var closestSnapPoint = orderedSnapPointsByClosestRotation.First();
			buildingGhost.Position = closestSnapPoint.Position + new Vector3( type.Depth / 2, 0f, 0f ) * closestSnapPoint.Rotation;
			buildingGhost.Rotation = closestSnapPoint.Rotation * type.DefaultRotation;
			return true;
		}
	}
}

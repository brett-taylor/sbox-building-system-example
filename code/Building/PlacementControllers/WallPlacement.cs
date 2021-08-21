using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ThatTycoonGame.Building.Types;
using ThatTycoonGame.Entities.Player;
using ThatTycoonGame.Utils;

namespace ThatTycoonGame.Building.PlacementControllers
{
	public class WallPlacement : RaycastFromCenterPlacement
	{
		private static readonly float SNAP_POINT_SEARCH_RADIUS = 15f;
		private static readonly string SNAP_POINT_PREFIX = "snap_point_wall";

		private static readonly IEnumerable<BuildingTag> VALID_TAGS = ListUtils.Of(
			BuildingTag.BUILDING_TAG_FOUNDATION,
			BuildingTag.BUILDING_TAG_WALL,
			BuildingTag.BUILDING_TAG_FLOOR
		);

		protected override bool PlacementUpdate( TycoonPlayer player, IBuildingType type, ModelEntity buildingGhost, TraceResult trHit )
		{
			var foundations = Physics.GetEntitiesInSphere( trHit.EndPos, SNAP_POINT_SEARCH_RADIUS )
				.Where( e => e.Tags.HasAtLeastOneOf( VALID_TAGS ) )
				.OrderBy( be => be.Position.Distance( trHit.EndPos ) )
				.ToList();
			
			if ( foundations.Any() )
				return DoEdgeSnap( foundations.First() as BuildingEntity, buildingGhost, trHit.EndPos, type );

			buildingGhost.Position = trHit.EndPos;
			return false;
		}

		private static bool DoEdgeSnap( BuildingEntity foundation, Entity buildingGhost, Vector3 rayEndPosition, IBuildingType type )
		{
			var orderedSnapPoints = foundation.GetAllSnapPointTransforms( SNAP_POINT_PREFIX )
				.Select( transform => foundation.Transform.ToWorld( transform ) )
				.OrderBy( transform => transform.Position.Distance( rayEndPosition ) )
				.ToList();

			if ( !orderedSnapPoints.Any() )
			{
				buildingGhost.Position = rayEndPosition;
				return false;
			}
			
			var closestSnapPoint = orderedSnapPoints.First();
			buildingGhost.Position = closestSnapPoint.Position;
			buildingGhost.Rotation = closestSnapPoint.Rotation * type.DefaultRotation;
			return true;
		}
	}
}

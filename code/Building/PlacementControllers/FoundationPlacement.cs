using System.Linq;
using Sandbox;
using ThatTycoonGame.Building.Types;
using ThatTycoonGame.Entities.Player;
using ThatTycoonGame.Utils;

namespace ThatTycoonGame.Building.PlacementControllers
{
	public class FoundationPlacement : RaycastFromCenterPlacement
	{
		private static readonly float SEARCH_RADIUS = 120f;
		private static readonly string SNAP_POINT_PREFIX = "snap_point_foundation";

		protected override bool PlacementUpdate( TycoonPlayer player, IBuildingType type, ModelEntity buildingGhost, TraceResult trHit )
		{
			if ( trHit.Entity.Tags.Has( BuildingTag.BUILDING_TAG_FLOOR ) )
			{
				buildingGhost.Position = trHit.EndPos;
				return false;
			}
			
			var foundations = Physics.GetEntitiesInSphere( trHit.EndPos, SEARCH_RADIUS )
				.Where( e => e.Tags.Has( BuildingTag.BUILDING_TAG_FOUNDATION ) )
				.OrderBy( be => be.Position.Distance( trHit.EndPos ) )
				.ToList();

			buildingGhost.Position = foundations.Any()
				? CalculateSnapPosition( foundations.First() as BuildingEntity, buildingGhost, trHit.EndPos, type )
				: trHit.EndPos;

			return true;
		}

		private static Vector3 CalculateSnapPosition( BuildingEntity foundation, ModelEntity buildingGhost, Vector3 rayEndPosition, IBuildingType type )
		{
			var closestSnapPoint = foundation.GetAllSnapPointTransforms( SNAP_POINT_PREFIX )
				.Select( transform => foundation.Transform.ToWorld( transform ) )
				.OrderBy( transform => transform.Position.Distance( rayEndPosition ) )
				.First();

			return foundation.Position + (new Vector3( type.Depth, 0f, 0f ) * closestSnapPoint.Rotation);
		}
	}
}

using Sandbox;
using ThatTycoonGame.Building.Types;
using ThatTycoonGame.Entities.Player;

namespace ThatTycoonGame.Building.PlacementControllers
{
	public abstract class RaycastFromCenterPlacement : IPlacementController
	{
		private static readonly float RESTING_DISTANCE = 150f;
		
		public bool PlacementUpdate( TycoonPlayer player, IBuildingType type, ModelEntity buildingGhost )
		{
			var tr = Trace.Ray( player.EyePos, player.EyePos + player.EyeRot.Forward * type.MaxBuildDistance )
				.Radius( 1 )
				.Ignore( player )
				.Run();

			if ( !tr.Hit )
			{
				buildingGhost.Position = player.EyePos 
					+ (player.EyeRot.Forward * RESTING_DISTANCE) 
					- (new Vector3(0f, 0f, type.Height / 2f) * buildingGhost.Rotation.Up);
				return false;
			}

			buildingGhost.EnableDrawing = true;
			return PlacementUpdate( player, type, buildingGhost, tr );
		}

		protected abstract bool PlacementUpdate( TycoonPlayer player, IBuildingType type, ModelEntity buildingGhost, TraceResult trHit );
	}
}

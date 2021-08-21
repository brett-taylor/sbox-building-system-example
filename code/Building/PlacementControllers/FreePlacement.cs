using Sandbox;
using ThatTycoonGame.Building.Types;
using ThatTycoonGame.Entities.Player;

namespace ThatTycoonGame.Building.PlacementControllers
{
	public class FreePlacement : RaycastFromCenterPlacement
	{
		private readonly float ROTATION_SNAP = 30f;

		protected override bool PlacementUpdate( TycoonPlayer player, IBuildingType type, ModelEntity buildingGhost, TraceResult trHit )
		{
			if ( trHit.EndPos != buildingGhost.Position )
				buildingGhost.Position = trHit.EndPos;

			if ( Input.MouseWheel != 0 )
				buildingGhost.Rotation *= Rotation.FromYaw( Input.MouseWheel * ROTATION_SNAP );

			return true;
		}
	}
}

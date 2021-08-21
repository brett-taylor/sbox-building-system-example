using Sandbox;
using ThatTycoonGame.Building.Types;
using ThatTycoonGame.Entities.Player;

namespace ThatTycoonGame.Building.PlacementControllers
{
	public interface IPlacementController
	{
		public bool PlacementUpdate( TycoonPlayer player, IBuildingType type, ModelEntity buildingGhost );
	}
}

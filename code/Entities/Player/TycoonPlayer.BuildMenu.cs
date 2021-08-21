using Sandbox;
using ThatTycoonGame.Building.Types;

namespace ThatTycoonGame.Entities.Player
{
	public partial class TycoonPlayer
	{
		private static readonly float OPEN_MENU_HOLD_TIME = 0.15f;

		public bool BuildMenuOpen { get; private set; }
		public IBuildingType LastBuiltType { get; set; }

		private float lastBuildButtonPress = 0f;

		public void BuildMenuSimulate()
		{
			if ( Input.Pressed( InputButton.Use ) )
			{
				lastBuildButtonPress = RealTime.Now;
				return;
			}

			if ( Input.Released( InputButton.Use ) && (lastBuildButtonPress + OPEN_MENU_HOLD_TIME > RealTime.Now) )
			{
				if ( !IsServer )
					return;

				if ( BuildingController.IsBuilding )
					BuildingController.StopBuilding();
				else if ( LastBuiltType is not null )
					BuildingController.StartBuilding( LastBuiltType );

				return;
			}

			BuildMenuOpen = Input.Down( InputButton.Use ) && lastBuildButtonPress + OPEN_MENU_HOLD_TIME < RealTime.Now;
		}
	}
}

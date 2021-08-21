using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ThatTycoonGame.Building;
using ThatTycoonGame.Building.Types;
using ThatTycoonGame.Entities.Player;

namespace ThatTycoonGame.Ui.BuildMenu
{
	public class SelectedBuildItemToast : Panel
	{
		private BuildingController buildingController = null;
		private IBuildingType previousBuildType = null;

		public SelectedBuildItemToast()
		{
			StyleSheet.Load( "/ui/BuildMenu/SelectedBuildItemToast.scss" );

			var useButton = Input.GetKeyWithBinding( "iv_use" );
			var labelOne = Add.Label( $"Tap {useButton.ToUpper()} to toggle building" );
			labelOne.Style.Dirty();

			Add.Label( $"Hold {useButton.ToUpper()} to change building" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( buildingController is null && Local.Pawn is not null )
				buildingController = (Local.Pawn as TycoonPlayer).BuildingController;

			if ( buildingController is null )
				return;

			UpdateStatus();
		}

		private void UpdateStatus()
		{
			var currentBuildType = buildingController.BuildingType;
			if ( currentBuildType == previousBuildType )
				return;

			if ( currentBuildType is null )
			{
				previousBuildType = null;
				return;
			}

			previousBuildType = buildingController.BuildingType;
		}
	}
}

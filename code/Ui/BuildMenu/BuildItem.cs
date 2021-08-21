using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ThatTycoonGame.Building.Types;

namespace ThatTycoonGame.Ui.BuildMenu
{
	public class BuildItem : Panel
	{
		private readonly Button button = null;
		private IBuildingType buildingType = null;

		public IBuildingType BuildingType
		{
			set => SetBuildingType( value );
		}

		public BuildItem()
		{
			StyleSheet.Load( "/ui/BuildMenu/BuildItem.scss" );
			button = Add.Button( "blah", "item", OnClick );
		}

		private void SetBuildingType( IBuildingType type )
		{
			buildingType = type;
			button.Text = type.ShortName;
		}

		private void OnClick()
		{
			ConsoleSystem.Run( "tg_start_building", Library.GetAttribute( buildingType.GetType() ).Name );
		}
	}
}

using Sandbox;
using Sandbox.UI;
using ThatTycoonGame.Building.Types;
using ThatTycoonGame.Entities.Player;

namespace ThatTycoonGame.Ui.BuildMenu
{
	public class BuildMenu : Panel
	{
		public BuildMenu()
		{
			StyleSheet.Load( "/ui/BuildMenu/BuildMenu.scss" );

			var mainPanel = AddChild<Panel>( "main-panel" );
			foreach ( var type in Library.GetAll<IBuildingType>() )
			{
				var bi = mainPanel.AddChild<BuildItem>();
				bi.BuildingType = Library.Create<IBuildingType>( type );
			}
		}

		public override void Tick()
		{
			base.Tick();
			SetClass( "open", (Local.Pawn as TycoonPlayer).BuildMenuOpen );
		}
	}
}

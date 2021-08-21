using Sandbox.UI;
using ThatTycoonGame.Utils;

namespace ThatTycoonGame.Ui.Hud
{
	public class Crosshair : Panel
	{
		private Panel dot;
		
		public Crosshair()
		{
			StyleSheet.Load( "/ui/Hud/Crosshair.scss" );

			dot = AddChild<Panel>( "center-dot" );
		}

		public override void Tick()
		{
			base.Tick();
			
			if (dot is not null)
				dot.PositionAtCrosshair();
		}
	}
}

using Sandbox;
using Sandbox.UI;
using ThatTycoonGame.Ui.Scoreboard;

namespace ThatTycoonGame.Ui
{
	public class TycoonHudRootPanel : HudEntity<RootPanel>
	{
		public static TycoonHudRootPanel Current { get; private set; }
		public TycoonScoreboard TycoonScoreboard { get; }
		public WorldText.WorldText WorldText { get; }

		public TycoonHudRootPanel()
		{
			if ( !IsClient )
				return;

			Current = this;
			TycoonScoreboard = RootPanel.AddChild<TycoonScoreboard>();
			WorldText = RootPanel.AddChild<WorldText.WorldText>();
		}
	}
}

using Sandbox;
using Sandbox.UI;
using ThatTycoonGame.Ui.BuildMenu;
using ThatTycoonGame.Ui.Hud;
using ThatTycoonGame.Ui.Scoreboard;

namespace ThatTycoonGame.Ui
{
	public class TycoonHudRootPanel : HudEntity<RootPanel>
	{
		public static TycoonHudRootPanel Current { get; private set; }
		public TycoonScoreboard TycoonScoreboard { get; }
		public WorldText.WorldText WorldText { get; }
		public Crosshair Crosshair { get; }
		public BuildMenu.BuildMenu BuildMenu { get; }
		public SelectedBuildItemToast SelectedBuildItemToast { get; set; } // TODO: REMOVE SETTER

		public TycoonHudRootPanel()
		{
			if ( !IsClient )
				return;

			Current = this;
			TycoonScoreboard = RootPanel.AddChild<TycoonScoreboard>();
			WorldText = RootPanel.AddChild<WorldText.WorldText>();
			Crosshair = RootPanel.AddChild<Crosshair>();
			BuildMenu = RootPanel.AddChild<BuildMenu.BuildMenu>();
			SelectedBuildItemToast = RootPanel.AddChild<SelectedBuildItemToast>();

			RootPanel.AddChild<NameTags>();
			RootPanel.AddChild<ChatBox>();
			RootPanel.AddChild<VoiceList>();
			RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
		}
	}
}

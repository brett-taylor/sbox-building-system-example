using Sandbox.UI;
using Sandbox.UI.Construct;
using ThatTycoonGame.Entities.Player;
using ThatTycoonGame.Plot.Type;

namespace ThatTycoonGame.Ui.Scoreboard
{
	public class TycoonScoreboardPlayerRow : Panel
	{
		private TycoonPlayer player;
		private readonly TycoonScoreboardRow row;
		private readonly Image avatar;

		public TycoonScoreboardPlayerRow()
		{
			avatar = Add.Image( "" );
			avatar.SetClass( "avatar", true );

			row = AddChild<TycoonScoreboardRow>();
			row.FirstColumn.SetText( "Player Name" );
			row.SecondColumn.SetText( "300" );
			row.SetClass( "player-row", true );
		}

		public void SetPlayer( TycoonPlayer player )
		{
			this.player = player;
			row.FirstColumn.SetText( player.GetClientOwner().Name );

			avatar.SetTexture( $"avatar:{player.GetClientOwner().SteamId}" );
			avatar.Style.BorderColor = player.PlotTeam.GetDescription().Color;
			avatar.Style.Dirty();
		}

		public void Update()
		{
			row.SecondColumn.SetText( player.Money.ToString() );
		}
	}
}

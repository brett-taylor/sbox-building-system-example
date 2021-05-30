using System.Linq;
using Sandbox;
using ThatTycoonGame.Data;
using ThatTycoonGame.Entities;
using ThatTycoonGame.Plot;
using ThatTycoonGame.Plot.Type;
using ThatTycoonGame.Ui;

namespace ThatTycoonGame
{
	[Library( "ThatTycoonGame" )]
	public partial class TycoonGame : Game
	{
		public static TycoonGame Instance => Current as TycoonGame;

		public TycoonGame()
		{
			if ( !IsServer )
				return;

			new TycoonHudRootPanel();
			new PlotManager();
			MapData.LoadData();
		}

		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new TycoonPlayer();
			client.Pawn = player;

			player.Respawn();
		}

		public override void ClientDisconnect( Client client, NetworkDisconnectionReason reason )
		{
			var player = client.Pawn as TycoonPlayer;
			player.SetPlot( PlotTeam.UNASSIGNED );
			
			base.ClientDisconnect( client, reason );
		}

		public override void PostLevelLoaded()
		{
			PlotManager.Current.CreatePlots( All.Where( e => e is PlotMapEntity )
				.Cast<PlotMapEntity>()
				.ToList()
			);
		}
	}
}

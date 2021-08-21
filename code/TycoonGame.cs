using System;
using System.Linq;
using Sandbox;
using ThatTycoonGame.Data;
using ThatTycoonGame.Entities;
using ThatTycoonGame.Entities.Player;
using ThatTycoonGame.Plot;
using ThatTycoonGame.Plot.Type;
using ThatTycoonGame.Ui;

namespace ThatTycoonGame
{
	[Library( "ThatTycoonGame" )]
	public partial class TycoonGame : Game
	{
		public TycoonGame()
		{
			if ( !IsServer )
				return;

			var _ = new TycoonHudRootPanel();
			var __ = new PlotManager();
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
			base.ClientDisconnect( client, reason );
		}

		public override void PostLevelLoaded()
		{
			PlotManager.Current.CreatePlots( All.Where( e => e is PlotMapEntity )
				.Cast<PlotMapEntity>()
				.ToList()
			);
		}

		[ServerCmd( "tg_join_plot" )]
		public static void JoinPlot( string plotName )
		{
			if ( Enum.TryParse( plotName, out PlotTeam plotTeam ) )
				(ConsoleSystem.Caller.Pawn as TycoonPlayer).SetPlot( plotTeam );
			else
				throw new ArgumentException( $"Plot {plotName} does not exist." );
		}
	}
}

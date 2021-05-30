using System.Collections.Generic;
using Sandbox;
using ThatTycoonGame.Plot.Type;

namespace ThatTycoonGame.Plot
{
	public partial class VirtualPlot : Plot
	{
		[Net] private PlotTeam PlotTeam { get; set; }
		[Net] private List<TycoonPlayer> Players { get; set; }

		public VirtualPlot()
		{
			Transmit = TransmitType.Always;
			Players = new List<TycoonPlayer>();
		}

		public VirtualPlot( PlotTeam plotTeam ) : this()
		{
			PlotTeam = plotTeam;
		}

		public override PlotTeam GetPlotTeam()
		{
			return PlotTeam;
		}

		public override IList<TycoonPlayer> GetPlayers()
		{
			return Players;
		}

		public override bool CanJoinTeam( TycoonPlayer player )
		{
			return true;
		}

		public override void RemovePlayer( TycoonPlayer player )
		{
			if ( Players.Contains( player ) )
				Players.Remove( player );
		}

		public override void AddPlayer( TycoonPlayer player )
		{
			if ( !Players.Contains( player ) )
				Players.Add( player );
		}
	}
}

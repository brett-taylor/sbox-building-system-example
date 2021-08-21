using System.Collections.Generic;
using Sandbox;
using ThatTycoonGame.Entities.Player;
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

		public override BBox GetBoundingBox()
		{
			return new();
		}

		public override int PlayerCount()
		{
			return Players.Count;
		}

		public override Vector3 RelativeSpawnPosition()
		{
			return Vector3.Zero;
		}

		public override Vector3 GetAbsolutePosition( Vector3 offset )
		{
			return offset;
		}

		public override Rotation GetAbsoluteRotation( Rotation rotation )
		{
			return rotation;
		}
	}
}

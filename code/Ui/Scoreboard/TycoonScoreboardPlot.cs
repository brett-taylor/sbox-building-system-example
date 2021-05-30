using System.Collections.Generic;
using System.Linq;
using Sandbox.UI;
using ThatTycoonGame.Plot.Type;

namespace ThatTycoonGame.Ui.Scoreboard
{
	public class TycoonScoreboardPlot : Panel
	{
		public Plot.Plot Plot { get; private set; }

		private readonly TycoonScoreboardRow header;
		private readonly Dictionary<TycoonPlayer, TycoonScoreboardPlayerRow> playerRows;

		public TycoonScoreboardPlot()
		{
			playerRows = new Dictionary<TycoonPlayer, TycoonScoreboardPlayerRow>();

			header = AddChild<TycoonScoreboardRow>();
			header.FirstColumn.SetText( "Team Name" );
			header.SecondColumn.SetText( "Cash" );
			header.SetClass( "plot-header", true );
		}

		public void SetPlot( Plot.Plot plot )
		{
			Plot = plot;
			header.Style.BackgroundColor = Plot.GetPlotTeam().GetDescription().Color;
			header.Style.Dirty();
			header.FirstColumn.SetText( Plot.GetPlotTeam().GetDescription().Name );
		}

		public override void Tick()
		{
			if ( Plot is null || playerRows is null )
				return;

			Plot.GetPlayers().Where( p => !playerRows.ContainsKey( p ) ).ToList().ForEach( AddPlayer );
			playerRows.Keys.Where( p => !Plot.GetPlayers().Contains( p ) ).ToList().ForEach( RemovePlayer );

			playerRows.Values.ToList().ForEach( pr => pr.Update() );
			header.SecondColumn.SetText( Plot.GetPlayers().Select( p => p.Money ).Sum().ToString() );
		}

		private void AddPlayer( TycoonPlayer player )
		{
			var playerRow = AddChild<TycoonScoreboardPlayerRow>();
			playerRow.SetPlayer( player );
			playerRows.Add( player, playerRow );
		}

		private void RemovePlayer( TycoonPlayer player )
		{
			playerRows[player].Delete( true );
			playerRows.Remove( player );
		}
	}
}

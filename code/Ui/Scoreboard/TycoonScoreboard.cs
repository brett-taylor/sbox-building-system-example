using System.Collections.Generic;
using System.Linq;
using Sandbox.UI;
using ThatTycoonGame.Plot;

namespace ThatTycoonGame.Ui.Scoreboard
{
	public class TycoonScoreboard : Panel
	{
		private readonly Dictionary<Plot.Plot, TycoonScoreboardPlot> plots;

		public TycoonScoreboard()
		{
			plots = new Dictionary<Plot.Plot, TycoonScoreboardPlot>();

			StyleSheet.Load( "/Ui/Scoreboard/TycoonScoreboard.scss" );

			var header = AddChild<TycoonScoreboardRow>();
			header.SetClass( "header", true );
			header.FirstColumn.SetText( "Player" );
			header.SecondColumn.SetText( "Cash" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( PlotManager.Current is not null )
			{
				var plotsToAdd = PlotManager.Current.Plots
					.Where( p => !plots.ContainsKey( p ) )
					.ToList();

				foreach ( var plot in plotsToAdd )
					AddPlot( plot );
			}
		}

		private void AddPlot( Plot.Plot plot )
		{
			var plotElement = AddChild<TycoonScoreboardPlot>();
			plotElement.SetPlot( plot );
			plots.Add( plot, plotElement );
		}
	}
}

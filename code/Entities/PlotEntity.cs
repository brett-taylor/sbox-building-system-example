using Sandbox;
using ThatTycoonGame.Plot;
using ThatTycoonGame.Plot.Type;

namespace ThatTycoonGame.Entities
{
	public partial class PlotEntity : ModelEntity
	{
		public PlotTeam PlotTeam { get; set; }

		protected override void OnDestroy()
		{
			base.OnDestroy();
			(PlotManager.Current.FindPlotForTeam( PlotTeam ) as RealPlot).UnregisterPlotEntity( this );
		}
	}
}

using Sandbox;
using ThatTycoonGame.Plot.Type;

namespace ThatTycoonGame.Entities
{
	[Library( "tg_plot" )]
	public partial class PlotMapEntity : Entity
	{
		[Property( "plot_team" )] public PlotTeam PlotTeam { get; private set; }

		[Property( "plot_maxs" )] public Vector3 LocalAreaMax { get; private set; }
		public Vector3 AbsoluteAreaMax => Position + LocalAreaMax;

		[Property( "plot_mins" )] public Vector3 LocalAreaMin { get; private set; }
		public Vector3 AbsoluteAreaMin => Position + LocalAreaMin;

		[Property( "plot_rotation" )] public Vector3 PlotRotation { get; private set; }
	}
}

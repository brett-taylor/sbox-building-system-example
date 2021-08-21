using Sandbox;
using ThatTycoonGame.Entities.Player;
using ThatTycoonGame.Plot.Type;

namespace ThatTycoonGame.Entities.Button
{
	[Library( "tg_join_plot_button" )]
	public partial class JoinPlotFloorButton : GenericFloorButton
	{
		[Net] [Property( "plot_team" )] public PlotTeam PlotTeam { get; private set; }

		protected override bool HasWorldText => true;
		protected override float WorldTextOffsetY => 50f;
		protected override Color WorldTextColor => PlotTeam.GetDescription().Color;
		protected override string WorldTextString => $"Join {PlotTeam.GetDescription().Name}";

		public override void Spawn()
		{
			base.Spawn();
			Transmit = TransmitType.Always;
		}

		protected override void DoClientTick()
		{
			if ( Local.Pawn is not null && Local.Pawn is TycoonPlayer tp )
				EnableDrawing = tp.PlotTeam != PlotTeam;
			else
				EnableDrawing = true;
		}

		protected override void DoTrigger( TycoonPlayer tycoonPlayer )
		{
			if ( !IsServer )
				return;

			tycoonPlayer.SetPlot( PlotTeam, true );
		}
	}
}

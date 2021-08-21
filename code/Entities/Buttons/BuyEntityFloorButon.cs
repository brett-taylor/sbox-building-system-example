using Sandbox;
using ThatTycoonGame.Entities.Player;
using ThatTycoonGame.Plot;
using ThatTycoonGame.Plot.Type;

namespace ThatTycoonGame.Entities.Button
{
	[Library( "tg_buy_entity_floor_button" )]
	public partial class BuyEntityFloorButton : GenericFloorButton
	{
		[Net] public PlotTeam PlotTeam { get; set; }
		[Net] public string EntityToCreateName { get; set; }
		[Net] public Vector3 EntityToCreatePosition { get; set; }
		[Net] public Rotation EntityToCreateRotation { get; set; }
		[Net] public string BuyButtonText { get; set; } = "Not Set";

		protected override string WorldTextString => BuyButtonText;
		protected override bool HasWorldText => true;
		protected override float WorldTextOffsetY => 50f;
		protected override Color WorldTextColor => PlotTeam.GetDescription().Color;

		public override void Spawn()
		{
			base.Spawn();
			Transmit = TransmitType.Always;
		}

		protected override void DoTrigger( TycoonPlayer tycoonPlayer )
		{
			if ( !IsServer )
				return;

			if ( tycoonPlayer.PlotTeam != PlotTeam )
				return;

			var plot = PlotManager.Current.FindPlotForTeam( PlotTeam ) as RealPlot;
			plot.CreateEntity( EntityToCreateName, EntityToCreatePosition, EntityToCreateRotation );

			Delete();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			(PlotManager.Current.FindPlotForTeam( PlotTeam ) as RealPlot).UnregisterPlotEntity( this );
		}

		protected override void DoClientTick()
		{
			if ( Local.Pawn is not null && Local.Pawn is TycoonPlayer tp )
				EnableDrawing = tp.PlotTeam == PlotTeam;
			else
				EnableDrawing = false;
		}
	}
}

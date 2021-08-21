using System.Linq;
using Sandbox;
using ThatTycoonGame.Building;
using ThatTycoonGame.Plot;
using ThatTycoonGame.Plot.Type;
using ThatTycoonGame.Ui.BuildMenu;

namespace ThatTycoonGame.Entities.Player
{
	public partial class TycoonPlayer : Sandbox.Player
	{
		[Net] public int Money { get; private set; }
		[Net] public PlotTeam PlotTeam { get; private set; }
		[Net] public BuildingController BuildingController { get; private set; }

		public TycoonPlayer()
		{
			Money = 500;

			if ( IsServer )
			{
				BuildingController = new BuildingController( this );
				SetPlot( PlotManager.Current.Plots.OrderBy( plot => plot.PlayerCount() ).First().GetPlotTeam() );
			}
		}

		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );
			Controller = new WalkController();
			Animator = new StandardPlayerAnimator();
			Camera = new ThirdPersonCamera();

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();

			var plot = PlotManager.Current.FindPlotForTeam( PlotTeam );
			Position = plot.GetAbsolutePosition( plot.RelativeSpawnPosition() );
		}

		private BuildMenu menu;

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
			BuildMenuSimulate();

			if ( !IsServer )
				return;

			BuildingController.Simulate();
		}

		public void SetPlot( PlotTeam newPlotTeam, bool onlyLeaveCurrentPlotIfCanJoinNewOne = false )
		{
			if ( !IsServer )
				return;

			var existingPlot = PlotManager.Current.FindPlotForTeam( PlotTeam );
			var newPlot = PlotManager.Current.FindPlotForTeam( newPlotTeam );
	
			var canJoin = newPlot is not null && newPlot.CanJoinTeam( this );
			var leaveCurrentPlot = (!onlyLeaveCurrentPlotIfCanJoinNewOne) || (canJoin);
			if ( existingPlot is not null && leaveCurrentPlot )
				existingPlot.RemovePlayer( this );

			if ( !canJoin )
				return;

			PlotTeam = newPlotTeam;
			newPlot.AddPlayer( this );
		}
	}
}

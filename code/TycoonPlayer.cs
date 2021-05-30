using Sandbox;
using ThatTycoonGame.Plot;
using ThatTycoonGame.Plot.Type;

namespace ThatTycoonGame
{
	public partial class TycoonPlayer : Player
	{
		[Net] public int Money { get; private set; }
		[Net] public PlotTeam PlotTeam { get; private set; }

		public TycoonPlayer()
		{
			Money = 500;

			if ( IsServer )
				SetPlot( PlotTeam.UNASSIGNED );
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
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
			if ( IsServer && Input.Pressed( InputButton.Attack1 ) )
			{
				var ragdoll = new ModelEntity();
				ragdoll.SetModel( "models/citizen/citizen.vmdl" );
				ragdoll.Position = EyePos + EyeRot.Forward * 40;
				ragdoll.Rotation = Rotation.LookAt( Vector3.Random.Normal );
				ragdoll.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
				ragdoll.PhysicsGroup.Velocity = EyeRot.Forward * 1000;
			}
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

using System;
using System.Collections.Generic;
using Sandbox;
using ThatTycoonGame.Data;
using ThatTycoonGame.Entities;
using ThatTycoonGame.Entities.Button;
using ThatTycoonGame.Entities.Player;
using ThatTycoonGame.Plot.Type;

namespace ThatTycoonGame.Plot
{
	public partial class RealPlot : Plot
	{
		private static readonly Vector3 SPAWN_POSITION = new(0f, -380f, 0f);

		[Net] private PlotTeam PlotTeam { get; set; }
		[Net] private List<TycoonPlayer> Players { get; set; }
		private List<Entity> EntitiesToRemoveOnPlotDeactivation { get; set; }
		private Vector3 PlotPosition { get; set; }
		private Rotation PlotRotation { get; set; }
		private BBox BoundingBox { get; set; }

		public RealPlot()
		{
			Transmit = TransmitType.Always;
			Players = new List<TycoonPlayer>();
			EntitiesToRemoveOnPlotDeactivation = new List<Entity>();
		}

		public RealPlot( PlotTeam plotTeam, Vector3 position, Rotation rotation, BBox boundingBox ) : this()
		{
			PlotTeam = plotTeam;
			PlotPosition = position;
			PlotRotation = rotation;
			BoundingBox = boundingBox;
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
			Players.Remove( player );

			if ( Players.Count == 0 )
				PlotDeactivated();
		}

		public override void AddPlayer( TycoonPlayer player )
		{
			if ( Players.Count == 0 )
				PlotActivated();

			Players.Add( player );
		}

		private void PlotActivated()
		{
			foreach ( var entitySpawnPoint in MapData.SpawnPoints )
				SpawnBuyEntityFloorButton( entitySpawnPoint );
		}

		private void SpawnBuyEntityFloorButton( EntitySpawnPoint spawnPoint )
		{
			var buyEntityButton = new BuyEntityFloorButton();
			buyEntityButton.PlotTeam = PlotTeam;
			buyEntityButton.EntityToCreateName = spawnPoint.EntityName;
			buyEntityButton.BuyButtonText = spawnPoint.BuyButtonText;
			buyEntityButton.Position = GetAbsolutePosition( spawnPoint.ButtonPosition );
			buyEntityButton.Rotation = GetAbsoluteRotation( Rotation.FromYaw( spawnPoint.ButtonRotation.y ) );
			buyEntityButton.EntityToCreatePosition = spawnPoint.EntityPosition;
			buyEntityButton.EntityToCreateRotation = Rotation.FromYaw( spawnPoint.EntityRotation.y );

			EntitiesToRemoveOnPlotDeactivation.Add( buyEntityButton );
		}

		public override Vector3 GetAbsolutePosition( Vector3 offset )
		{
			return PlotPosition + (offset * PlotRotation);
		}

		public override Rotation GetAbsoluteRotation( Rotation rotation )
		{
			return PlotRotation * rotation;
		}

		private void PlotDeactivated()
		{
			var listToDelete = new List<Entity>( EntitiesToRemoveOnPlotDeactivation );
			EntitiesToRemoveOnPlotDeactivation.Clear();
			listToDelete.ForEach( e => e.Delete() );
		}

		public void CreateEntity( string entityName, Vector3 position, Rotation rotation )
		{
			if ( !Library.Exists<Entity>( entityName ) )
				throw new ArgumentException( $"No library entity found with {entityName}" );

			var entity = Library.Create<Entity>( entityName );
			entity.Position = GetAbsolutePosition( position );
			entity.Rotation = GetAbsoluteRotation( rotation );

			EntitiesToRemoveOnPlotDeactivation.Add( entity );

			if ( entity is PlotEntity pe )
				pe.PlotTeam = PlotTeam;
		}

		public void UnregisterPlotEntity( Entity plotEntity )
		{
			if ( EntitiesToRemoveOnPlotDeactivation.Contains( plotEntity ) )
				EntitiesToRemoveOnPlotDeactivation.Remove( plotEntity );
		}

		public override BBox GetBoundingBox()
		{
			return BoundingBox;
		}

		public override int PlayerCount()
		{
			return Players.Count;
		}

		public override Vector3 RelativeSpawnPosition()
		{
			return SPAWN_POSITION;
		}
	}
}

using System;
using System.Collections.Generic;
using Sandbox;
using ThatTycoonGame.Data;
using ThatTycoonGame.Entities;
using ThatTycoonGame.Entities.SpawnPoint;
using ThatTycoonGame.Plot.Type;

namespace ThatTycoonGame.Plot
{
	public partial class RealPlot : Plot
	{
		[Net] private PlotTeam PlotTeam { get; set; }
		[Net] private List<TycoonPlayer> Players { get; set; }
		private List<Entity> EntitiesToRemoveOnPlotDeactivation { get; set; }
		private Vector3 PlotPosition { get; set; }
		private Rotation PlotRotation { get; set; }

		public RealPlot()
		{
			Transmit = TransmitType.Always;
			Players = new List<TycoonPlayer>();
			EntitiesToRemoveOnPlotDeactivation = new List<Entity>();
		}

		public RealPlot( PlotTeam plotTeam, Vector3 position, Rotation rotation ) : this()
		{
			PlotTeam = plotTeam;
			PlotPosition = position;
			PlotRotation = rotation;
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
			return Players.Count == 0;
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

		private BuyEntityFloorButton SpawnBuyEntityFloorButton( EntitySpawnPoint spawnPoint )
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
			return buyEntityButton;
		}

		public Vector3 GetAbsolutePosition( Vector3 offset )
		{
			return PlotPosition + (offset * PlotRotation);
		}

		public Rotation GetAbsoluteRotation( Rotation rotation )
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
	}
}

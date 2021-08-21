using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ThatTycoonGame.Building.PlacementControllers;
using ThatTycoonGame.Building.Types;
using ThatTycoonGame.Entities.Player;
using ThatTycoonGame.Plot;
using ThatTycoonGame.Utils;

namespace ThatTycoonGame.Building
{
	public partial class BuildingController : NetworkComponent
	{
		private static readonly float COLLISION_AREA_CHECK_REDUCE_BY = 0.05f;

		private static readonly Dictionary<PlacementType, IPlacementController> CONTROLLERS = new()
		{
			[PlacementType.FOUNDATION] = new FoundationPlacement(),
			[PlacementType.FREE_PLACEMENT] = new FreePlacement(),
			[PlacementType.WALL] = new WallPlacement(),
			[PlacementType.FLOOR] = new FloorPlacement(),
			[PlacementType.FLOOR_CENTER] = new FloorCenterPlacement()
		};

		public TycoonPlayer Player { get; }
		public bool IsBuilding => BuildingType is not null;

		[Net] private string BuildingTypeName { get; set; }
		public IBuildingType BuildingType
		{
			get => (BuildingTypeName ?? "").Length > 0 ? Library.Create<IBuildingType>( BuildingTypeName ) : null;
			set => BuildingTypeName = value is null ? null : Library.GetAttribute( value.GetType() ).Name;
		}

		private GhostBuilding ghostBuilding;
		private bool validPlacement = false;

		public BuildingController()
		{
		}

		public BuildingController( TycoonPlayer player ) : this()
		{
			Player = player;
			CreateGhostModel();
		}

		private void CreateGhostModel()
		{
			ghostBuilding = new GhostBuilding();
			ghostBuilding.Spawn();
		}

		[ServerCmd( "tg_start_building" )]
		public static void StartBuilding( string type )
		{
			if ( !Host.IsServer || ConsoleSystem.Caller is null )
				return;

			if ( Library.Exists<IBuildingType>( type ) == false )
				throw new ArgumentException( $"Can not start building with invalid building - {type}" );

			(ConsoleSystem.Caller.Pawn as TycoonPlayer).BuildingController.StartBuilding( Library.Create<IBuildingType>( type ) );
		}

		public void StartBuilding( IBuildingType type )
		{
			if ( ghostBuilding is null )
				CreateGhostModel();

			BuildingType = type;
			Player.LastBuiltType = BuildingType;

			ghostBuilding.SetModel( BuildingType.Model );
			ghostBuilding.SetupPhysicsFromModel( PhysicsMotionType.Static );

			PlacementUpdate();
			ghostBuilding.EnableDrawing = true;
		}

		public void StopBuilding()
		{
			if ( !IsBuilding )
				return;

			ghostBuilding.EnableDrawing = false;
			BuildingType = null;
		}

		public void Simulate()
		{
			if ( !IsBuilding )
				return;

			if ( Input.Pressed( InputButton.Attack1 ) && validPlacement )
			{
				ConfirmPlacement();
				return;
			}

			PlacementUpdate();
		}

		private void PlacementUpdate()
		{
			validPlacement = CONTROLLERS[BuildingType.PlacementType].PlacementUpdate( Player, BuildingType, ghostBuilding )
			                 && (!BuildingType.DoPlotBoundsCheck || IsInsidePlotBounds())
			                 && CheckPositionForCollisions();

			ghostBuilding.SetValid( validPlacement );
		}

		private void UpdateTiltColorBasedOnValidPlacement()
		{
			ghostBuilding.SetValid( validPlacement );
		}

		private bool IsInsidePlotBounds()
		{
			var plotBBox = PlotManager.Current.FindPlotForTeam( Player.PlotTeam ).GetBoundingBox();
			var ghostBBox = ghostBuilding.PhysicsBody.GetBounds();
			return ghostBBox.IsInside( plotBBox );
		}

		private void ConfirmPlacement()
		{
			var buildingEntity = new BuildingEntity();
			buildingEntity.Type = BuildingType;
			buildingEntity.Position = ghostBuilding.Position;
			buildingEntity.Rotation = ghostBuilding.Rotation;

			buildingEntity.Tags.Add( BuildingTag.BUILDING_TAG_DEFAULT );
			buildingEntity.Tags.Add( BuildingType.Tags );

			buildingEntity.Spawn();
		}

		private bool CheckPositionForCollisions()
		{
			var b = Physics.GetEntitiesInBox( ghostBuilding.PhysicsBody.GetBounds().ReduceBy( COLLISION_AREA_CHECK_REDUCE_BY ) )
				.OfType<ModelEntity>()
				.Where( me => me is TycoonPlayer or BuildingEntity )
				.ToList();

			return !b.Any();
		}
	}
}

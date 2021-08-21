using System.Collections.Generic;
using Sandbox;
using ThatTycoonGame.Entities.Player;
using ThatTycoonGame.Plot.Type;

namespace ThatTycoonGame.Plot
{
	public abstract class Plot : Entity
	{
		public abstract PlotTeam GetPlotTeam();

		public abstract IList<TycoonPlayer> GetPlayers();

		public abstract bool CanJoinTeam( TycoonPlayer player );

		public abstract void RemovePlayer( TycoonPlayer player );

		public abstract void AddPlayer( TycoonPlayer player );

		public abstract BBox GetBoundingBox();

		public abstract int PlayerCount();

		public abstract Vector3 RelativeSpawnPosition();

		public abstract Vector3 GetAbsolutePosition( Vector3 offset );

		public abstract Rotation GetAbsoluteRotation( Rotation rotation );
	}
}

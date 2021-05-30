using System.Collections.Generic;
using Sandbox;
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
	}
}

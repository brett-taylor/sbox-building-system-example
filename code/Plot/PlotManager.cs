using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ThatTycoonGame.Entities;
using ThatTycoonGame.Plot.Type;

namespace ThatTycoonGame.Plot
{
	public partial class PlotManager : Entity
	{
		public static PlotManager Current { get; private set; }
		[Net] public List<Plot> Plots { get; set; }

		public PlotManager()
		{
			Current = this;
			Plots = new List<Plot>();
			Transmit = TransmitType.Always;
		}

		public void CreatePlots( IEnumerable<PlotMapEntity> PlotEntities )
		{
			Plots = PlotEntities
				.Select( pe => new RealPlot(
					pe.PlotTeam,
					pe.Position,
					Rotation.FromYaw( pe.PlotRotation.y ),
					new BBox( pe.AbsoluteAreaMin, pe.AbsoluteAreaMax )
				) )
				.Cast<Plot>()
				.ToList();
		}

		public Plot FindPlotForTeam( PlotTeam team )
		{
			var plot = Plots.Where( plot => plot.GetPlotTeam() == team ).ToList();
			return plot.Count switch
			{
				0 => throw new Exception( $"Did not find plot for team {team}" ),
				> 1 => throw new Exception( $"Found too many plots for team {team}" ),
				_ => plot.First()
			};
		}
	}
}

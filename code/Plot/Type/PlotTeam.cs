using System;
using System.Collections.Generic;

namespace ThatTycoonGame.Plot.Type
{
	public enum PlotTeam
	{
		RED = 0,
		GREEN = 1,
		BLUE = 2,
		PURPLE = 3,
		PINK = 4,
		ORANGE = 5,
		UNASSIGNED = 9999,
	}

	public static class PlotTeamExtensions
	{
		private static readonly Dictionary<PlotTeam, PlotTeamDescription> teamDescriptions = new()
		{
			{PlotTeam.RED, new PlotTeamDescription( "Red Tycoon", new Color( 0.92F, 0.2470588F, 0.1568628F ) )},
			{PlotTeam.GREEN, new PlotTeamDescription( "Green Tycoon", new Color( 0.1333333F, 0.7019608F, 0.3215686F ) )},
			{PlotTeam.BLUE, new PlotTeamDescription( "Blue Tycoon", new Color( 0.2F, 0.509804F, 0.8392157F ) )},
			{PlotTeam.PURPLE, new PlotTeamDescription( "Purple Tycoon", new Color( r: 0.5294118F, 0.06666667F, 0.8392157F ) )},
			{PlotTeam.PINK, new PlotTeamDescription( "Pink Tycoon", Color.Parse( "#fc5dda" ).Value )},
			{PlotTeam.ORANGE, new PlotTeamDescription( "Orange Tycoon", new Color( 0.8901961F, 0.3333333F, 0.05490196F ) )},
			{PlotTeam.UNASSIGNED, new PlotTeamDescription( "Unassigned", Color.Gray )}
		};

		public static PlotTeamDescription GetDescription( this PlotTeam team )
		{
			if ( teamDescriptions.ContainsKey( team ) )
				return teamDescriptions[team];

			throw new Exception( $"PlotTeam {team} does not have a description." );
		}
	}
}

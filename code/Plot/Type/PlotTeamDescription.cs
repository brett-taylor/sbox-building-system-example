namespace ThatTycoonGame.Plot.Type
{
	public class PlotTeamDescription
	{
		public string Name { get; }
		public Color Color { get; }

		public PlotTeamDescription( string name, Color color )
		{
			Name = name;
			Color = color;
		}
	}
}

using Sandbox.UI;

namespace ThatTycoonGame.Ui.Scoreboard
{
	public class TycoonScoreboardRow : Panel
	{
		public Label FirstColumn { get; }
		public Label SecondColumn { get; }

		public TycoonScoreboardRow()
		{
			FirstColumn = AddChild<Label>();
			FirstColumn.SetClass( "first-column", true);
			SecondColumn = AddChild<Label>();
			SecondColumn.SetClass( "second-column", true);
		}
	}
}

using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ThatTycoonGame.Ui.WorldText
{
	public class WorldTextData
	{
		public Vector3 Position { get; set; } = Vector3.Zero;
		public string Text { get; set; } = "";
		public Color Color { get; set; } = Color.Red;
		public bool Show { get; set; } = true;
		public float MaxDistance { get; set; } = 1250;
		public float MinFontSize { get; set; } = 16f;
		public float MaxFontSize { get; set; } = 30f;

		internal SingularWorldText SingularWorldText { get; set; }
	}

	public class WorldText : Panel
	{
		internal readonly Dictionary<WorldTextData, SingularWorldText> WorldTexts;

		public WorldText()
		{
			WorldTexts = new Dictionary<WorldTextData, SingularWorldText>();
			StyleSheet.Load( "/Ui/WorldText/WorldText.scss" );
		}

		public WorldTextData CreateWorldText()
		{
			var wtd = new WorldTextData();
			var swt = AddChild<SingularWorldText>();
			swt.WorldTextData = wtd;
			wtd.SingularWorldText = swt;
			WorldTexts.Add( wtd, swt );
			return wtd;
		}

		public void DeleteWorldText( WorldTextData textData )
		{
			if ( WorldTexts.ContainsKey( textData ) )
				WorldTexts.Remove( textData );

			if ( textData.SingularWorldText != null )
				textData.SingularWorldText.Delete( true );
		}

		public override void Tick()
		{
			foreach ( var singularWorldText in WorldTexts.Values )
				singularWorldText.Update();
		}
	}

	internal class SingularWorldText : Panel
	{
		internal WorldTextData WorldTextData { get; set; }
		private readonly Label label;

		public SingularWorldText()
		{
			label = Add.Label( "Unset" );
			blah2 = blah++;
		}

		private static int blah = 0;
		private int blah2 = 0;

		public void Update()
		{
			if ( WorldTextData is null )
				return;

			var distance = WorldTextData.Position.Distance( CurrentView.Position );
			var outOfDistance = distance > WorldTextData.MaxDistance;
			var isVisible = !outOfDistance && WorldTextData.Show;
			if ( !isVisible )
			{
				Style.Opacity = 0f;
				Style.Dirty();
				return;
			}

			var alpha = distance.LerpInverse( WorldTextData.MaxDistance, WorldTextData.MaxDistance * 0.1f );
			Style.Opacity = alpha;

			label.Text = WorldTextData.Text;
			label.Style.FontColor = WorldTextData.Color;

			var position = WorldTextData.Position.ToScreen();
			Style.Left = Length.Fraction( position.x );
			Style.Top = Length.Fraction( position.y );
			label.Style.Left = 0;
			label.Style.Top = 0;
			label.Style.Right = 0;
			label.Style.Bottom = 0;

			var distancePercentage = (1 - (distance / WorldTextData.MaxDistance));
			var fontSizePercentage = distancePercentage * WorldTextData.MaxFontSize;
			var fontSizeClamped = fontSizePercentage.Clamp( WorldTextData.MinFontSize, WorldTextData.MaxFontSize );
			label.Style.FontSize = fontSizeClamped;

			var panelTransform = new PanelTransform();
			panelTransform.AddTranslateY( Length.Fraction( -1.0f ) );
			panelTransform.AddTranslateX( Length.Fraction( -0.5f ) );
			Style.Transform = panelTransform;
			Style.Dirty();
			label.Style.Dirty();
		}
	}
}

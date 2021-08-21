using Sandbox;
using ThatTycoonGame.Entities.Player;
using ThatTycoonGame.Ui;
using ThatTycoonGame.Ui.WorldText;

namespace ThatTycoonGame.Entities.Button
{
	[Library( "tg_floor_button" )]
	public class GenericFloorButton : ModelEntity
	{
		public override void Spawn()
		{
			base.Spawn();
			SetModel( "models/tg_floor_button.vmdl" );
			SetupPhysicsFromAABB( PhysicsMotionType.Static, new Vector3( -32f, -32f, 1f ), new Vector3( 32f, 32f, 10f ) );
			EnableTouch = true;
			CollisionGroup = CollisionGroup.Trigger;
		}

		public override void StartTouch( Entity other )
		{
			if ( other is not TycoonPlayer tp )
				return;

			DoTrigger( tp );
		}

		protected virtual void DoTrigger( TycoonPlayer tycoonPlayer )
		{
		}

		protected WorldTextData WorldTextData { get; private set; }
		protected virtual bool HasWorldText => false;
		protected virtual float WorldTextOffsetY => 40f;
		protected virtual Color WorldTextColor => Color.Red;
		protected virtual string WorldTextString => "Whoops";

		[Event.Tick.ClientAttribute]
		public void OnClientTick()
		{
			if ( HasWorldText )
			{
				WorldTextData ??= TycoonHudRootPanel.Current.WorldText.CreateWorldText();
				WorldTextData.Color = WorldTextColor;
				WorldTextData.Position = Position + (WorldTextOffsetY * Vector3.Up);
				WorldTextData.Text = WorldTextString;
				WorldTextData.Show = EnableDrawing;
			}

			DoClientTick();
		}

		protected virtual void DoClientTick()
		{
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			DestroyWorldText();
		}

		protected void DestroyWorldText()
		{
			if ( WorldTextData is null )
				return;

			TycoonHudRootPanel.Current?.WorldText.DeleteWorldText( WorldTextData );
		}
	}
}

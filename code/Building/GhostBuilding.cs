using Sandbox;

namespace ThatTycoonGame.Building
{
	public partial class GhostBuilding : ModelEntity
	{
		private static readonly int INVALID_PLACEMENT_MATERIAL_GROUP = 1;
		private static readonly int VALID_PLACEMENT_MATERIAL_GROUP = 2;

		public override void Spawn()
		{
			Transmit = TransmitType.Always;
			EnableDrawing = false;
			EnableAllCollisions = false;
			EnableSolidCollisions = false;
			EnableSelfCollisions = false;
			EnableHitboxes = false;
			EnableShadowCasting = false;

			SetMaterialGroup(INVALID_PLACEMENT_MATERIAL_GROUP);
		}

		public void SetValid( bool isValid )
		{
			SetMaterialGroup( isValid ? VALID_PLACEMENT_MATERIAL_GROUP : INVALID_PLACEMENT_MATERIAL_GROUP );
		}
	}
}

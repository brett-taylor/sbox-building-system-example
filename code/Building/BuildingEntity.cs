using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ThatTycoonGame.Building.Types;

namespace ThatTycoonGame.Building
{
	public partial class BuildingEntity : ModelEntity
	{
		public IBuildingType Type { get; set; }

		public override void Spawn()
		{
			if ( Type is null )
				return;

			SetModel( Type.Model );
			SetupPhysicsFromModel( PhysicsMotionType.Static );
			Transmit = TransmitType.Always;
		}

		public IEnumerable<Transform> GetAllSnapPointTransforms( string prefix )
		{
			var model = GetModel();
			return Enumerable
				.Range( 0, model.AttachmentCount )
				.Where( i => model.GetAttachmentName( i ).StartsWith( prefix ) )
				.Select( i => model.GetAttachment( i ).Value );
		}
	}
}

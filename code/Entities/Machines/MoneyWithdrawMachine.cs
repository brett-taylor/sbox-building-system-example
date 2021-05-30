using Sandbox;

namespace ThatTycoonGame.Entities.Machines
{
	[Library( "tg_money_withdraw_machine" )]
	public class MoneyWithdrawMachine : PlotEntity
	{
		public override void Spawn()
		{
			base.Spawn();
			SetModel( "models/tg_money_withdraw_machine/tg_money_withdraw_machine.vmdl" );
			SetupPhysicsFromModel( PhysicsMotionType.Static );

			Transmit = TransmitType.Always;
		}
	}
}

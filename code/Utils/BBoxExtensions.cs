namespace ThatTycoonGame.Utils
{
	public static class BBoxExtensions
	{
		public static bool IsInside( this BBox one, BBox two )
			=> one.Mins.IsInside( two ) && one.Maxs.IsInside( two );

		public static bool IsInside( this Vector3 point, BBox one )
			=> one.Mins.IsLessThan( point ) && point.IsLessThan( one.Maxs );

		public static bool IsLessThan( this Vector3 x, Vector3 y )
			=> x.x <= y.x && x.y <= y.y && x.z <= y.z;

		public static string Print( this BBox bBox )
			=> $"{bBox.Mins} {bBox.Maxs}";

		public static string SimplePrint( this BBox bBox )
			=> $"({bBox.Mins.SimplePrint()} | {bBox.Maxs.SimplePrint()})";

		public static string SimplePrint( this Vector3 vec )
			=> $"({vec.x:0}, {vec.y:0}, {vec.z:0})";

		public static BBox ReduceBy( this BBox box, float percentage )
		{
			return new(
				box.Mins.LerpTo( box.Maxs, percentage ),
				box.Maxs.LerpTo( box.Mins, percentage )
			);
		}
	}
}

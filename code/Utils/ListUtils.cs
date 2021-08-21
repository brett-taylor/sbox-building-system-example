using System.Collections.Generic;

namespace ThatTycoonGame.Utils
{
	public static class ListUtils
	{
		public static List<T> Of<T>( params T[] objects ) => new(objects);
		public static List<T> Empty<T>() => new();
	}
}

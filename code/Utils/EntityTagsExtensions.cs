using System.Collections.Generic;
using System.Linq;
using Sandbox.Internal;
using ThatTycoonGame.Building.Types;

namespace ThatTycoonGame.Utils
{
	public static class EntityTagsExtensions
	{
		public static void Add( this EntityTags et, params BuildingTag[] tags ) =>
			et.Add( tags.Select( tag => tag.ToString() ).ToArray() );

		public static void Add( this EntityTags et, IEnumerable<BuildingTag> tags ) =>
			et.Add( tags.Select( tag => tag.ToString() ).ToArray() );

		public static bool Has( this EntityTags et, BuildingTag tag ) =>
			et.Has( tag.ToString() );

		public static bool HasAtLeastOneOf( this EntityTags et, IEnumerable<BuildingTag> tag )
		{
			return tag
				.Select( st => Has( et, st ) )
				.Where( b => b is true )
				.Any( _ => true );
		}
	}
}

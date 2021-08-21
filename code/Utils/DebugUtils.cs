using Sandbox;

namespace ThatTycoonGame.Utils
{
	public static class DebugUtils
	{
		private static string ServerClientString()
		{
			var server = $"S:" + (Host.IsServer ? "T" : "F");
			var client = $"S:" + (Host.IsClient ? "T" : "F");
			return $"S:{server} C:{client}";
		}

		public static void ScreenTextWithServerClient( object text, float duration = 0f, int line = 0 ) =>
			ScreenText( $"{ServerClientString()} => {text}", duration, line );

		public static void ScreenText( object text, float duration = 0f, int line = 0 ) =>
			DebugOverlay.ScreenText( new Vector2( 50f, 50f ), line, Color.White, $"{text}", duration );

		public static void LogServerClient() => Log.Warning( $"{ServerClientString()}" );
		public static void LogServerClient( object message ) => Log.Warning( $"{ServerClientString()} => {message}" );

		public static void Box( BBox box, Color color ) =>
			DebugOverlay.Box( box.Mins, box.Maxs, color );
	}
}

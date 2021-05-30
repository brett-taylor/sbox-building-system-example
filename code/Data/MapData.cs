using System;
using System.Collections.Generic;
using Sandbox;

namespace ThatTycoonGame.Data
{
	public static class MapData
	{
		private static readonly string MAP_DATA_FOLDER = "data";
		private static readonly string MAP_DATA_FILE_POSTFIX = ".json";

		public static List<EntitySpawnPoint> SpawnPoints { get; private set; } = new();

		[ServerCmd( "tg_load_map_data" )]
		public static void LoadData()
		{
			if ( !Host.IsServer )
				return;

			if ( !FileSystem.Mounted.FileExists( GetMapDataPath() ) )
				throw new Exception( $"Could not find map data for {Global.MapName} - expected {GetMapDataPath()}" );

			SpawnPoints = FileSystem.Mounted.ReadJson<List<EntitySpawnPoint>>( GetMapDataPath() );
		}

		private static string GetMapDataPath()
		{
			return $"{MAP_DATA_FOLDER}/{Global.MapName.ToLower()}{MAP_DATA_FILE_POSTFIX}";
		}
	}
}

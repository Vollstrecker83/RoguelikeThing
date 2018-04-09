using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RoguelikeThing
{
    // Making a Singleton here to make sure we only ever make one of these.
    public sealed class TerrainManager
    {
        #region Member Variables
        static TerrainManager terrainManager = new TerrainManager();
        public static TerrainManager GetTerrainManager { get { return terrainManager; } }
        private static Dictionary<int, Terrain> mapList;
        private static int currentLevel;
        private static Point tileSize;
        #endregion

        #region Accessors/Mutators
        public static Dictionary<int, Terrain> MapList { get { return mapList; } set {  mapList = value; } }
        public static int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }
        public static Point TileSize { get { return tileSize; } set { tileSize = value; } }
        #endregion

        private TerrainManager()
        {
            // Initialize
            mapList = new Dictionary<int, Terrain>();

        }

        

        public static Terrain GetCurrentMap(int level)
        {
            Terrain foundTerrain;
            if (MapList.TryGetValue(level, out foundTerrain ))
                return foundTerrain;
            else
                throw new Exception("Failed to locate the current level in the TerrainManager!");            
        }

        public static Point GetMapSize(int level)
        {
            Terrain foundTerrain;
            if (MapList.TryGetValue(level, out foundTerrain))
                return foundTerrain.MapSize;
            else
                throw new Exception("Failed to retrieve the mapsize for level " + level + "!");
        }
    }
}

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
        public static TerrainManager GetTerrainManager => terrainManager;
        private Dictionary<int, Terrain> mapList;
        private int currentLevel;
        private Point tileSize;
        #endregion

        #region Accessors/Mutators
        public Dictionary<int, Terrain> MapList { get => mapList; set => mapList = value; }
        public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
        public Point TileSize { get => tileSize; set => tileSize = value; }
        #endregion

        TerrainManager()
        {
            // Initialize
            mapList = new Dictionary<int, Terrain>();

        }

        

        public Terrain GetCurrentMap(int level)
        {
            if (MapList.TryGetValue(level, out Terrain foundTerrain))
                return foundTerrain;
            else
                throw new Exception("Failed to locate the current level in the TerrainManager!");            
        }

        public Point GetMapSize(int level)
        {
            if (MapList.TryGetValue(level, out Terrain foundTerrain))
                return foundTerrain.MapSize;
            else
                throw new Exception("Failed to retrieve the mapsize for level " + level + "!");
        }
    }
}

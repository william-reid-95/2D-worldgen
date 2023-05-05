using MyUtilities;

namespace Tonbale
{
    public class World
    {
        //this object creates and holds a 2d array of tiles, and fills it with new tile instances

        public Tile[,] tiles;
        public int xMax { get { return tiles.GetLength(0); }} //returns the highest x value of the map (lowest x value is always 0)
        public int yMax { get { return tiles.GetLength(1); } } //returns the highest y value of the map (lowest y value is always 0)


        public World(int _sizeX, int _sizeY)
        {
            tiles = new Tile[_sizeX, _sizeY];

            for (int x = 0; x < _sizeX; x++)
            {
                for (int y = 0; y < _sizeY; y++)
                {
                    tiles[x, y] = new Tile(x,y);
                }
            }
        }

        public void PrintMap()
        {
            string output = "";
            for (int x = 0; x < xMax; x++)
            {
                for (int y = 0; y < yMax; y++)
                {
                    //TODO: colour the characters to reflect their biome 
                    output += tiles[x, y].biome.height.ToString();
                }
                output += "\n";
            }
            Console.WriteLine(output);
        }

        public void SmoothWorld(int smoothCount)
        {
            // create smooth transitions between tiles heights
            // such that the greatest difference in height between 2 tiles is not > 1
      
            foreach (Tile tile in tiles)
            {
                for (int i = 0; i < smoothCount; i++)
                {
                    //if tile is on the border, height = 0, else calculate height based on average height of surrounding tiles + own height
                    if (tile.xPos == 0 || tile.xPos >= xMax - 1 || tile.yPos == 0 || tile.yPos >= yMax - 1)
                    {
                        tile.biome.height = 0;
                    }
                    else
                    {
                        Tile[] adjacentTiles = new Tile[] {
                
                        //middle four tiles (orthanganly adjacent)
                        tiles[tile.xPos+1,tile.yPos],
                        tiles[tile.xPos-1,tile.yPos],
                        tiles[tile.xPos,tile.yPos+1],
                        tiles[tile.xPos,tile.yPos-1],

                        //corner four tiles (diagonally adjacent)
                        tiles[tile.xPos+1,tile.yPos+1],
                        tiles[tile.xPos-1,tile.yPos-1],
                        tiles[tile.xPos+1,tile.yPos-1],
                        tiles[tile.xPos-1,tile.yPos+1]};

                        tile.biome.height = GetTileHeightAverage(adjacentTiles, tile);
                    }

                }

                // generate tile data
                tile.GenerateTilePrefix();
                tile.GenerateSettlementOnTile();
                tile.GenerateCharactersOnTile();
            }

        }

        public int GetTileHeightAverage(Tile[] _adjacentTiles, Tile _tile)
        {
            int total = _tile.biome.height;

            foreach(Tile tile in _adjacentTiles)
            {
                total += tile.biome.height;
            }

            return total / (_adjacentTiles.Length + 1);
        }

    }
 
    public class Biome
    {
        public string name;
        public int height;

        //this dictionary could be a list, having the int values visible just helps with editing.
        public Dictionary<int, string> biomeMap = new Dictionary<int, string> {
            {0,"Ocean"},
            {1,"Coastline"},
            {2,"Shore"},
            {3,"Plains"},
            {4,"Forest"},
            {5,"Hills"},
            {6,"Mountains"}
        };

        public Biome()
        {
            this.height = RandomGen.RandomInt(0,biomeMap.Count);
            this.name = biomeMap[height];
        }
    }

    public class Tile
    {
        //a Tile is a spsace ond a 2D grid that contains some data

        // items on tile
        // characters on tile
        // description of tile

        public int xPos { get; private set; } //I only want the xPos and yPos fields to be writable in the constructor
        public int yPos { get; private set; }


        public string descriptionPrefix;
        public Biome biome;
        public Settlement? settlementOnTile;
        public List<Character>? charactersOnTile;


        public Tile(int _xPos, int _yPos)
        {
            this.xPos = _xPos;
            this.yPos = _yPos;
            descriptionPrefix = GenerateTilePrefix();
            biome = new Biome();

            Console.WriteLine($"Tile Created: {descriptionPrefix} {biome.name} @ {xPos},{yPos}");
        }

        public string GenerateTilePrefix()
        {
            var prefixes = LocationData.locationPrefixes;
            return prefixes[RandomGen.RandomInt(0,prefixes.Length)];
        }

        public void GenerateSettlementOnTile()
        {
            if(RandomGen.RandomBool())
            {
                settlementOnTile = new Settlement();

            }
        }

        public void GenerateCharactersOnTile()
        {
           // charactersOnTile = new Character();
        }

    }


    public enum SettlementType
    {
        farmstead,
        village,
        town,
        castle,
        ruins,
        dungeon
    }

    public class Settlement
    {

        // if a tile contains a settlement it becomes a Pont of interest and has some extended functionality

        public SettlementType settlementType;

        public string name;
        public string description;

        public Settlement()
        {
            //this.settlementType = SettlementType(Random());
        }
    }

    public static class LocationData{

        //this class will contain all the types of data to create permutations of when generating tiles

        public static string[] locationPrefixes = new string[] { 
            "Bleak",
            "Creepy",
            "Desolate",
            "Stormy",
            "Rocky",
            "Quiet",
            "Rainy",
            "Dry",
            "Creepy",
            "Misty",
            "Haunted"


        };

    }
}

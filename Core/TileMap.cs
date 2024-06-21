using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sorcery.Core
{
    public struct MapTile{
        public Tile tile;
        public Vector2 position;
        public int layer;

        public MapTile(Tile tile, Vector2 position, int layer){
            this.tile = tile;
            this.position = position;
            this.layer = layer;
        }
    }
    public class TileMap : Component
    {
        //represents the tilemap with
        //public Dictionary<Tile, Dictionary<Vector2, int>> tileMap = new Dictionary<Tile, Dictionary<Vector2, int>>();
        public List<MapTile> tileMap = new List<MapTile>();
        public float testFloat;

        public GameManager gameManager;

        public TileMap (string name): base(){
            this.name = name;
        }

        public void Draw(SpriteBatch spriteBatch){
            if(tileMap.Count > 0){
                foreach(var tile in tileMap){
                    //Console.WriteLine(tileMap[0].Keys[0]);
                    tile.tile.image.Draw(spriteBatch, tile.position);
                    //spriteBatch.Draw(tile.image.sprite, new Vector2(200, 200), Color.White);
                }
            }
        }
    }

    public class Tile{
        public Sprite image;

        public Tile(Sprite image){
            this.image = image;
        }
    }

    //public class RuleTile : Tile{
    //    public Dictionary<string, Vector2> directions;
//
    //    public RuleTile(Sprite image){
    //        this.image = image;
    //    }
    //}

    public class TileMapEditor{
        TileMap tileMap;
        public int currentLayer, amountOfLayers;

        Tile selectedTile;

        Vector2 mousePosition, tilePosition = new Vector2(0, 0);

        public TileMapEditor(TileMap tileMap){
            this.tileMap = tileMap;
        }

        public void AddTile(Tile tile, Vector2 position){
            //Dictionary<Vector2, int> positionLayer = new Dictionary<Vector2, int>();
            MapTile mapTile = new MapTile(tile, position, currentLayer);
            //positionLayer.Add(position, currentLayer);
            tileMap.tileMap.Add(mapTile);
        }

        public void SelectTile(Tile tile){
            selectedTile = tile;
        }

        public void Update(Vector2 mousePosition, bool leftClick, bool rightClick) {
            this.mousePosition = mousePosition;
            tilePosition.X = (int)(mousePosition.X);
            tilePosition.Y = (int)(mousePosition.Y);
            if (leftClick){
                AddTile(selectedTile, tilePosition);
            }
        }
    }
}
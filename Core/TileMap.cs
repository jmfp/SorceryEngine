using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sorcery.Core
{
    public class TileMap : Component
    {
        //represents the tilemap with
        public Dictionary<Tile, Dictionary<Vector2, int>> tileMap = new Dictionary<Tile, Dictionary<Vector2, int>>();

        public GameManager gameManager;

        //public TileMap (Dictionary<Tile, Dictionary<Vector2, int>> tileMap){
        //    this.tileMap = tileMap;
        //}

        public void Draw(SpriteBatch spriteBatch){
            if(tileMap.Count > 0){
                foreach(var tile in tileMap.Keys){
                    //Console.WriteLine(tileMap[0].Keys[0]);
                    tile.image.Draw(spriteBatch, new Vector2(200, 200));
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

        Vector2 mousePosition;

        public TileMapEditor(TileMap tileMap){
            this.tileMap = tileMap;
        }

        public void AddTile(Tile tile, Vector2 position){
            Dictionary<Vector2, int> positionLayer = new Dictionary<Vector2, int>();
            positionLayer.Add(position, currentLayer);
            tileMap.tileMap.Add(tile, positionLayer);
        }

        public void SelectTile(Tile tile){
            selectedTile = tile;
        }

        public void Update(Vector2 mousePosition, bool leftClick, bool rightClick) {
            this.mousePosition = mousePosition;
            if (leftClick && !tileMap.tileMap.ContainsKey(selectedTile)){
                AddTile(selectedTile, new Vector2(mousePosition.X / 16, mousePosition.Y / 16));
            }
        }
    }
}
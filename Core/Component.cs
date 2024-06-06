using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sorcery.Core{
    public abstract class Component{
    public string Name;
    public Component(){

    }

    public abstract void Update(GameTime gameTime, SpriteBatch spriteBatch);
    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}
}


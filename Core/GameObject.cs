using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Sorcery.Core{
    public class GameObject{
    public string name;
    public GameObject[] children;
    public GameObject parent;
    public Vector3 position;
    public Component[] components;
    public GameObject(string name, Vector3 position){
        this.name = name;
        this.position = position;
    }
}
}


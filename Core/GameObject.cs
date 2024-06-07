using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Numerics;
using System.Collections.Generic;
namespace Sorcery.Core{
    public class GameObject{
    public string name;
    public GameObject[] children;
    public GameObject parent;
    public System.Numerics.Vector3 position;
    public List<Component> components = new List<Component>();
    public GameObject(string name, System.Numerics.Vector3 position){
        this.name = name;
        this.position = position;
    }

    public void AddComponent(Component component) {
        components.Add(component);
    }
}
}


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using System.Numerics;
using System.Collections.Generic;
namespace Sorcery.Core{
    public class GameObject{
    public string name;
    public GameObject[] children;
    public GameObject parent;
    public Vector3 position;
    public List<Component> components = new List<Component>();
    public Rectangle Rectangle
    {
        get { return new Rectangle((int)position.X, (int)position.Y, 16, 16); }
    }
    public GameObject(string name, Vector3 position){
        this.name = name;
        this.position = position;
    }

    public void AddComponent(Component component) {
        component.parent = this;
        components.Add(component);
    }

    public Component GetComponent<T>(){
        for(int i = 0; i < components.Count; i++){
            if (components[i].GetType() == typeof(T)){
                return components[i];
            }
        }
        return null;
    }
}
}


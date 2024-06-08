using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sorcery.Core{
    public class Input : Component{
        public Vector2 movementVector = new Vector2(0, 0);
        public float maxSpeed;
        public float minSpeed;
        public Input(float maxSpeed) {
            //this.movementVector = movementVector;
            this.maxSpeed = maxSpeed;
            //setting minSpeed to maxSpeed on instantiation so that only if minSpeed < maxSpeed will the player speed up during input
            this.minSpeed = maxSpeed;
        }

        public void TopDown8(){
            if(Keyboard.GetState().IsKeyDown(Keys.A)) {
                movementVector.X = -1f;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D)) {
                movementVector.X = 1f;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.W)) {
                movementVector.Y = -1f;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S)) {
                movementVector.Y = 1f;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.A)) {
                movementVector.X = 0f;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.D)) {
                movementVector.X = 0f;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.W)) {
                movementVector.Y = 0f;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.S)) {
                movementVector.Y = 0f;
            }

            parent.position.X += movementVector.X * maxSpeed;
            parent.position.Y += movementVector.Y * maxSpeed;
        }
    }
}
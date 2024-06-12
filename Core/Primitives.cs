using OpenTK.Graphics.OpenGL4;

namespace Sorcery.Core{
    public class Triangle3D{
        //shader program
        int shaderProgram;
        //opentk in counter-clockwise
        public float[] vertices = {
            0f, 0.5f, 0f,
            -0.5f, -0.5f, 0f,
            0.5f, -0.5f, 0f,
        };

        //vertex array object
        int vao = GL.GenVertexArray();
        int vbo = GL.GenBuffer();

        public void Setup(){
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length*sizeof(float), vertices, BufferUsageHint.StaticDraw);
            //bind vao
            GL.BindVertexArray(vao);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexArrayAttrib(vao, 0);
            //unload vbo
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //unbind vao
            GL.BindVertexArray(0);
        }


    }
}
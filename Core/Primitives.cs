using OpenTK.Graphics.OpenGL4;

namespace Sorcery.Core{
    public class Triangle3D{
        //shader program
        public int shaderProgram;
        //opentk in counter-clockwise
        public float[] vertices = {
            0f, 0.5f, 0f,
            -0.5f, -0.5f, 0f,
            0.5f, -0.5f, 0f,
        };

        //vertex array object
        public int vao = GL.GenVertexArray();
        public int vbo = GL.GenBuffer();
        public int vertexShader, fragmentShader;

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

            //create shader program
            shaderProgram = GL.CreateProgram();
            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            Shader vert = new Shader("Default.vert");
            GL.ShaderSource(vertexShader, vert.source);
            GL.CompileShader(vertexShader);
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            Shader frag = new Shader("Default.frag");
            GL.ShaderSource(fragmentShader, frag.source);
            GL.CompileShader(fragmentShader);
        }
    }

    public class Plane3D{
        //shader program
        int shaderProgram;
        //opentk in clockwise
        public float[] vertices = {
            -0.5f, 0.5f, 0f, //top left
            0.5f, 0.5f, 0f, //top right
            0.5f, -0.5f, 0f, //bottom right
            -0.5f, -0.5f, 0f //bottom left
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
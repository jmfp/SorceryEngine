using System;
using System.IO;
using System.Reflection.Metadata;
using OpenTK.Graphics.OpenGL4;

namespace Sorcery.Core
{
	public class Shader: IDisposable
	{
		public int handle, shaderProgram;
		public string source;
        private bool disposedValue = false;

        public Shader(string filepath, ShaderType shaderType)
		{
			this.source = LoadShaderSource(filepath);
			shaderProgram = GL.CreateShader(shaderType);
			GL.ShaderSource(shaderProgram, source);
            GL.CompileShader(shaderProgram);

            GL.GetShader(shaderProgram, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(shaderProgram);
                Console.WriteLine(infoLog);
            }
            handle = GL.CreateProgram();

            GL.AttachShader(handle, shaderProgram);
            GL.LinkProgram(handle);

            GL.GetProgram(handle, GetProgramParameterName.LinkStatus, out int success2);
            if (success2 == 0)
            {
                string infoLog = GL.GetProgramInfoLog(handle);
                Console.WriteLine(infoLog);
            }
            //cleanup
            GL.DetachShader(handle, shaderProgram);
            GL.DeleteShader(shaderProgram);
        }

		public static string LoadShaderSource(string filepath)
		{
			string shaderSource = "";
			try
			{
				shaderSource = File.ReadAllText(filepath);
            }
			catch(Exception e)
			{
				Console.WriteLine("Failed to load source file for shader " + e.Message);
			}
			return shaderSource;
		}

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            if (disposedValue == false)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Use()
        {
            GL.UseProgram(handle);
        }
    }
}


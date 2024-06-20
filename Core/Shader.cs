using System;
using System.IO;

namespace Sorcery.Core
{
	public class Shader
	{
		public string source;
		public Shader(string filepath)
		{
			this.source = LoadShaderSource(filepath);
		}

		public static string LoadShaderSource(string filepath)
		{
			string shaderSource = "";
			try
			{
				using (StreamReader reader = new StreamReader("../../../Content/Shaders/" + filepath))
				{
					shaderSource = reader.ReadToEnd();
				}
			}
			catch(Exception e)
			{
				Console.WriteLine("Failed to load source file for shader " + e.Message);
			}
			return shaderSource;
		}
	}
}


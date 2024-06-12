using Newtonsoft.Json;

namespace Sorcery.Core{
    public class JSON{
        public string filePath;

        public JSON(string filePath) { 
            this.filePath = filePath;
        }

        //public void JSONRead<T>(){
        //    string jsonString = System.IO.File.ReadAllText("ProjectSettings.json");
        //    var deserializedBaseSettings = JsonConvert.DeserializeObject<typeof(T)>(jsonString);
        //    Console.WriteLine(deserializedBaseSettings.project_dimensions);
        //}
    }
}
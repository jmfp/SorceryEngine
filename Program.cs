class Program{
    static bool editor;
    static void Main(string[] args){
        if(editor){
            using var game = new Sorcery.Editor3D();
            game.Run();
        }else{
            using var game = new Sorcery.Game3D(800, 800);
            game.Run();
        }
    }
}



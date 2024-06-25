class Program{
    static bool editor = true;
    static void Main(string[] args){
        if(editor){
            using var game = new Sorcery.Editor();
            game.Run();
        }else{
            using var game = new Sorcery.Game3D(800, 800, "Sorcery");
            game.Run();
        }
    }
}



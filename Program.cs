using Core.Field;
using Match3;

namespace Core.Main
{
    class Program
    {
        [System.STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
}

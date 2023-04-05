using Microsoft.Xna.Framework;

namespace Core.Config
{
    public static class GameConfig
    {
        public static Vector2 WindowSize;
        public static Vector2 MiddlePoint => WindowSize / 2;
        public static Vector2 FieldContainerSize;// = new Vector2(400,400);
        public static int SelectSizeModifier;
        public static int GameFieldSize;
        public static int CellSize;
        public static int CellPadding;// = (int)FieldContainerSize.Length() / GameFieldSize;
    }
}
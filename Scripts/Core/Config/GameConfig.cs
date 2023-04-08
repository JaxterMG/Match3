using Core.Field;
using Microsoft.Xna.Framework;

namespace Core.Config
{
    public static class GameConfig
    {
        public static bool BlockInput;
        public static GameField Field;
        public static Vector2 WindowSize;
        public static Vector2 MiddlePoint => WindowSize / 2;
        public static Vector2 FieldContainerSize;
        public static int SelectSizeModifier;
        public static int GameFieldSize;
        public static int CellSize;
        public static int CellPadding;
    }
}
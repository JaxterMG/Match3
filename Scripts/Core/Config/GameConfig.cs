using Core.Field;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Config
{
    public static class GameConfig
    {
        public static Texture2D DefaultCell;
        public static Texture2D HorizontalLineCell;
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
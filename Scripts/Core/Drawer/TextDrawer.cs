using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Drawer
{
    public abstract class TextDrawer
    {
        public SpriteFont Font;
        public Vector2 ScreenPos;
        public Vector2 TextSize;
        public virtual void Initialize(Vector2 screenPos)
        {
            ScreenPos = screenPos;
        }

        public virtual void LoadFont(ContentManager contentManager, string fontName = "Fonts/Arial")
        {
            Font = contentManager.Load<SpriteFont>(fontName);
        }

        public virtual void DrawText(SpriteBatch spriteBatch,  string text, Color color)
        {
            spriteBatch.DrawString(Font, text, ScreenPos, color);
        }

        public Vector2 MeasureText(string text)
        {
            TextSize = Font.MeasureString(text);
            return TextSize;
        }
    }
}
using Core.Config;
using Core.Field;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Drawer.CellElement
{
    public class ButtonDrawer : Drawer
    {
        public Rectangle ButtonRectangle;

        public void InitializeScreenPos(Vector2 screenPos, int size)
        {
            ButtonRectangle = new Rectangle
            (
                (int)screenPos.X - size / 2,
                (int)screenPos.Y - size / 2,
                size,
                size
            );
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D cellTexture, Vector2 screenPos, int size)
        {
            spriteBatch.Draw(cellTexture, ButtonRectangle, Color.White);
        }

        public override void Draw(GameField gameField, SpriteBatch spriteBatch, Texture2D cellTexture)
        {
            throw new System.NotImplementedException();
        }
    }
}
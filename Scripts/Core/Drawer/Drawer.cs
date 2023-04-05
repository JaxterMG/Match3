using Core.Field;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Drawer
{
    public abstract class Drawer
    {
        public abstract void Draw(SpriteBatch spriteBatch, Texture2D cellTexture, Vector2 screenPos, int size);
        public abstract void Draw(GameField gameField, SpriteBatch spriteBatch, Texture2D cellTexture);
    }
}
using Core.Config;
using Core.Field;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Drawer.CellElement
{
    public class ContainerDrawer : Drawer
    {
        public Rectangle FieldContainerRect {get; private set;}
        public override void Draw(SpriteBatch spriteBatch, Texture2D cellTexture, Vector2 screenPos, int size)
        {
            spriteBatch.Draw(cellTexture, FieldContainerRect, Color.White);
        }

        public void SetupRectangle()
        {
            FieldContainerRect = new Rectangle
            (
                (int)GameConfig.MiddlePoint.X - (int)GameConfig.FieldContainerSize.X / 2,
                (int)GameConfig.MiddlePoint.Y - (int)GameConfig.FieldContainerSize.Y / 2,
                (int)GameConfig.FieldContainerSize.X,
                (int)GameConfig.FieldContainerSize.Y
            );
        }

        public override void Draw(GameField gameField, SpriteBatch spriteBatch, Texture2D cellTexture)
        {
            throw new System.NotImplementedException();
        }
    }
}
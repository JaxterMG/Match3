using Core.Config;
using Core.Field;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Drawer.Field
{
    public class FieldDrawer : Drawer
    {
        public override void Draw(GameField gameField, SpriteBatch spriteBatch, Texture2D cellTexture)
        {
            for (int x = 0; x < gameField.Width; x++)
            {
                for (int y = 0; y < gameField.Height; y++)
                {
                    var cellRect = new Rectangle
                    (
                        (int)gameField.Field[x, y].ScreenPos.X,
                        (int)gameField.Field[x, y].ScreenPos.Y,
                        gameField.Field[x, y].Size, gameField.Field[x, y].Size
                    );

                    spriteBatch.Draw(cellTexture, cellRect, Color.Gray);
                    var cellElementRect = new Rectangle
                    (
                        (int)gameField.Field[x, y].ScreenPos.X + 3,
                        (int)gameField.Field[x, y].ScreenPos.Y + 3,
                        gameField.Field[x, y].CellElement.Size - 6, gameField.Field[x, y].CellElement.Size - 6
                    );

                    spriteBatch.Draw(cellTexture, cellElementRect, gameField.Field[x, y].CellElement.Color);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D cellTexture, Vector2 screenPos, int size)
        {
            throw new System.NotImplementedException();
        }

        public bool InitializeScreenPos(GameField gameField, Rectangle fieldContainerRect, int cellSize)
        {
            for (int x = 0; x < gameField.Width; x++)
            {
                for (int y = 0; y < gameField.Height; y++)
                {
                    var cellRect = new Rectangle
                    (
                        fieldContainerRect.Width / gameField.Width + fieldContainerRect.X - x * cellSize,
                        fieldContainerRect.Height / gameField.Height + fieldContainerRect.Y - y * cellSize,
                        GameConfig.CellSize, GameConfig.CellSize
                    );
                    gameField.Field[x, y].ScreenPos = new Vector2(cellRect.X, cellRect.Y);

                }
            }
            return true;
        }
    }
}

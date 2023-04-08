using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Cells
{
    public class EmptyCellElement : CellElement
    {
        public EmptyCellElement(CellType cellType, Color color, Texture2D cellSprite, int x, int y, int size, Vector2 screenPos) : base(cellType, color, cellSprite, x, y, size, screenPos)
        {
        }

        public override void PopElement()
        {
            //throw new System.NotImplementedException();
        }
    }
}

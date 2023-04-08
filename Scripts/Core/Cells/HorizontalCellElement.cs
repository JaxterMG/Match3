using Core.Points;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Cells
{
    public class HorizontalCellElement : SpecialCellElement
    {
        public HorizontalCellElement(CellType cellType, Color color, Texture2D cellSprite, int x, int y, int size, Vector2 screenPos) : base(cellType, color, cellSprite, x, y, size, screenPos)
        {
            _points = 2;
        }

        public override void PopElement()
        {
            base.PopElement();
        }
    }
}

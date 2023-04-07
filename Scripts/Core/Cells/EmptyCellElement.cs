using Microsoft.Xna.Framework;

namespace Core.Cells
{
    public class EmptyCellElement : CellElement
    {
        public EmptyCellElement(CellType cellType, Color color, int x, int y, int size, Vector2 screenPos) : base(cellType, color, x, y, size, screenPos)
        {
        }

        public override void PopElement()
        {
            throw new System.NotImplementedException();
        }
    }
}

using Core.Points;
using Microsoft.Xna.Framework;

namespace Core.Cells
{
    public class DefaultCellElement : CellElement
    {
        private int _points = 1; 
        public DefaultCellElement(CellType cellType, Color color, int x, int y, int size, Vector2 screenPos) : base(cellType, color, x, y, size, screenPos)
        {
        }

        public override void PopElement()
        {
            PointsCounter.AddPoints(_points);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Cells
{
    public class SpecialCellElement : CellElement
    {
        public SpecialCellElement(CellType cellType, Color color, Texture2D cellSprite, int x, int y, int size, Vector2 screenPos) : base(cellType, color, cellSprite, x, y, size, screenPos)
        {
        }

        public override void PopElement()
        {
            base.PopElement();
        }
    }
}
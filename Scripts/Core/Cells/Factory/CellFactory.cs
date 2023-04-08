using System;
using Core.Config;
using Microsoft.Xna.Framework;

namespace Core.Cells.Factory
{
    public static class CellFactory
    {
        public static CellElement CreateCellElement(CellType cellType, Color color, int x, int y, Vector2 screenPos)
        {
            switch (cellType)
            {
                case CellType.Default:
                    return new DefaultCellElement(cellType, color, GameConfig.DefaultCell, x, y, GameConfig.CellSize, screenPos);
                case CellType.HorizontalLine:
                    return new HorizontalCellElement(cellType, color, GameConfig.HorizontalLineCell, x, y, GameConfig.CellSize, screenPos);
                // case CellType.VerticalLine
                //     return new DefaultCellElement(cellType, Color, x, y, GameConfig.CellSize, screenPos);
                // case CellType.Bomb
                //     return new DefaultCellElement(cellType, Color, x, y, GameConfig.CellSize, screenPos);
                // case CellType.Destroyer
                //     return new DefaultCellElement(cellType, Color, x, y, GameConfig.CellSize, screenPos);
                case CellType.Empty:
                    return new EmptyCellElement(CellType.Empty, Color.Gray, GameConfig.DefaultCell, x, y, GameConfig.CellSize, screenPos);
                default:
                    throw new ArgumentException($"Invalid cell type '{cellType}'", nameof(cellType));
            }
        }
    }
}
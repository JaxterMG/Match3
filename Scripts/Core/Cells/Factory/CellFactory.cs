using System;
using Core.Config;
using Microsoft.Xna.Framework;

namespace Core.Cells.Factory
{
    public static class CellFactory
    {
        public static CellElement CreateCellElement(CellType cellType, int x, int y, Vector2 screenPos)
        {
            switch (cellType)
            {
                case CellType.Red:
                    return new RedCellElement(cellType, Color.Red, x, y, GameConfig.CellSize, screenPos);
                case CellType.Green:
                    return new GreenCellElement(cellType, Color.Green, x, y, GameConfig.CellSize, screenPos);
                case CellType.Blue:
                    return new BlueCellElement(cellType, Color.Blue, x, y, GameConfig.CellSize, screenPos);
                case CellType.Yellow:
                    return new BlueCellElement(cellType, Color.Yellow, x, y, GameConfig.CellSize, screenPos);
                case CellType.Pink:
                    return new BlueCellElement(cellType, Color.Pink, x, y, GameConfig.CellSize, screenPos);
                case CellType.Empty:
                    return new EmptyCellElement(cellType, Color.Gray, x, y, GameConfig.CellSize, screenPos);
                default:
                    throw new ArgumentException($"Invalid cell type '{cellType}'", nameof(cellType));
            }
        }
    }
}
using System;
using Core.Cells;
using Core.Cells.Factory;
using Microsoft.Xna.Framework;
using Core.Config;

namespace Core.Field
{
    public class GameField
    {
        public readonly Cell[,] Field;
        public readonly int Width;
        public readonly int Height;

        public Vector2 StartPos;

        public GameField(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            Field = new Cell[width, height];
            FillBoard();
            FindNeighbours();
        }

        private void FillBoard()
        {
            Random random = new Random();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Field[x, y] = new Cell(x, y, GameConfig.CellSize, Vector2.Zero);
                    Array values = Enum.GetValues(typeof(CellType));
                    int index = random.Next(values.Length);
                    CellType desiredCellType = (CellType)values.GetValue(index);
                    Field[x, y].CellElement = CellFactory.CreateCellElement(desiredCellType, x, y, Vector2.Zero);
                }
            }
        }
        private void FindNeighbours()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Field[x, y].FindNeighbours(Field);
                }
            }
        }
    }
}
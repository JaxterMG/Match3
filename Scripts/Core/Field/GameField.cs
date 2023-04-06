using System;
using Core.Cells;
using Core.Cells.Factory;
using Microsoft.Xna.Framework;
using Core.Config;
using System.Threading.Tasks;

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
                    int index = random.Next(values.Length - 1);
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
        public async Task ShiftGrid()
        {
            for (int x = 0; x < Field.GetLength(0); x++)
            {
                var emtpyCels = 0;
                for (int y = Field.GetLength(1) - 1; y >= 0; y--)
                {

                    if (Field[x, y].CellElement.CellType == CellType.Empty)
                    {
                        emtpyCels++;
                    }
                    else if (emtpyCels > 0 && Field[x, y].CellElement.CellType != CellType.Empty)
                    {
                        Field[x, y + emtpyCels].CellElement = Field[x, y].CellElement;
                        Field[x, y].CellElement = CellFactory.CreateCellElement(CellType.Empty, x, y, Field[x, y].ScreenPos);

                    }
                }
                await Task.Delay(100);
            }
            await GameConfig.Field.GenerateNewCellElements();
        }
        public async Task GenerateNewCellElements()
        {
            Random random = new Random();
            var emptyCells = 0;

            for (int x = 0; x < Width; x++)
            {
                if (Field[x, 0].CellElement.CellType != CellType.Empty) continue;
                emptyCells++;
                Array values = Enum.GetValues(typeof(CellType));
                int index = random.Next(values.Length - 1);
                CellType desiredCellType = (CellType)values.GetValue(index);
                Field[x, 0].CellElement = CellFactory.CreateCellElement(desiredCellType, x, 0, Vector2.Zero);
            }
            if (emptyCells > 0)
                await ShiftGrid();
        }
    }
}

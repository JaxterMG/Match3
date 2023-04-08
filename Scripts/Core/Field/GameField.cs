using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices;
using System;
using Core.Cells;
using Core.Cells.Factory;
using Microsoft.Xna.Framework;
using Core.Config;
using System.Threading.Tasks;
using MatchLogic;

namespace Core.Field
{
    public class GameField
    {
        public readonly Cell[,] Field;
        public readonly int Width;
        public readonly int Height;

        public Vector2 StartPos;
        private Color[] _colors;
        public GameField(int width, int height)
        {
            _colors = new Color[5]{Color.Red, Color.Blue, Color.Green, Color.Pink, Color.Gray};
            GameConfig.Field = this;

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
                    int index = random.Next(_colors.Length - 1);
                    Field[x, y].CellElement = CellFactory.CreateCellElement(CellType.Default, _colors[index], x, y, Vector2.Zero);
                }
            }
        }
        private async void FindNeighbours()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Field[x, y].FindNeighbours(Field);
                }
            }
           await CheckFieldMatches();
        }
       public async Task CheckFieldMatches()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if(Field[x, y].CellElement.CellType == CellType.Empty) continue;
                    await MatchFinder.FindMatches(Field[x, y]);
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
                        Field[x, y].CellElement = CellFactory.CreateCellElement(CellType.Empty, Color.Gray, x, y, Field[x, y].ScreenPos);
                    }
                }
                    await Task.Delay(100);
            }
        }
        public async Task GenerateNewCellElements()
        {
            Random random = new Random();
            var emptyCells = 0;
            for (int x = 0; x < Width; x++)
            {
                if (Field[x, 0].CellElement.CellType != CellType.Empty) continue;
                emptyCells++;
                int index = random.Next(_colors.Length-1);
                Field[x, 0].CellElement = CellFactory.CreateCellElement(CellType.Default, _colors[index], x, 0, Vector2.Zero);
            }
                await Task.Delay(5);
        }
    }
}

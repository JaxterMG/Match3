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
            _colors = new Color[5] { Color.Red, Color.Blue, Color.Green, Color.Pink, Color.Gray };
            GameConfig.Field = this;

            this.Width = width;
            this.Height = height;
            Field = new Cell[width, height];
            FillBoard();
            FindNeighbours();
            StartCheck(Field[0, 0], 0);
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
        public async void StartCheck(Cell startingCell, int delay)
        {
            while (true)
            {
                GameConfig.BlockInput = true;
                
                List<List<Cell>> matches = MatchFinder.FindMatches(this, startingCell);

                if (matches.Count == 0) break;

                await ClearMatches(matches, delay);
                await ShiftGrid(delay);
                GenerateNewCellElements();
                startingCell = Field[0,0];
            }
            GameConfig.BlockInput = false;
        }
        private async Task ClearMatches(List<List<Cell>> matches, int delay)
        {
            foreach (var match in matches)
            {
                foreach (var cell in match)
                {
                    cell.ClearCell();
                }
                await Task.Delay(delay);
            }
        }

        private async Task ShiftGrid(int delay)
        {
            for (int x = 0; x < Field.GetLength(0); x++)
            {
                var emptyCells = 0;
                for (int y = Field.GetLength(1) - 1; y >= 0; y--)
                {
                    if (Field[x, y].CellElement.CellType == CellType.Empty)
                    {
                        emptyCells++;
                    }
                    else if (emptyCells > 0 && Field[x, y].CellElement.CellType != CellType.Empty)
                    {
                        Field[x, y + emptyCells].CellElement = Field[x, y].CellElement;
                        Field[x, y].CellElement = CellFactory.CreateCellElement(CellType.Empty, Color.Gray, x, y, Field[x, y].ScreenPos);
                    }
                }
                if (emptyCells > 0)
                {
                    for (int i = 0; i < emptyCells; i++)
                    {
                        int index = new Random().Next(_colors.Length - 1);
                        Field[x, i].CellElement = CellFactory.CreateCellElement(CellType.Default, _colors[index], x, i, Vector2.Zero);
                    }
                }
                await Task.Delay(delay);
            }
        }

        public void GenerateNewCellElements()
        {
            Random random = new Random();
            while (true)
            {
                bool foundEmptyCell = false;
                for (int x = 0; x < Width; x++)
                {
                    if (Field[x, 0].CellElement.CellType == CellType.Empty)
                    {
                        foundEmptyCell = true;
                        int index = random.Next(_colors.Length);
                        Field[x, 0].CellElement = CellFactory.CreateCellElement(CellType.Default, _colors[index], x, 0, Vector2.Zero);
                    }
                }
                if (!foundEmptyCell) break;
            }
        }
    }
}

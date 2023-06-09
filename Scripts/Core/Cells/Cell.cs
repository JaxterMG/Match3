using System.Collections.Generic;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Core.Field;
using Core.Points;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Cells
{
    public class Cell
    {
        public readonly List<Cell> Neighbours;
        private CellElement _cellElement;
        
        public CellElement CellElement
        {
            get
            {
                return _cellElement;
            }
            set
            {
                _cellElement = value;
                _cellElement.Position = Position;
                _cellElement.ScreenPos = ScreenPos;
            }
        }
        public Vector2 ScreenPos { get; set; }
        public Vector2 Position { get; set; }
        public int Size { get; set; }
        public Cell(int x, int y, int size, Vector2 screenPos)
        {
            Neighbours = new List<Cell>();
            ScreenPos = screenPos;
            Position = new Vector2(x, y);
            Size = size;
        }
        public void FindNeighbours(Cell[,] gameField)
        {
            int x = (int)Position.X;
            int y = (int)Position.Y;

            if (x > 0)
            {
                Neighbours.Add(gameField[x - 1, y]); // Left Neighbour
            }

            if (x < gameField.GetLength(0) - 1)
            {
                Neighbours.Add(gameField[x + 1, y]); // Right Neighbour
            }

            if (y > 0)
            {
                Neighbours.Add(gameField[x, y - 1]); // Down Neighbour
            }

            if (y < gameField.GetLength(1) - 1)
            {
                Neighbours.Add(gameField[x, y + 1]); // Up Neighbour
            }
        }

        public void ClearCell()
        {
            CellElement.PopElement();
            CellElement = Factory.CellFactory.CreateCellElement(CellType.Empty, Color.Gray, (int)Position.X, (int)Position.Y, ScreenPos);
        }
    }
    public abstract class CellElement
    {
        public CellType CellType;
        public Color Color { get; set; }
        public Vector2 ScreenPos { get; set; }
        public Vector2 Position { get; set; }
        public int Size { get; set; }
        protected int _points;
        public Texture2D CellSprite;
        public CellElement(CellType cellType, Color color, Texture2D cellSprite, int x, int y, int size, Vector2 screenPos)
        {
            CellSprite = cellSprite;
            ScreenPos = screenPos;
            Position = new Vector2(x, y);
            CellType = cellType;
            Color = color;
            Size = size;
        }
        public virtual void PopElement()
        {
            PointsCounter.AddPoints(_points);
        }
    }
}

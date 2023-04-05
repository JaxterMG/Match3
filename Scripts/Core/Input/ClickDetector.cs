using System.Collections.Generic;
using Core.Cells;
using Core.Config;
using Core.Field;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Core.Input.Detection
{
    public class ClickDetector
    {
        GameField _gameField;
        Cell _selectedCell = null;
        Cell _prevSelectedCell = null;

        public ClickDetector(GameField gameField)
        {
            _gameField = gameField;
        }

        public void CheckRectangle()
        {
            MouseState mouseState = Mouse.GetState();

            for (int x = 0; x < _gameField.Width; x++)
            {
                for (int y = 0; y < _gameField.Height; y++)
                {
                    Rectangle rect = new Rectangle((int)_gameField.Field[x, y].ScreenPos.X, (int)_gameField.Field[x, y].ScreenPos.Y, _gameField.Field[x, y].Size, _gameField.Field[x, y].Size);

                    if (!rect.Contains(mouseState.Position)) continue;
                    if (_selectedCell == null && _prevSelectedCell != null)
                    {
                        _prevSelectedCell = null;
                        //break;
                    }
                    _selectedCell = _gameField.Field[x, y];

                    if (_selectedCell == _prevSelectedCell) break;

                    _selectedCell.CellElement.Size += GameConfig.SelectSizeModifier;
                    if (_prevSelectedCell != null)
                    {
                        _prevSelectedCell.CellElement.Size -= GameConfig.SelectSizeModifier;
                        _selectedCell.CellElement.Size -= GameConfig.SelectSizeModifier;

                        SwapElements();
                        _selectedCell = null;
                        break;
                    }

                    _prevSelectedCell = _gameField.Field[x, y];
                    break;
                }
            }
        }
        private void SwapElements()
        {
            List<CellElement> chain = new List<CellElement>();
            foreach (var neighbour in _prevSelectedCell.Neighbours)
            {
                if (neighbour.CellElement.Equals(_selectedCell.CellElement))
                {
                    var selectedPos = _selectedCell.Position;
                    _selectedCell.CellElement = _gameField.Field[(int)_prevSelectedCell.Position.X, (int)_prevSelectedCell.Position.Y].CellElement;
                    //_selectedCell.FindMatch(_selectedCell.CellElement, chain);
                    _prevSelectedCell.CellElement = _gameField.Field[(int)selectedPos.X, (int)selectedPos.Y].CellElement;
                    chain.Add(_selectedCell.CellElement);
                    _selectedCell.FindMatch(_selectedCell.CellElement, chain);
                    //_prevSelectedCell.FindMatch(_prevSelectedCell.CellElement, chain);
                    
                    break;
                }
            }


        }

    }
}

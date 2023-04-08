using Core.Cells;
using Core.Config;
using Core.Field;
using MatchLogic;
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
                    if (_gameField.Field[x, y].CellElement.CellType == CellType.Empty) continue;
                    if (_selectedCell == null && _prevSelectedCell != null)
                    {
                        _prevSelectedCell = null;
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
            foreach (var neighbour in _prevSelectedCell.Neighbours)
            {
                if (neighbour.CellElement.Equals(_selectedCell.CellElement))
                {
                    CellElement temp = _selectedCell.CellElement;
                    _selectedCell.CellElement = _prevSelectedCell.CellElement;
                    _prevSelectedCell.CellElement = temp;
                    var selectedCellMatches = MatchFinder.FindMatchesForCell(_gameField, _selectedCell);

                    var prevSelectedCellMatches = MatchFinder.FindMatchesForCell(_gameField, _prevSelectedCell);

                    if (selectedCellMatches.Count >= 3 || prevSelectedCellMatches.Count >= 3)
                    {
                        _gameField.StartCheck(_selectedCell, 100);
                        _gameField.StartCheck(_prevSelectedCell, 100);
                    }
                    else
                    {
                        CellElement backTemp = _selectedCell.CellElement;
                        _selectedCell.CellElement = _prevSelectedCell.CellElement;
                        _prevSelectedCell.CellElement = backTemp;
                    }

                    break;
                }

            }
        }

    }
}

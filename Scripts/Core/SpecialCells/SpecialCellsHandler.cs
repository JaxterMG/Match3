using Core.Cells;
using Core.Cells.Factory;
using Microsoft.Xna.Framework;

namespace Core.SpecialCells
{
    public class SpecialCellsHandler
    {
        public bool HasSpecialConditions { get; private set; }
        public bool HasHorizontalLineCondition { get; private set; }
        public bool HasVerticalLineCondition { get; private set; }
        public bool HasBombCondition { get; private set; }
        public void CheckForSpecialConditions(int horizontalMatchesCount, int verticalMatchesCount)
        {
            HasSpecialConditions = CheckForHorizontalLinePossibility(horizontalMatchesCount);
            //HasSpecialConditions = CheckForVerticalLinePossibility(verticalMatchesCount);
            //HasSpecialConditions = CheckForBombPossibility(horizontalMatchesCount, verticalMatchesCount);
        }
        private bool CheckForHorizontalLinePossibility(int horizontalMatchesCount)
        {
            if(horizontalMatchesCount == 4)
            {
                HasHorizontalLineCondition = true;
                return true;
            }
            return false;
        }
        private bool CheckForVerticalLinePossibility(int verticalMatchesCount)
        {
            if(verticalMatchesCount == 4)
            {
                HasVerticalLineCondition = true;
                return true;
            }
            return false;
        }
        private bool CheckForBombPossibility(int horizontalMatchesCount, int verticalMatchesCount)
        {
            if(horizontalMatchesCount == 5 || verticalMatchesCount == 5)
            {
                HasBombCondition = true;
                return true;
            }
            return false;
        }
        public void RequestSpecialCell(Cell cellToChange, Color color)
        {
            if (HasHorizontalLineCondition)
            {
                cellToChange.CellElement = CellFactory.CreateCellElement
                (
                    CellType.HorizontalLine, color,
                    (int)cellToChange.Position.X,
                    (int)cellToChange.Position.Y,
                    cellToChange.ScreenPos
                );
                HasHorizontalLineCondition = false;
            }
            if (HasVerticalLineCondition)
            {
                cellToChange.CellElement = CellFactory.CreateCellElement
                (
                    CellType.VerticalLine, color,
                    (int)cellToChange.Position.X,
                    (int)cellToChange.Position.Y,
                    cellToChange.ScreenPos
                );
                HasVerticalLineCondition = false;
            }
            if (HasBombCondition)
            {
                cellToChange.CellElement = CellFactory.CreateCellElement
                (
                    CellType.Bomb, color,
                    (int)cellToChange.Position.X,
                    (int)cellToChange.Position.Y,
                    cellToChange.ScreenPos
                );
                HasBombCondition = false;
            }
        }

    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Cells;
using Core.Config;
using Core.Field;

namespace MatchLogic
{
    public static class MatchFinder
    {
        public static List<List<Cell>> FindMatches(GameField gameField, Cell startCell)
        {
            HashSet<Cell> visitedCells = new HashSet<Cell>();
            List<List<Cell>> matches = new List<List<Cell>>();
            if (startCell.CellElement.CellType == CellType.Empty)
                return matches;
            bool[,] visited = new bool[gameField.Field.GetLength(0), gameField.Field.GetLength(1)];
            int startX = (int)startCell.Position.X;
            int startY = (int)startCell.Position.Y;
            for (int i = startX; i < gameField.Field.GetLength(0); i++)
            {
                for (int j = startY; j < gameField.Field.GetLength(1); j++)
                {
                    Cell currentCell = gameField.Field[i, j];
                    if (!visited[i, j] && !visitedCells.Contains(currentCell) && currentCell.CellElement.CellType != CellType.Empty)
                    {
                        List<Cell> match = new List<Cell>();
                        match.Add(currentCell);
                        visited[i, j] = true;
                        visitedCells.Add(currentCell);
                        StartFindingRecursive(currentCell, match);
                        if (match.Count >= 3)
                        {
                            matches.Add(match);
                        }
                    }
                }
            }
            return matches;
        }

        public static List<Cell> FindMatchesForCell(GameField gameField, Cell startCell)
        {
            List<Cell> matches = new List<Cell>();
            if (startCell.CellElement.CellType == CellType.Empty)
                return matches;

            StartFindingRecursive(startCell, matches);

            return matches;
        }


        public static void StartFindingRecursive(Cell currentCell, List<Cell> match)
        {
            foreach (var neighbour in currentCell.Neighbours)
            {
                if (match.Contains(neighbour)) continue;
                if (neighbour.CellElement.CellType == CellType.Empty) continue;

                if (neighbour.CellElement.Color.Equals(currentCell.CellElement.Color))
                {
                    match.Add(neighbour);
                    StartFindingRecursive(neighbour, match);
                }
            }
        }
    }
}

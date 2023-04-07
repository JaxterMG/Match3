using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Cells;
using Core.Config;

namespace MatchLogic
{
    public static class MatchFinder
    {
        public static List<Cell> FindMatches(Cell currentCell)
        {
            List<Cell> matches = new List<Cell>();

            matches.Add(currentCell);
            StartFindingRecursive(currentCell, currentCell, matches);
            if (matches.Count >= 3)
            {
                var xGroups = matches.GroupBy(item => item.Position.X)
                   .Where(group => group.Count() > 2);

                var yGroups = matches.GroupBy(item => item.Position.Y)
                                   .Where(group => group.Count() > 2);

                var intersectingItems = xGroups.SelectMany(group => group)
                                               .Union(yGroups.SelectMany(group => group));

                var newItems = matches.Where(item =>
                {
                    if (intersectingItems.Contains(item))
                    {
                        return true;
                    }

                    else if (xGroups.Any(group => group.Key == item.Position.X && group.Count() > 2)
                             || yGroups.Any(group => group.Key == item.Position.Y && group.Count() > 2))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }).ToList();
                matches = newItems;
                
            }
            return matches;
        }

        public static void StartFindingRecursive(Cell currentCell, Cell exceptionCell, List<Cell> matches)
        {
            foreach (var neighbour in currentCell.Neighbours)
            {
                if (matches.Contains(neighbour)) continue;

                if (neighbour.CellElement.Color.Equals(currentCell.CellElement.Color))
                {
                    matches.Add(neighbour);
                    StartFindingRecursive(neighbour, currentCell, matches);
                }
            }
        }
    }
}
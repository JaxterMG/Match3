using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Cells;
using Core.Config;

namespace MatchLogic
{
    public static class MatchFinder
    {
        public static async Task FindMatches(Cell currentCell)
        {
            List<Cell> matches = new List<Cell>();

            matches.Add(currentCell);
            StartFindingRecursive(currentCell, currentCell, matches);
            if (matches.Count >= 3)
            {
                var xGroups = matches.GroupBy(item => item.Position.X)
                   .Where(group => group.Count() > 2);

                // group items by their y coordinate and filter groups with more than 2 items
                var yGroups = matches.GroupBy(item => item.Position.Y)
                                   .Where(group => group.Count() > 2);

                // get all items that intersect with another dimension
                var intersectingItems = xGroups.SelectMany(group => group)
                                               .Union(yGroups.SelectMany(group => group));

                // create a new list to store the items that we want to keep
                var newItems = matches.Where(item =>
                {
                    // if this item intersects with another dimension that has more than 3 items
                    if (intersectingItems.Contains(item))
                    {
                        return true;
                    }
                    // otherwise, if this item has at least 3 items in its own dimension
                    else if (xGroups.Any(group => group.Key == item.Position.X && group.Count() > 2)
                             || yGroups.Any(group => group.Key == item.Position.Y && group.Count() > 2))
                    {
                        return true;
                    }
                    // otherwise, exclude this item from the new list
                    else
                    {
                        return false;
                    }
                }).ToList();
                matches = newItems;
                foreach (var match in matches)
                {

                    match.ClearCell();

                }
                await GameConfig.Field.ShiftGrid();
            }
        }
        public static void StartFindingRecursive(Cell currentCell, Cell exceptionCell, List<Cell> matches)
        {
            foreach (var neighbour in currentCell.Neighbours)
            {
                if (matches.Contains(neighbour)) continue;

                if (neighbour.CellElement.CellType.Equals(currentCell.CellElement.CellType))
                {
                    matches.Add(neighbour);
                    StartFindingRecursive(neighbour, currentCell, matches);
                }
            }
        }
    }
}
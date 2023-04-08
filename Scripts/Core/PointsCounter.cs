
namespace Core.Points
{
    public static class PointsCounter
    {
        public static int Points;

        public static void AddPoints(int points)
        {
            Points += points;
        }
        public static void ClearPoint()
        {
            Points = 0;
        }
        
    }
}
namespace MazeTest.Core
{
    public static class GameConfig
    {
        public const int MazeCellsCountWidth = 15;
        public const int MazeCellsCountHeight = 30;

        public const float MazeCellSizeWidth = 10f;
        public const float MazeCellSizeHeight = 10f;
        
        public const int EnemiesMinCount = 5;
        public const int EnemiesMaxCount = 20;

        public const float EnemiesFieldOfViewDegrees = 60f;
        public const float EnemiesFieldOfViewDist = 10f;
        public const float EnemiesCloseDistance = 1f;

        public const float EnemiesSpeed = 5f;
        public const float PlayerSpeed = 15f;
    }
}
namespace MazeTest.Core
{
    public static class AssetPaths
    {
        private const string ViewsPath = "Views/";

        public static string GetViewPath(string viewName)
        {
            return ViewsPath + viewName;
        }
    }
}
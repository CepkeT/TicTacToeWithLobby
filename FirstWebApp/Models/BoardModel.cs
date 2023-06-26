namespace FirstWebApp.Models
{
    public static class BoardModel
    {
        public static TicTacToeModel BoardInfo = new TicTacToeModel();
        public static bool IsX = true;
        public static void Restart()
        {
            BoardInfo = new TicTacToeModel();
        }
    }
}

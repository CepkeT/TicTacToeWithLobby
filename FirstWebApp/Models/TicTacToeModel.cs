namespace FirstWebApp.Models
{
    public class TicTacToeModel
    {
        public string MakeMoveName { get; set; }
        public string WinnerName { get; set; }
        public string[] BoardRandom;
        public TicTacToeModel()
        {
            MakeMoveName = string.Empty;
            WinnerName = string.Empty;
            BoardRandom = new string[9] { " ", " ", " ", " ", " ", " ", " ", " ", " " };
        }
    }
}

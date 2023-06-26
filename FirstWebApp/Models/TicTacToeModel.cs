using System.Collections.Generic;

namespace FirstWebApp.Models
{
    public class TicTacToeModel
    {
        public string makeMoveName { get; set; }
        public string winnerName { get; set; }
        public string[] boardRandom;
        public TicTacToeModel()
        {
            makeMoveName = string.Empty;
            winnerName = string.Empty;
            boardRandom = new string[9] { " ", " ", " ", " ", " ", " ", " ", " ", " " };
        }
    }
}

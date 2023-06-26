using FirstWebApp.ServerDatabase;

namespace FirstWebApp.Models
{
    public class LobbyModel
    {
        public string CurrentPlayerName { get; private set; }
        public LobbyTableModel[] Tables { get; private set; }

        public LobbyModel(string currentPlayerName) { 
            CurrentPlayerName = currentPlayerName;
            Tables = new LobbyTableModel[3];

            for (var counter = 0; counter < Tables.Length; counter++)
            {
                var lobbyTabeGuid = Database.Tables.ElementAt(counter).Key;
                Tables[counter] = new LobbyTableModel(lobbyTabeGuid);
            }
        }

    }
}

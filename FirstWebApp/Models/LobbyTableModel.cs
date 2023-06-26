using FirstWebApp.ServerDatabase;

namespace FirstWebApp.Models
{
    public class LobbyTableModel
    {
        public Guid TableGuid { get; private set; }
        public Guid GameGuid { get; private set; }

        public GameInfo Game;

        public LobbyTableModel(Guid tableGuid) {
            TableGuid = tableGuid;
            GameGuid = Database.Tables[TableGuid] ?? Guid.Empty;

            Game = GameGuid == Guid.Empty ? new GameInfo() : Database.Games[GameGuid];
        }
    }
}

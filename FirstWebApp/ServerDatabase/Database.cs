using FirstWebApp.Models;

namespace FirstWebApp.ServerDatabase
{
    public class Database
    {
        /// <summary>
        /// UserGuid -> UserName
        /// </summary>
        public static Dictionary<Guid, string> Users = new Dictionary<Guid, string>();
        /// <summary>
        /// GameGuid -> GameInfo
        /// </summary>
		public static Dictionary<Guid, GameInfo> Games = new Dictionary<Guid, GameInfo>();
        /// <summary>
        /// TableGuid -> GameGuid
        /// </summary>
        public static Dictionary<Guid, Guid?> Tables = new Dictionary<Guid, Guid?>();

        static Database()
        {
            // Создаем 3 стола не связанных с игрой (пустые столы)
            for (var counter = 0; counter < 3; counter++)
            {
                Tables[Guid.NewGuid()] = null;
            }

            Users[Guid.Empty] = "Nobody";
        }
    }
}

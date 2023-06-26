using FirstWebApp.ServerDatabase;
using FirstWebApp.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FirstWebApp.Extensions
{
	public static class ControllerSessionExtension
	{
		public static string? GetCurrentPlayerNameFromSession(this Controller controller)
		{
			var playerGuidInString = controller.HttpContext.Session.GetString(SessionVariables.CurrentPlayerGuid);

			return playerGuidInString == null ? null : Database.Users[new Guid(playerGuidInString)];
		}

		public static void SetCurrentPlayerToSession(this Controller controller, string playerName)
		{
			var playerGuid = Database.Users.First(x => x.Value == playerName).Key;

			controller.HttpContext.Session.SetString(SessionVariables.CurrentPlayerGuid, playerGuid.ToString());
		}

        public static int? GetEncodedFieldFromSession(this Controller controller)
        {
            return controller.HttpContext.Session.GetInt32(SessionVariables.EncodedField);
        }

        public static void SetEncodedFieldToSession(this Controller controller, int encodedField)
        {
            controller.HttpContext.Session.SetInt32(SessionVariables.EncodedField, encodedField);
        }

        public static Guid? GetTableGuidFromSession(this Controller controller)
        {
            var tableGuid = controller.HttpContext.Session.GetString(SessionVariables.TableGuid);

            return tableGuid == null ? null : new Guid(tableGuid.ToString());
        }

        public static void SetTableGuidToSession(this Controller controller, Guid tableGuid)
        {
            controller.HttpContext.Session.SetString(SessionVariables.TableGuid, tableGuid.ToString());
        }
    }
}

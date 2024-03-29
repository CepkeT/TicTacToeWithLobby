﻿using FirstWebApp.Extensions;
using FirstWebApp.Models;
using FirstWebApp.ServerDatabase;
using FirstWebApp.Session;
using FirstWebApp.Utilites;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            BoardModel.restart();
            return View("Index");
        }

        [HttpPost]
        public IActionResult EnroleTable(Guid tableGuid, int field)
        {
            this.SetEncodedFieldToSession(field);
            this.SetTableGuidToSession(tableGuid);

            return RedirectToAction("GamePage");
        }

        [HttpGet]
        [Route("Home/GamePage")]
        public IActionResult GetGamePage()
        {
            var playerName = this.GetCurrentPlayerNameFromSession();
            var field = this.GetEncodedFieldFromSession();
            var tableGuid = this.GetTableGuidFromSession();

            if (playerName == null || field == null || tableGuid == null)
            {
                return RedirectToAction("SessionOutdated");
            }

            BoardModel.boardInfo.boardRandom = 
                FieldDecodeUtility.DecodeField((int)field).Select(x=>$"{x}").ToArray();
            BoardModel.boardInfo.makeMoveName = playerName;

            return View("GamePage", BoardModel.boardInfo);
        }

        [HttpPost]
        public IActionResult GamePage([FromForm(Name = "index")] string index = "-1")
        {
            int indexInt = Convert.ToInt32(index);

            if (indexInt >= 0 && indexInt <= 8)
            {
                putPoint(indexInt);
                if (isWinnerChecker(BoardModel.boardInfo.boardRandom) == true)
                {
                    return View("WinnerPage", BoardModel.boardInfo);
                }
            }

            return View(BoardModel.boardInfo);
        }

        public IActionResult refreshGamePage()
        {
            BoardModel.restart();
            return View("GamePage", BoardModel.boardInfo);
        }

        public void putPoint(int index)
        {
            if (BoardModel.isX == true)
            {
                BoardModel.boardInfo.boardRandom[index] = "X";
                BoardModel.isX = false;
            }
            else
            {
                BoardModel.boardInfo.boardRandom[index] = "O";
                BoardModel.isX = true;
            }
        }

		[HttpGet]
		public IActionResult SessionOutdated()
        {
            return View();
        }

		[HttpGet]
		public IActionResult Lobby()
		{
            var playerName = this.GetCurrentPlayerNameFromSession();

            if (playerName == null)
            {
                return RedirectToAction("SessionOutdated");
            }

            var lobbyDataModel = new LobbyModel(playerName);

			return View(lobbyDataModel);
		}

		[HttpPost]
        public IActionResult AddUser(string playerName)
        {
            if (!Database.Users.ContainsValue(playerName))
            {
                Database.Users.Add(Guid.NewGuid(), playerName);
            }

            this.SetCurrentPlayerToSession(playerName);

            return RedirectToAction("Lobby");
        }

        public bool isWinnerChecker(string[] ticTactToeBoardModel)
        {
            if (
                ((ticTactToeBoardModel[0] == ticTactToeBoardModel[4]) & (ticTactToeBoardModel[4] == ticTactToeBoardModel[8]) &
                (ticTactToeBoardModel[0] != " " & ticTactToeBoardModel[4] != " " & ticTactToeBoardModel[8] != " ")) ||

                ((ticTactToeBoardModel[2] == ticTactToeBoardModel[4]) & (ticTactToeBoardModel[4] == ticTactToeBoardModel[6]) &
                (ticTactToeBoardModel[2] != " " & ticTactToeBoardModel[4] != " " & ticTactToeBoardModel[6] != " ")) ||

                ((ticTactToeBoardModel[2] == ticTactToeBoardModel[5]) & (ticTactToeBoardModel[5] == ticTactToeBoardModel[8]) &
                (ticTactToeBoardModel[2] != " " & ticTactToeBoardModel[5] != " " & ticTactToeBoardModel[8] != " ")) ||

                ((ticTactToeBoardModel[1] == ticTactToeBoardModel[4]) & (ticTactToeBoardModel[4] == ticTactToeBoardModel[7]) &
                (ticTactToeBoardModel[1] != " " & ticTactToeBoardModel[4] != " " & ticTactToeBoardModel[7] != " ")) ||

                ((ticTactToeBoardModel[0] == ticTactToeBoardModel[3]) & (ticTactToeBoardModel[3] == ticTactToeBoardModel[6]) &
                (ticTactToeBoardModel[0] != " " & ticTactToeBoardModel[3] != " " & ticTactToeBoardModel[6] != " ")) ||

                ((ticTactToeBoardModel[6] == ticTactToeBoardModel[7]) & (ticTactToeBoardModel[7] == ticTactToeBoardModel[8]) &
                (ticTactToeBoardModel[6] != " " & ticTactToeBoardModel[7] != " " & ticTactToeBoardModel[8] != " ")) ||

                ((ticTactToeBoardModel[3] == ticTactToeBoardModel[4]) & (ticTactToeBoardModel[4] == ticTactToeBoardModel[5]) &
                (ticTactToeBoardModel[3] != " " & ticTactToeBoardModel[4] != " " & ticTactToeBoardModel[5] != " ")) ||

                ((ticTactToeBoardModel[0] == ticTactToeBoardModel[1]) & (ticTactToeBoardModel[1] == ticTactToeBoardModel[2]) &
                (ticTactToeBoardModel[0] != " " & ticTactToeBoardModel[1] != " " & ticTactToeBoardModel[2] != " "))
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
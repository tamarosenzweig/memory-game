using API.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [RoutePrefix("api/game")]
    [EnableCors("*", "*", "*")]
    public class GameController : ApiController
    {
        [HttpGet]
        [Route("getGameByUserName")]
        public HttpResponseMessage GetGameByUserName(string userName)
        {
            try
            {

                Game currentGame = DB.Games.FirstOrDefault(game => game.Player1.UserName.Equals(userName) || game.Player2.UserName.Equals(userName));
                var response = new { CardArray = currentGame.CardArray, CurrentTurn = currentGame.CurrentTurn };
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpPut]
        [Route("checkGameStatus")]
        public HttpResponseMessage CheckGameStatus()
        {
            try
            {
                string userName = HttpContext.Current.Request.Form["userName"];
                string card1 = HttpContext.Current.Request.Form["card1"];
                string card2 = HttpContext.Current.Request.Form["card2"];

                bool isMatchCards = card1.Equals(card2);
                Game currentGame = DB.Games.FirstOrDefault(game => game.Player1.UserName.Equals(userName) || game.Player2.UserName.Equals(userName));
                if (isMatchCards)
                {
                    currentGame.CardArray[card1] = currentGame.CurrentTurn;
                    if (IsGameOver(currentGame))
                    {
                        FindWinner(currentGame);
                        Task task = new Task(() =>
                          {
                              Thread.Sleep(2000);
                              User currentUser = DB.Users.FirstOrDefault(user => user.UserName.Equals(userName));
                              string partnerName =currentUser.PartnerUserName;
                              currentUser.PartnerUserName = null;
                              DB.Users.FirstOrDefault(user => user.UserName.Equals(partnerName)).PartnerUserName = null;
                              DB.Games.Remove(currentGame);
                          });
                        task.Start();
                        return Request.CreateResponse(HttpStatusCode.Created, true);
                    }
                }
                else
                {
                    currentGame.CurrentTurn = currentGame.CurrentTurn.Equals(currentGame.Player1.UserName) ?
                          currentGame.Player2.UserName :
                          currentGame.Player1.UserName;
                }
                return Request.CreateResponse(HttpStatusCode.Created, false);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }


        }
        private bool IsGameOver(Game game)
        {
            bool isOver = game.CardArray.All(card => card.Value != null);
            return isOver;
        }
        private void FindWinner(Game game)
        {
            int count1 = game.CardArray.Count(card => card.Value.Equals(game.Player1.UserName));
            int count2 = game.CardArray.Count(card => card.Value.Equals(game.Player2.UserName));
            if (count1 >= count2)
                DB.Users.FirstOrDefault(user => user.UserName.Equals(game.Player1.UserName)).Score++;
            if (count1 <= count2)
                DB.Users.FirstOrDefault(user => user.UserName.Equals(game.Player2.UserName)).Score++;

        }

    }
}

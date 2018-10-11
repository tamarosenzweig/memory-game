using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{

    public class GameController : ApiController
    {
        [HttpGet]
        [Route("GetAllCards/{userName}")]
        public HttpResponseMessage GetAllCards(string userName)
        {
            //Get current game by user name
            Game currentGame = GlobalProp.GameList.Find(p => p.Player1.UserName == userName || p.Player2.UserName == userName);
            lock (currentGame)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { cards = currentGame.CardArray, currentTurn = currentGame.CurrentTurn });
            }
        }
        [HttpPut]
        [Route("ChooseTwoCards/{userName}")]
        public HttpResponseMessage ChooseTwoCards(string userName, [FromBody]string[] choosenCards)
        {

            Game currentGame = GlobalProp.GameList.Find(p => p.Player1.UserName == userName || p.Player2.UserName == userName);
            //If the partner user try to choose cards we will not let him:
            if (currentGame.CurrentTurn != userName)
                return Request.CreateResponse(HttpStatusCode.OK);
            lock (GlobalProp.GameList)
            {
                //Change current user to the partner
                if (currentGame.Player1.UserName == userName)
                    currentGame.CurrentTurn = currentGame.Player2.UserName;
                else currentGame.CurrentTurn = currentGame.Player1.UserName;

                //Check if the 2 card are equal
                if (choosenCards[0] == choosenCards[1])
                {
                    currentGame.CardArray[choosenCards[0]] = userName;
                    currentGame.CurrentTurn = userName;
                }

                //Check if game is over
                if (!currentGame.CardArray.ContainsValue(null))
                {
                    //Check who won
                    int player1Score = currentGame.CardArray.Count(p => p.Value == currentGame.Player1.UserName);
                    int player2Score = currentGame.CardArray.Count(p => p.Value == currentGame.Player2.UserName);
                    if (player1Score > player2Score)
                    {
                        currentGame.Player1.Score++;
                       return Request.CreateResponse(HttpStatusCode.OK, currentGame.Player1.UserName);
                    }
                    else
                    {
                        currentGame.Player2.Score++;
                       return Request.CreateResponse(HttpStatusCode.OK, currentGame.Player2.UserName);
                    }
                    

                }
                return Request.CreateResponse(HttpStatusCode.OK,"continue");


            }
        }




    }
}

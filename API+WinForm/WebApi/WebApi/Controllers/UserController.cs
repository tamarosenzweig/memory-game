using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{

    public class UserController : ApiController
    {
        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login([FromBody]User user)
        {
            //Check validations and return the correct error
            if (user.UserName.Length < 2 || user.UserName.Length > 10)
                return Request.CreateResponse(HttpStatusCode.OK, "User name must contains 2-10 chars.");
            if (user.UserName == null)
                return Request.CreateResponse(HttpStatusCode.OK, "User name is required.");
            lock (GlobalProp.UserList)
            {
                User userExists = GlobalProp.UserList.Find(p => p.UserName == user.UserName);
                if (userExists != null)
                    return Request.CreateResponse(HttpStatusCode.OK, "User name is exists, choose another username.");
            }
            if (user.Age < 18 || user.Age > 120)
                return Request.CreateResponse(HttpStatusCode.OK, "Age must be between 18 and 120.");
            if (user.Age == 0)
                return Request.CreateResponse(HttpStatusCode.OK, "Age is required");
            GlobalProp.UserList.Add(user);
            return Request.CreateResponse(HttpStatusCode.OK, "true");
        }

        [HttpGet]
        [Route("GetUsers")]
        public HttpResponseMessage GetUsers()
        {
            lock (GlobalProp.UserList)
            {
                List<User> waitingUsers = GlobalProp.UserList.FindAll(p => p.PartnerUserName == null);
                return Request.CreateResponse(HttpStatusCode.OK, waitingUsers);
            }

        }

        [HttpGet]
        [Route("GetCurrentUser/{userName}")]
        public HttpResponseMessage GetCurrentUser(string userName)
        {
            lock (GlobalProp.UserList)
            {
                User currentUser = GlobalProp.UserList.Find(p => p.UserName == userName);
                return Request.CreateResponse(HttpStatusCode.OK, currentUser);
            }
        }

        [HttpPut]
        [Route("ChoosePatner/{partnerName}")]
        public HttpResponseMessage ChoosePatner(string partnerName, [FromBody]string currentUserName)
        {
            lock (GlobalProp.UserList)
            {
                User currentUser = GlobalProp.UserList.Find(p => p.UserName == currentUserName);
                User partner = GlobalProp.UserList.Find(p => p.UserName == partnerName);
                //If the partner was choosen by another player
                if (partner.PartnerUserName != null)
                    return Request.CreateResponse(HttpStatusCode.OK, "Choose another partner.");
                //If the user was choosen by another player
                if (currentUser.PartnerUserName != null)
                    return Request.CreateResponse(HttpStatusCode.OK, "You have already been choosen to a game.");
                //If the user chose imself
                if (partnerName == currentUserName)
                    return Request.CreateResponse(HttpStatusCode.OK, "You can't choose yourself.");
                //Set the partners' partners
                currentUser.PartnerUserName = partnerName;
                partner.PartnerUserName = currentUserName;

                //Create a new game with the two players
                Game newGame = new Game() { Player1 = currentUser, Player2 = partner, CurrentTurn = currentUser.UserName };
                GlobalProp.GameList.Add(newGame);

                return Request.CreateResponse(HttpStatusCode.OK, "true");

            }
        }


    }
}

using memoryGame.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace memoryGame
{
    public partial class Game : Form
    {
        string currentTurn;
        Dictionary<string, string> CardsDictionary;
        string currentPlayer;
        string partnerPlayer;
        private static Random rng = new Random();
        private int counterClick = 0;
        private string firstCardValue;
        private string firstCardName;
        private string secondCardName;
        List<string> allCards = new List<string>();


        public Game(string player1, string player2)
        {
            InitializeComponent();
            currentPlayer = player1;
            partnerPlayer = player2;
            GlobalProp.CurrentUser.PartnerUserName = partnerPlayer;
            lbl_player_name.Text = player1;
            lbl_partner_name_2.Text = player2+" counter";
            lbl_my_sets.Text = "0";
            lbl_partner_sets.Text = "0";
            //Get the CardsList
            GetCardsAndCurrentTurn();
            //Create new list and duplicate the cards from dictionary(list);
            allCards.AddRange(CardsDictionary.Keys);
            allCards.AddRange(CardsDictionary.Keys);
            MixList();
            showCards();

        }

        private void showCards()
        {
            //View the cards:

            //Set the values of users' count sets.
            lbl_my_sets.Text = CardsDictionary.Values.Count(p => p == GlobalProp.CurrentUser.UserName).ToString();
            lbl_partner_sets.Text = CardsDictionary.Values.Count(p => p == GlobalProp.CurrentUser.PartnerUserName).ToString();
            int indexForBtn = 1;
            //Iterating the cardslist:
            foreach (string item in allCards)
            {
                //Get the suitable button which matches the current card
                Button btn = (tableLayoutPanel1.Controls["b" + indexForBtn++] as Button);
                //If this card is partof chosen pair
                if (CardsDictionary[item] != null)
                {
                    //Set the value
                    btn.Text = item;
                    //Set the matches color according to player
                    if (CardsDictionary[item] == currentPlayer)
                    {
                        btn.BackColor = Color.Red;
                        btn.Enabled = false;
                    }
                    else
                    {
                        btn.BackColor = Color.Green;
                        btn.Enabled = false;
                    }
                }
               //If the button is the last which has been clicked
                else if (btn.Name == firstCardName || btn.Name == secondCardName)
                    btn.Text = item;
                //Or set "?" mark
                else btn.Text = "?";
                if (btn.Name == secondCardName)
                {
                    Thread.Sleep(1000);
                    secondCardName = "";
                }
            }
        }

        public void GetCardsAndCurrentTurn()
        {
            //Request via GET-GetAllCards
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:57034/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"GetAllCards/{GlobalProp.CurrentUser.UserName}").Result;  
            if (response.IsSuccessStatusCode)
            {
                var CardsJson = response.Content.ReadAsStringAsync().Result;
                dynamic obj = JsonConvert.DeserializeObject(CardsJson);
                CardsDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(obj["cards"]));
                currentTurn = JsonConvert.SerializeObject(obj["currentTurn"]);
                currentTurn = currentTurn.Trim(new Char[] { '\"' });
                //If the game is over
                if (!CardsDictionary.Values.Contains(null))
                {
                    int my_sets = CardsDictionary.Values.Count(p => p == currentPlayer);
                    int partner_sets = CardsDictionary.Values.Count(p => p == partnerPlayer);
                    if (my_sets > partner_sets)
                    {
                        timer1.Stop();
                        Close();
                        MessageBox.Show("You won the game.");

                    }
                    else
                    {
                        timer1.Stop();
                        Close();
                        MessageBox.Show($"{partnerPlayer} won the game.");

                    }

                }
                else showCards();
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Power the get request
            GetCardsAndCurrentTurn();
            //Enable the buttons according to turn.
            if (currentTurn == currentPlayer)
            {
                lbl_player_name.Text = currentPlayer;
                foreach (Control item in tableLayoutPanel1.Controls)
                {
                    item.Enabled = true;
                }
            }
            else
            {
                lbl_player_name.Text = partnerPlayer;
                foreach (Control item in tableLayoutPanel1.Controls)
                {
                    item.Enabled = false;
                }
            }
        }

        public void MixList()
        {

            int n = allCards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = allCards[k];
                allCards[k] = allCards[n];
                allCards[n] = value;
            }
        }
        
        private void ClickCard(object sender, EventArgs e)
        {
            //No. of clicks per turn.
            counterClick++;
            (sender as Button).Text = allCards[int.Parse((sender as Button).Name.Remove(0, 1)) - 1];
            if (counterClick == 2)
            {
                firstCardName = "";
                secondCardName = (sender as Button).Name;
                //If the user chose the same card twice
                if ((sender as Button).Name == firstCardName)
                {
                    counterClick--;
                    MessageBox.Show("You can not choose the same card twice!!!");
                    return;
                }
                sendCards(firstCardValue, allCards[int.Parse((sender as Button).Name.Remove(0, 1)) - 1]);
                counterClick = 0;
            }
            else
            {
                firstCardValue = allCards[int.Parse((sender as Button).Name.Remove(0, 1)) - 1];
                firstCardName = (sender as Button).Name;
            }

        }

        private void sendCards(string firstCardValue, string secondCardValue)
        {
            //Request via PUT-ChooseTwoCards
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($@"http://localhost:57034/ChooseTwoCards/{currentPlayer}");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string[] cards = new string[] { firstCardValue, secondCardValue };
                dynamic cardsArray = cards;
                string cardsString = Newtonsoft.Json.JsonConvert.SerializeObject(cardsArray, Formatting.None);
                streamWriter.Write(cardsString);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream(), ASCIIEncoding.ASCII))
            {
                string result = streamReader.ReadToEnd();
                //If game is over
                if (!result.Contains("continue"))
                {
                    timer1.Stop();
                    Close();
                    MessageBox.Show($"{result} won the game!!!");
                }
            }
        }
    }
}

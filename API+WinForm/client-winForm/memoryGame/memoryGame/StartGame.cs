using memoryGame.Models;
using System;
using System.Windows.Forms;

namespace memoryGame
{
    public partial class StartGame : Form
    {
        User partner;
        public StartGame(User partner)
        {
            InitializeComponent();
            this.partner = partner;
            lbl_name.Text = partner.UserName;
            lbl_age.Text = partner.Age.ToString();
        }

        private void btn_startGame_Click(object sender, EventArgs e)
        {
            Game game = new Game(GlobalProp.CurrentUser.UserName,partner.UserName);
            game.Show();
            Close();
        }

     
    }
}

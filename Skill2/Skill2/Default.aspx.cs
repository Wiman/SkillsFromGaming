using System;
using System.Net;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace Skill2
{
    public partial class Default : System.Web.UI.Page
    {

        ArrayList selectedGames = new ArrayList();
        Games games;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["games"] == null)
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.UTF8;
                    string json = wc.DownloadString("http://skillsfromgaming.azurewebsites.net/api/gaming");
                    json = "{\"Games\": " + json + "}";
                    games = JsonConvert.DeserializeObject<Games>(json);
                    Console.WriteLine("done");
                    Session["games"] = games;
                }
            } else
            {
                games = (Games)Session["games"];
            }
            if (Session["selectedGames"] == null)
            {
                selectedGames = new ArrayList();
            } else
            {
                selectedGames = (ArrayList)Session["selectedGames"];
            }

            Label1.Text = "";
            
            CreateButtons();

        }


        void LinkButton_Click(Object sender, EventArgs e)
        {
            Random rnd = new Random();
            int r = rnd.Next(0, 3);

            LinkButton lButton = (LinkButton)sender;
            string[] Phrases = { "Yo, You got Skills!", "Them skills you have", "Lets see what skills you have aquierd" , "Skills that you got, They are super effective", };
            int i = selectedGames.IndexOf(lButton.CommandName);
            if (i == -1)
            {
                selectedGames.Add(lButton.CommandName);
            } else
            {
                selectedGames.RemoveAt(i);
            }

            ArrayList skills = new ArrayList();
            
            foreach(string _id in selectedGames)
            {
                Game game = games.GetGame(int.Parse(_id));
                if (game != null)
                {
                    foreach (GameSkill gameSkill in game.gameSkills)
                    {
                        if(skills.IndexOf(gameSkill.name) == -1)
                        {
                            skills.Add(gameSkill.name);
                        }
                    }
                }
            }

            if (skills.Count > 0)
            {
                skills.Sort();
                Label2.Text = "<l1>" + Phrases[r] + "</l1><br>";
                Label1.Text = String.Join("<br>", skills.ToArray());
            } else
            {
                Label1.Text = "";
            }

            Session["selectedGames"] = selectedGames;

            UpdateButtons();

        }

        void CreateButtons()
        {
            foreach (Game game in games.gamesSorted)
            {
                //ListBox1.Items.Add(new ListItem(game.name, game._id.ToString()));
                LinkButton lButton = new LinkButton();
                lButton.ID = game.shortName;
                lButton.Text = game.name;
                if (selectedGames.IndexOf(game._id.ToString()) > -1)
                {
                    lButton.CssClass = "w3-button w3-red w3-block w3-padding-large w3-jumbo div-button";
                }
                else
                {
                    lButton.CssClass = "w3-button w3-black w3-block w3-padding-large w3-jumbo div-button";
                }
                lButton.Command += LinkButton_Click;
                lButton.CommandName = game._id.ToString();
               
                panel1.Controls.Add(lButton);
                
            }
        }

        void UpdateButtons()
        {
            foreach(LinkButton lButton in panel1.Controls.OfType<LinkButton>())
            {
                
                if (selectedGames.IndexOf(lButton.CommandName) > -1)
                {
                    lButton.CssClass = "w3-button w3-red w3-block w3-padding-large w3-jumbo div-button";
                } else
                {
                    lButton.CssClass = "w3-button w3-black w3-block w3-padding-large w3-jumbo div-button";
                }
            }

        }

       
    }
}
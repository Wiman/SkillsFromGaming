﻿using System;
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
    public partial class WebForm1 : System.Web.UI.Page
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
            if(Session["selectedGames"] == null)
            {
                selectedGames = new ArrayList();
            } else
            {
                selectedGames = (ArrayList)Session["selectedGames"];
            }

            CreateButtons();

        }

        void LinkButton_Click(Object sender, EventArgs e)
        {
            LinkButton lButton = (LinkButton)sender;

            int i = selectedGames.IndexOf(lButton.CommandName);
            if (i == -1)
            {
                selectedGames.Add(lButton.CommandName);
            } else
            {
                selectedGames.RemoveAt(i);
            }

            Label1.Text = "";
            foreach(object obj in selectedGames)
            {
                Label1.Text += (string)obj + "   ";
            }

            Session["selectedGames"] = selectedGames;

            CreateButtons();

        }

        void CreateButtons()
        {
            foreach (Game game in games.games)
            {
                //ListBox1.Items.Add(new ListItem(game.name, game._id.ToString()));
                LinkButton lButton = new LinkButton();
                lButton.ID = game.shortName;
                lButton.Text = game.name;
                if (selectedGames.IndexOf(game._id.ToString()) > -1)
                {
                    lButton.CssClass = "w3-button w3-red w3-block w3-padding-large w3-jumbo";
                }
                else
                {
                    lButton.CssClass = "w3-button w3-black w3-block w3-padding-large w3-jumbo";
                }
                lButton.Command += LinkButton_Click;
                lButton.CommandName = game._id.ToString();

                panel1.Controls.Add(lButton);
            }
        }



    }
}
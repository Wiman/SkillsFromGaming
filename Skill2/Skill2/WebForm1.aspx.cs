using System;
using System.Net;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Skill2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Games games;
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                string json = wc.DownloadString("http://afgamingserver.azurewebsites.net/api/gaming");
                json = "{\"Games\": " + json + "}";
                games = JsonConvert.DeserializeObject<Games>(json);
                Console.WriteLine("done");
            }

            foreach(Game game in games.games)
            {
                ListBox1.Items.Add(new ListItem(game.name, game._id.ToString()));
            }
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
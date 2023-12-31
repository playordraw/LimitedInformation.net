// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
//
// File: top8.aspx.cs
//
// Copyright (c) 2023 Jeffrey Reynolds
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Text;

public partial class top8 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Rounds.Text) <= 20 && Convert.ToInt32(Rounds.Text) >= 5)
            {
                if (Convert.ToInt32(Players.Text) > 16)
                {
                    StringBuilder results = new StringBuilder(144 + (Convert.ToInt32(Rounds.Text) * 30));
                    resultsbox.Controls.Clear();
                    if (NotMTGO.Checked)
                        Mylib.top8.CalculateSwiss(Convert.ToInt32(Players.Text), Convert.ToInt32(Rounds.Text), results);
                    else
                        Mylib.top8.CalculateMTGOSwiss(Convert.ToInt32(Players.Text), Convert.ToInt32(Rounds.Text), results);
                    resultsbox.Controls.Add(new LiteralControl(results.ToString()));
                }
                else
                {//No players
                    resultsbox.Controls.Clear();
                    resultsbox.Controls.Add(new LiteralControl("<h2>Sorry!</h2><p>Please make sure the number of players is 17 or above (No top 8 with 16 players).</p>"));
                }
            }
            else
            {//Rounds is too big or too small.
                if (Convert.ToInt32(Rounds.Text) > 20)
                {
                    resultsbox.Controls.Clear();
                    resultsbox.Controls.Add(new LiteralControl("<h2>Sorry!</h2><p>A current limitation of the program is only working for 20 rounds or less. This may be increased in the future.</p>"));
                }
                else
                {
                    resultsbox.Controls.Clear();
                    resultsbox.Controls.Add(new LiteralControl("<h2>Sorry!</h2><p>There is no top 8 for less then 5 rounds.</p>"));
                }
            }
        }
        catch (System.FormatException)
        {
            resultsbox.Controls.Clear();
            resultsbox.Controls.Add(new LiteralControl("<h2>Sorry!</h2><p>There was a problem with your input, please check it and try again.</p>"));
        }
    }
}

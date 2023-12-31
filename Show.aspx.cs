// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
//
// File: Show.aspx.cs
//
// Copyright (c) 2023 Jeffrey Reynolds
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Show : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet temp = ShowDetails();
        DetailListrptr.SelectedIndex = temp.Tables[0].Rows.Count - 1;
        DetailListrptr.DataSource = temp;
        DetailListrptr.DataBind();
        temp = GetRatings();
        temp.Tables["Rating"].DefaultView.Sort = "cardname";
        for (int i = 0; i < DetailListrptr.SelectedIndex; i++)
        {
            HtmlGenericControl control = ((HtmlGenericControl)DetailListrptr.Controls[i].FindControl("RatingLabel"));
            string title = control.Attributes["title"];
            int rowid = temp.Tables["Rating"].DefaultView.Find(title);
            if (rowid < 0)
            {
                control.InnerText = "Rating: No Rating";
            }
            else
            {
                DataRow row = temp.Tables["Rating"].Rows[rowid];
                control.InnerText = "Rating: " + row[1].ToString();
            }
        }

        {
            HtmlGenericControl control = ((HtmlGenericControl)DetailListrptr.Controls[DetailListrptr.SelectedIndex].FindControl("UpdatePanel1").FindControl("divrating"));
            string title = control.Attributes["title"];
            int rowid = temp.Tables["Rating"].DefaultView.Find(title);
            if (rowid < 0)
            {
                control.Style.Add("width", "0px");
            }
            else
            {
                DataRow row = temp.Tables["Rating"].Rows[rowid];
                control.Style.Add("width", row[1].ToString() + "px");
            }
        }       

        Previous.Visible = (Convert.ToInt32(Request.QueryString["Pack"]) * Convert.ToInt32(Request.QueryString["SubPack"])) != 1;
        Next.Visible = !Convert.ToBoolean((Convert.ToInt32(Request.QueryString["Pack"]) * Convert.ToInt32(Request.QueryString["SubPack"])) / 45);
        Previous.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] +
                                        "&Pack=" + (Convert.ToInt32(Request.QueryString["Pack"]) - (((16 - Convert.ToInt32(Request.QueryString["SubPack"])) / 15) * 1)) +
                                     "&SubPack=" + (Convert.ToInt32(Request.QueryString["SubPack"]) - 1 + ((16 - Convert.ToInt32(Request.QueryString["SubPack"])) / 15) * 15);
        Next.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] +
                                    "&Pack=" + (Convert.ToInt32(Request.QueryString["Pack"]) + Convert.ToInt32(Request.QueryString["SubPack"]) / 15) +
                                 "&SubPack=" + (Convert.ToInt32(Request.QueryString["SubPack"]) % 15 + 1);
        Previous2.Visible = Previous.Visible;
        Next2.Visible = Next.Visible;
        Previous2.NavigateUrl = Previous.NavigateUrl;
        Next2.NavigateUrl = Next.NavigateUrl;

        Set1.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=1" + "&SubPack=" + Request.QueryString["SubPack"];
        Set2.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=2" + "&SubPack=" + Request.QueryString["SubPack"];
        Set3.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=3" + "&SubPack=" + Request.QueryString["SubPack"];

        Pack1.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=1";
        Pack2.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=2";
        Pack3.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=3";
        Pack4.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=4";
        Pack5.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=5";
        Pack6.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=6";
        Pack7.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=7";
        Pack8.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=8";
        Pack9.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=9";
        Pack10.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=10";
        Pack11.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=11";
        Pack12.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=12";
        Pack13.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=13";
        Pack14.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=14";
        Pack15.NavigateUrl = "~/Show.aspx?ID=" + Request.QueryString["ID"] + "&Pack=" + Request.QueryString["Pack"] + "&SubPack=15";
    }

    private DataSet GetRatings()
    {
        SqlConnection cnnt = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["limitedinformationConnectionStringLive"].ConnectionString);
        string query = String.Format("SELECT cardtbl.cardname, AVG(ratingtbl.rating), cardtbl.cardid FROM cardtbl INNER JOIN drafttbl ON cardtbl.cardid = drafttbl.cardid INNER JOIN eventtbl ON drafttbl.eventid = eventtbl.eventid INNER JOIN ratingtbl on cardtbl.cardid = ratingtbl.cardid Where eventtbl.eventid = {0} AND drafttbl.pack = {1} AND drafttbl.cardpack = {2} GROUP BY drafttbl.pick, cardtbl.cardname, cardtbl.setname, cardtbl.cardid", Request.QueryString["ID"], Request.QueryString["Pack"], Request.QueryString["SubPack"]);
        SqlDataAdapter SQL = new SqlDataAdapter(query, cnnt);
        DataSet data = new DataSet();
        SQL.Fill(data, "Rating");
        return data;
    }
    DataSet ShowDetails()
    {
        SqlConnection cnnt = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["limitedinformationConnectionStringLive"].ConnectionString);
        string query = String.Format("SELECT cardtbl.cardname, cardtbl.setname, drafttbl.pick, cardtbl.cardid FROM cardtbl INNER JOIN drafttbl ON cardtbl.cardid = drafttbl.cardid INNER JOIN eventtbl ON drafttbl.eventid = eventtbl.eventid Where eventtbl.eventid = {0} AND drafttbl.pack = {1} AND drafttbl.cardpack = {2} GROUP BY drafttbl.pick, cardtbl.cardname, cardtbl.cardid, cardtbl.setname", Request.QueryString["ID"], Request.QueryString["Pack"], Request.QueryString["SubPack"]);
        SqlDataAdapter SQL = new SqlDataAdapter(query, cnnt);
        DataSet data = new DataSet();
        SQL.Fill(data, "Detail");
        return data;
    }
    protected void FormatString(Object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image imgcontrol = (Image)e.Item.FindControl("Image1");
            string card = ((DataRowView)e.Item.DataItem).Row.ItemArray[0].ToString().Replace(",", "").Replace(":", "").Replace("'", "").Replace("’", "").Replace("/", "").Replace(" ", "_").Replace("-", "_").Replace("Æ", "e");
            string set = ((DataRowView)e.Item.DataItem).Row.ItemArray[1].ToString();
            imgcontrol.ImageUrl = "http://wizards.com/global/images/magic/" + set + "/" + card + ".jpg";
            HtmlGenericControl span = (HtmlGenericControl)e.Item.FindControl("RatingLabel");
            span.Attributes.Add("title", ((DataRowView)e.Item.DataItem).Row.ItemArray[0].ToString());
        }
        else if (e.Item.ItemType == ListItemType.SelectedItem)
        {
            Image imgcontrol = (Image)e.Item.FindControl("Image1");
            string card = ((DataRowView)e.Item.DataItem).Row.ItemArray[0].ToString().Replace(",", "").Replace(":", "").Replace("'", "").Replace("’", "").Replace("/", "").Replace(" ", "_").Replace("-", "_").Replace("Æ", "e");
            string set = ((DataRowView)e.Item.DataItem).Row.ItemArray[1].ToString();
            imgcontrol.ImageUrl = "http://wizards.com/global/images/magic/" + set + "/" + card + ".jpg";
            HtmlGenericControl div = (HtmlGenericControl)e.Item.FindControl("divrating");
            div.Attributes.Add("title", ((DataRowView)e.Item.DataItem).Row.ItemArray[0].ToString());
        }
    }
    protected string GetSha(string input)
    {
        System.Security.Cryptography.SHA256Managed hash = new System.Security.Cryptography.SHA256Managed();
        byte[] data = hash.ComputeHash(Encoding.Default.GetBytes(input));
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            result.Append(data[i].ToString("x2"));
        }
        return result.ToString();
    }
    protected void rating_click(object sender, ImageMapEventArgs e)
    {
        HtmlGenericControl control = ((HtmlGenericControl)DetailListrptr.Controls[DetailListrptr.SelectedIndex].FindControl("UpdatePanel1").FindControl("divrating"));
        string title = control.Attributes["title"];

        //TODO: Set cookie that user has rated this card
        //TODO: Insert new rating into the database
        //query = string.Format("INSERT INTO [limitedinformation].[licnnt].[eventtbl] ([playername],[created])VALUES('{0}','{1}')", playername.Replace("\'", "\'\'"), datetime.Replace("\'", "\'\'"));
        //command = new SqlCommand(query, connection);
        //command.ExecuteNonQuery();
        SqlConnection cnnt = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["limitedinformationConnectionStringLive"].ConnectionString);
        String query = String.Format("INSERT INTO ratingtbl (cardid,rating)VALUES(SELECT '{0}','{1}')", "0".Replace("\'", "\'\'"), "1".Replace("\'", "\'\'"));
        //Get ratings from the database
        DataSet temp = GetRatings();
        temp.Tables["Rating"].DefaultView.Sort = "cardname";
        //Disable rating for this card.
        ImageMap map = (ImageMap)control.FindControl("rating");
        map.Enabled = false;
        //Display new rating
        int rowid = temp.Tables["Rating"].DefaultView.Find(title);
        DataRow row = temp.Tables["Rating"].Rows[rowid];
        control.Style.Add("width", row[1].ToString() + "px");
    }
    protected void RegisterAsync(object sender, EventArgs e)
    {
        ScriptManager1.RegisterAsyncPostBackControl(rating);
    }
}

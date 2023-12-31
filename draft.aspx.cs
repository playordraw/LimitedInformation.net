// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
//
// File: draft.aspx.cs
//
// Copyright (c) 2023 Jeffrey Reynolds
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class draft : System.Web.UI.Page
{
    DataSet data;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.Trace.IsEnabled = true;        
        if (!IsPostBack)
        {//Set initial View for gridview
            ViewState["SortExpression"] = "Draft Date";
            ViewState["SortDirection"] = "DESC";
            ViewState["CurrentPageIndex"] = 0;
            GetData(ref data);
        }
        else
            //GetDataFiltered(ref data);
            GetData(ref data);
        if (ViewState["CurrentPageIndex"] != null)
        {
            GridView1.PageIndex = (Int32)ViewState["CurrentPageIndex"];
        }
        BindGrid(ref data);
        PopulateLists(ref data);        
    }
    protected void UploadButton_Click(object sender, EventArgs e)
    {
        UploadResults.Text = "";
        if (UploadDraft.HasFile)
        {
            try
            {
                //Hands off the file as a stream to be parsed and added to the database.
                int results = Mylib.Draft.AddNewDraft(new StreamReader(UploadDraft.FileContent), System.Configuration.ConfigurationManager.ConnectionStrings["limitedinformationConnectionStringLive"].ConnectionString);//Server=(local);Database=limitedinformation;User ID=t8cnnt;Password=All#s2u$
                if (results == -1)
                {//Invalid File format
                    UploadResults.Text = "There was an error with the file format. Please check the file and try again.";
                    UploadDraft.SaveAs("C:/Websites/limitedinfo/limitedinformation.net/wwwroot/Logs/" + UploadDraft.FileName);
                    StreamWriter errorlog = new StreamWriter("C:/Websites/limitedinfo/limitedinformation.net/wwwroot/Logs/" + DateTime.Today.ToLongDateString() + ".txt");
                    errorlog.WriteLine("AddNewDraft returned " + results + " with the file " + UploadDraft.FileName);
                    errorlog.WriteLine("\r\n");
                    errorlog.Flush();
                    errorlog.Close();
                    return;
                }
                if (results == -2)
                {//Invalid File format, database was updated and changes reversed
                    UploadResults.Text = "There was an error with the data in the file. Please check the file and try again.";
                    UploadDraft.SaveAs("C:/Websites/limitedinfo/limitedinformation.net/wwwroot/Logs/" + UploadDraft.FileName);
                    StreamWriter errorlog = new StreamWriter("C:/Websites/limitedinfo/limitedinformation.net/wwwroot/Logs/" + DateTime.Today.ToLongDateString() + ".txt");
                    errorlog.WriteLine("AddNewDraft returned " + results + " with the file " + UploadDraft.FileName);
                    errorlog.WriteLine("\r\n");
                    errorlog.Flush();
                    errorlog.Close();
                    return;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {//SQL exception. Most likely an attempt to upload a duplicate event.
                UploadResults.Text = "There was an error adding information to the database. If this is the first time you've uploaded this file, please check the file and try again.";
                UploadDraft.SaveAs("C:/Websites/limitedinfo/limitedinformation.net/wwwroot/Logs/" + UploadDraft.FileName);
                StreamWriter errorlog = new StreamWriter("C:/Websites/limitedinfo/limitedinformation.net/wwwroot/Logs/" + DateTime.Today.ToLongDateString() + ".txt");
                errorlog.WriteLine("SQL exception occured with the file " + UploadDraft.FileName);
                errorlog.WriteLine(ex);
                errorlog.WriteLine("\r\n");
                errorlog.Flush();
                errorlog.Close();
                return;
            }
        }
        else
        {//No file attached
            UploadResults.Text = "No file detected. Please choose a file, then click on upload.";
            return;
        }
        //File was uploaded successfully
        UploadResults.Text = "File Uploaded successfully!";
        //Response.Redirect("~/draft.aspx");
    }
    void GetData(ref DataSet data)
    {//Retrive the Event ID, Player name, Set information, and draft date from the database.
        SqlConnection cnnt = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["limitedinformationConnectionStringLive"].ConnectionString);        
        string query = "SELECT DISTINCT eventtbl_1.eventid AS ID, eventtbl_1.playername AS [Player Name], (SELECT DISTINCT cardtbl.setname FROM eventtbl INNER JOIN drafttbl ON eventtbl.eventid = drafttbl.eventid INNER JOIN cardtbl ON drafttbl.cardid = cardtbl.cardid WHERE (drafttbl.pack = 1) AND (eventtbl_1.eventid = eventtbl.eventid)) AS [Set 1], (SELECT DISTINCT cardtbl_3.setname FROM eventtbl AS eventtbl_3 INNER JOIN drafttbl AS drafttbl_3 ON eventtbl_3.eventid = drafttbl_3.eventid INNER JOIN cardtbl AS cardtbl_3 ON drafttbl_3.cardid = cardtbl_3.cardid WHERE (drafttbl_3.pack = 2) AND (eventtbl_1.eventid = eventtbl_3.eventid)) AS [Set 2], (SELECT DISTINCT cardtbl_2.setname FROM eventtbl AS eventtbl_2 INNER JOIN drafttbl AS drafttbl_2 ON eventtbl_2.eventid = drafttbl_2.eventid INNER JOIN cardtbl AS cardtbl_2 ON drafttbl_2.cardid = cardtbl_2.cardid WHERE (drafttbl_2.pack = 3) AND (eventtbl_1.eventid = eventtbl_2.eventid)) AS [Set 3], eventtbl_1.created AS [Draft Date] FROM eventtbl AS eventtbl_1 INNER JOIN drafttbl AS drafttbl_1 ON eventtbl_1.eventid = drafttbl_1.eventid INNER JOIN cardtbl AS cardtbl_1 ON drafttbl_1.cardid = cardtbl_1.cardid";
        SqlDataAdapter SQL = new SqlDataAdapter(query, cnnt);
        data = new DataSet();
        SQL.Fill(data, "Detail");
    }
    void GetDataFiltered(ref DataSet data)
    {//Gets a filtered set of data based on drop down boxes in gridview. The where of the query string i built dynamically from all of boxes that have selections.
        SqlConnection cnnt = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["limitedinformationConnectionStringLive"].ConnectionString);
        string query = "SELECT DISTINCT eventtbl_1.eventid AS ID, eventtbl_1.playername AS [Player Name], (SELECT DISTINCT cardtbl.setname FROM eventtbl INNER JOIN drafttbl ON eventtbl.eventid = drafttbl.eventid INNER JOIN cardtbl ON drafttbl.cardid = cardtbl.cardid WHERE (drafttbl.pack = 1) AND (eventtbl_1.eventid = eventtbl.eventid)) AS [Set 1], (SELECT DISTINCT cardtbl_3.setname FROM eventtbl AS eventtbl_3 INNER JOIN drafttbl AS drafttbl_3 ON eventtbl_3.eventid = drafttbl_3.eventid INNER JOIN cardtbl AS cardtbl_3 ON drafttbl_3.cardid = cardtbl_3.cardid WHERE (drafttbl_3.pack = 2) AND (eventtbl_1.eventid = eventtbl_3.eventid)) AS [Set 2], (SELECT DISTINCT cardtbl_2.setname FROM eventtbl AS eventtbl_2 INNER JOIN drafttbl AS drafttbl_2 ON eventtbl_2.eventid = drafttbl_2.eventid INNER JOIN cardtbl AS cardtbl_2 ON drafttbl_2.cardid = cardtbl_2.cardid WHERE (drafttbl_2.pack = 3) AND (eventtbl_1.eventid = eventtbl_2.eventid)) AS [Set 3], eventtbl_1.created AS [Draft Date] FROM eventtbl AS eventtbl_1 INNER JOIN drafttbl AS drafttbl_1 ON eventtbl_1.eventid = drafttbl_1.eventid INNER JOIN cardtbl AS cardtbl_1 ON drafttbl_1.cardid = cardtbl_1.cardid ";
        bool concat = false;
        if (((DropDownList)GridView1.Controls[0].Controls[0].Controls[1].Controls[3]).SelectedValue != "")
        {
            query += "WHERE " + string.Format("eventtbl_1.playername = '{0}'", ((DropDownList)GridView1.Controls[0].Controls[0].Controls[1].Controls[3]).SelectedValue);
            concat = true;
        }
        if (((DropDownList)GridView1.Controls[0].Controls[0].Controls[2].Controls[3]).SelectedValue != "")
        {
            if (!concat)
            {
                query += "WHERE " + string.Format("(SELECT DISTINCT cardtbl.setname FROM eventtbl INNER JOIN drafttbl ON eventtbl.eventid = drafttbl.eventid INNER JOIN cardtbl ON drafttbl.cardid = cardtbl.cardid WHERE (drafttbl.pack = 1) AND (eventtbl_1.eventid = eventtbl.eventid)) = '{0}'", ((DropDownList)GridView1.Controls[0].Controls[0].Controls[2].Controls[3]).SelectedValue);
                concat = true;
            }
            else
            {
                query += " AND " + string.Format("(SELECT DISTINCT cardtbl.setname FROM eventtbl INNER JOIN drafttbl ON eventtbl.eventid = drafttbl.eventid INNER JOIN cardtbl ON drafttbl.cardid = cardtbl.cardid WHERE (drafttbl.pack = 1) AND (eventtbl_1.eventid = eventtbl.eventid)) = '{0}'", ((DropDownList)GridView1.Controls[0].Controls[0].Controls[2].Controls[3]).SelectedValue);
            }
        }
        if (((DropDownList)GridView1.Controls[0].Controls[0].Controls[3].Controls[3]).SelectedValue != "")
        {
            if (!concat)
            {
                query += "WHERE " + string.Format("(SELECT DISTINCT cardtbl_3.setname FROM eventtbl AS eventtbl_3 INNER JOIN drafttbl AS drafttbl_3 ON eventtbl_3.eventid = drafttbl_3.eventid INNER JOIN cardtbl AS cardtbl_3 ON drafttbl_3.cardid = cardtbl_3.cardid WHERE (drafttbl_3.pack = 2) AND (eventtbl_1.eventid = eventtbl_3.eventid)) = '{0}'", ((DropDownList)GridView1.Controls[0].Controls[0].Controls[3].Controls[3]).SelectedValue);
                concat = true;
            }
            else
            {
                query += " AND " + string.Format("(SELECT DISTINCT cardtbl_3.setname FROM eventtbl AS eventtbl_3 INNER JOIN drafttbl AS drafttbl_3 ON eventtbl_3.eventid = drafttbl_3.eventid INNER JOIN cardtbl AS cardtbl_3 ON drafttbl_3.cardid = cardtbl_3.cardid WHERE (drafttbl_3.pack = 2) AND (eventtbl_1.eventid = eventtbl_3.eventid)) = '{0}'", ((DropDownList)GridView1.Controls[0].Controls[0].Controls[3].Controls[3]).SelectedValue);
            }
        }
        if (((DropDownList)GridView1.Controls[0].Controls[0].Controls[4].Controls[3]).SelectedValue != "")
        {
            if (!concat)
            {
                query += "WHERE " + string.Format("(SELECT DISTINCT cardtbl_2.setname FROM eventtbl AS eventtbl_2 INNER JOIN drafttbl AS drafttbl_2 ON eventtbl_2.eventid = drafttbl_2.eventid INNER JOIN cardtbl AS cardtbl_2 ON drafttbl_2.cardid = cardtbl_2.cardid WHERE (drafttbl_2.pack = 3) AND (eventtbl_1.eventid = eventtbl_2.eventid)) = '{0}'", ((DropDownList)GridView1.Controls[0].Controls[0].Controls[4].Controls[3]).SelectedValue);
                concat = true;
            }
            else
            {
                query += " AND " + string.Format("(SELECT DISTINCT cardtbl_2.setname FROM eventtbl AS eventtbl_2 INNER JOIN drafttbl AS drafttbl_2 ON eventtbl_2.eventid = drafttbl_2.eventid INNER JOIN cardtbl AS cardtbl_2 ON drafttbl_2.cardid = cardtbl_2.cardid WHERE (drafttbl_2.pack = 3) AND (eventtbl_1.eventid = eventtbl_2.eventid)) = '{0}'", ((DropDownList)GridView1.Controls[0].Controls[0].Controls[4].Controls[3]).SelectedValue);
            }
        }
        SqlDataAdapter SQL = new SqlDataAdapter(query, cnnt);
        data = new DataSet();
        SQL.Fill(data, "Detail");
    }
    void BindGrid(ref DataSet data)
    {
        data.Tables[0].DefaultView.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"];
        GridView1.DataSource = data.Tables[0].DefaultView;
        GridView1.DataBind();
        //DisableViewState(GridView1);
    }
    protected void GridView1_SortCommand(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["SortExpression"].ToString() == e.SortExpression)
        {
            if (ViewState["SortDirection"].ToString() == "ASC")
                ViewState["SortDirection"] = "DESC";
            else
                ViewState["SortDirection"] = "ASC";
        }
        else
            ViewState["SortDirection"] = "ASC";
        ViewState["SortExpression"] = e.SortExpression;
        BindGrid(ref data);
    }
    protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        ViewState["CurrentPageIndex"] = e.NewPageIndex;
        BindGrid(ref data);
    }
    private void DisableViewState(GridView gv)
    {
        foreach (GridViewRow gvi in gv.Rows)
        {
            gvi.EnableViewState = false;
        }
    }
    protected void GridView1_SortCommand(object sender, EventArgs e)
    {
        if (ViewState["SortExpression"].ToString() == ((System.Web.UI.WebControls.LinkButton)(sender)).Text)
        {
            if (ViewState["SortDirection"].ToString() == "ASC")
                ViewState["SortDirection"] = "DESC";
            else
                ViewState["SortDirection"] = "ASC";
        }
        else
            ViewState["SortDirection"] = "ASC";
        ViewState["SortExpression"] = ((System.Web.UI.WebControls.LinkButton)(sender)).Text;
        SelectedIndexChanged(sender, e);
        //BindGrid(ref data);
        //PopulateLists(ref data);        
    }
    void PopulateLists(ref DataSet data)
    {//Dynamically populate the drop down lists based on current view
        System.Collections.Hashtable player, set1, set2, set3;
        player = new System.Collections.Hashtable();
        set1 = new System.Collections.Hashtable();
        set2 = new System.Collections.Hashtable();
        set3 = new System.Collections.Hashtable();
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            if (!player.Contains(data.Tables[0].Rows[i]["Player Name"].ToString()))
            {
                player.Add(data.Tables[0].Rows[i]["Player Name"].ToString(), data.Tables[0].Rows[i]["Player Name"].ToString());
            }
            if (!set1.Contains(data.Tables[0].Rows[i]["Set 1"].ToString()))
            {
                set1.Add(data.Tables[0].Rows[i]["Set 1"].ToString(), data.Tables[0].Rows[i]["Set 1"].ToString());
            }
            if (!set2.Contains(data.Tables[0].Rows[i]["Set 2"].ToString()))
            {
                set2.Add(data.Tables[0].Rows[i]["Set 2"].ToString(), data.Tables[0].Rows[i]["Set 2"].ToString());
            }
            if (!set3.Contains(data.Tables[0].Rows[i]["Set 3"].ToString()))
            {
                set3.Add(data.Tables[0].Rows[i]["Set 3"].ToString(), data.Tables[0].Rows[i]["Set 3"].ToString());
            }
        }
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[1].Controls[3]).DataSource = player.Values;
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[1].Controls[3]).DataBind();
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[1].Controls[3]).Items.Insert(0, new ListItem("*", ""));
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[2].Controls[3]).DataSource = set1.Values;
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[2].Controls[3]).DataBind();
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[2].Controls[3]).Items.Insert(0, new ListItem("*", ""));
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[3].Controls[3]).DataSource = set2.Values;
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[3].Controls[3]).DataBind();
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[3].Controls[3]).Items.Insert(0, new ListItem("*", ""));
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[4].Controls[3]).DataSource = set3.Values;
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[4].Controls[3]).DataBind();
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[4].Controls[3]).Items.Insert(0, new ListItem("*", ""));
    }
    protected void SelectedIndexChanged(object sender, EventArgs e)
    {//Drop box
        ListItem list1, list2, list3, list4;
        list1 = ((DropDownList)GridView1.Controls[0].Controls[0].Controls[1].Controls[3]).SelectedItem;
        list2 = ((DropDownList)GridView1.Controls[0].Controls[0].Controls[2].Controls[3]).SelectedItem;
        list3 = ((DropDownList)GridView1.Controls[0].Controls[0].Controls[3].Controls[3]).SelectedItem;
        list4 = ((DropDownList)GridView1.Controls[0].Controls[0].Controls[4].Controls[3]).SelectedItem;
        GetDataFiltered(ref data);
        BindGrid(ref data);
        PopulateLists(ref data);
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[1].Controls[3]).SelectedIndex = ((DropDownList)GridView1.Controls[0].Controls[0].Controls[1].Controls[3]).Items.IndexOf(list1);
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[2].Controls[3]).SelectedIndex = ((DropDownList)GridView1.Controls[0].Controls[0].Controls[2].Controls[3]).Items.IndexOf(list2);
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[3].Controls[3]).SelectedIndex = ((DropDownList)GridView1.Controls[0].Controls[0].Controls[3].Controls[3]).Items.IndexOf(list3);
        ((DropDownList)GridView1.Controls[0].Controls[0].Controls[4].Controls[3]).SelectedIndex = ((DropDownList)GridView1.Controls[0].Controls[0].Controls[4].Controls[3]).Items.IndexOf(list4);
    }
}

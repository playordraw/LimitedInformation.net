// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
//
// File: deck.aspx.cs
//
// Copyright (c) 2023 Jeffrey Reynolds
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class deck : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.Url.ToString().Contains("deck.aspx"))
        {
            HttpContext.Current.Response.Redirect("Commingsoon.aspx");
        }
    }
}

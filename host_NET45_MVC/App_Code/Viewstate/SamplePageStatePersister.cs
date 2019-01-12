
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// -----------------------------------------------------------------------------------------------------------------------------------------------------------------
/// This class is the page state persister for the application.
/// 
/// Created by Jason Hill on 26/6/2007.
/// -----------------------------------------------------------------------------------------------------------------------------------------------------------------
/// </summary>
public class SamplePageStatePersister : System.Web.UI.PageStatePersister
{


    private Page _page;
    /// -----------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <remarks>
    /// <author>jhill</author>
    /// <creation>Wednesday, 30 May 2007</creation>
    /// </remarks>
    /// <param name="page">Page.</param>
    /// -----------------------------------------------------------------------------------------------------------------------------------------------------------------
    public SamplePageStatePersister(Page page)
        : base(page)
    {
        _page = page;
    }

    /// -----------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Get the unique ID for the view state.
    /// </summary>
    /// <remarks>
    /// <author>jhill</author>
    /// <creation>Wednesday, 30 May 2007</creation>
    /// </remarks>
    /// -----------------------------------------------------------------------------------------------------------------------------------------------------------------
    private Guid GetViewStateID()
    {
        string viewStateKey = null;

        // Get the ID from the request
        viewStateKey = _page.Request["__VIEWSTATEID"];

        // Assign a new ID if we don't have one in the request
        if (string.IsNullOrEmpty(viewStateKey))
        {
            return Guid.NewGuid();
        }

        // Use the ID from the request if it is valid, else assign a new ID
        try
        {
            return new Guid(viewStateKey);
        }
        catch (FormatException generatedExceptionName)
        {
            return Guid.NewGuid();
        }

    }

    /// -----------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Load the view state from persistent medium.
    /// </summary>
    /// <remarks>
    /// <author>jhill</author>
    /// <creation>Wednesday, 30 May 2007</creation>
    /// </remarks>
    /// -----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public override void Load()
    {
        // Load view state from DB
        string pageViewState = PageViewStateServices.GetByID(GetViewStateID());

        if (pageViewState == null)
        {
            ViewState = null;
            ControlState = null;

        }
        else
        {
            // Deserialize into a Pair of ViewState and ControlState objects
            IStateFormatter formatter = StateFormatter;
            Pair statePair = (Pair)formatter.Deserialize(pageViewState);

            // Update ViewState and ControlState
            ViewState = statePair.First;
            ControlState = statePair.Second;
        }

    }

    /// -----------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Save the view state to persistent medium.
    /// </summary>
    /// <remarks>
    /// <author>jhill</author>
    /// <creation>Wednesday, 30 May 2007</creation>
    /// </remarks>
    /// <param name="viewState">View state to save.</param>
    /// -----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public override void Save()
    {
        // Create a pair for ViewState and ControlState
        Pair statePair = new Pair(ViewState, ControlState);
        IStateFormatter formatter = StateFormatter;

        // Save the view state
        Guid id = GetViewStateID();
        PageViewStateServices.Save(id, formatter.Serialize(statePair));

        // Store the ID of the view state in a hidden form field
        HtmlInputHidden control = _page.FindControl("__VIEWSTATEID") as HtmlInputHidden;
        if (control == null)
        {
            ScriptManager.RegisterHiddenField(_page, "__VIEWSTATEID", id.ToString());
        }
        else
        {
            control.Value = id.ToString();
        }

    }


}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================

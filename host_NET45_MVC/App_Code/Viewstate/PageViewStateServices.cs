
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;


/// <summary> 
/// ----------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/// This class provides services for handling page view state. 
///  
/// Created by Jason Hill on 26/6/2007. 
/// ----------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/// </summary> 
public sealed class PageViewStateServices
{
    private PageViewStateServices()
    {
    }

    /// ----------------------------------------------------------------------------------------------------------------------------------------------------------------- 
    /// <summary> 
    /// Get a page view state by ID. 
    /// </summary> 
    /// <remarks> 
    ///         <author>jhill</author> 
    ///         <creation>Wednesday, 30 May 2007</creation> 
    /// </remarks> 
    /// <param name="id">ID.</param> 
    /// ----------------------------------------------------------------------------------------------------------------------------------------------------------------- 
    public static string GetByID(Guid id)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MERCURYConnectionString"].ConnectionString))
        {
            connection.Open();

            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetByID";
                    command.Parameters.Add(new SqlParameter("@id", id));
                    return (string)command.ExecuteScalar();
                }

            }
            finally
            {
                connection.Close();
            }
        }

    }

    /// ----------------------------------------------------------------------------------------------------------------------------------------------------------------- 
    /// <summary> 
    /// Save the view state. 
    /// </summary> 
    /// <remarks> 
    ///         <author>jhill</author> 
    ///         <creation>Wednesday, 30 May 2007</creation> 
    /// </remarks> 
    /// <param name="id">Unique ID.</param> 
    /// <param name="value">View state value.</param> 
    /// ----------------------------------------------------------------------------------------------------------------------------------------------------------------- 

    public static void Save(Guid id, string value)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MERCURYConnectionString"].ConnectionString))
        {
            connection.Open();


            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SaveViewState";
                    command.Parameters.Add(new SqlParameter("@id", id));
                    command.Parameters.Add(new SqlParameter("@value", value));
                    command.ExecuteNonQuery();

                }

            }
            finally
            {
                connection.Close();
            }
        }

    }

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================

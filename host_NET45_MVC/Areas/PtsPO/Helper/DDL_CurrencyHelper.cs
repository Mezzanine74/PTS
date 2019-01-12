using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace host_NET45_MVC.Areas.PtsPO.Helper
{
    public class DDL_CurrencyHelper
    {
        public class DDL_Currency
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }

        public List<DDL_Currency> getDDL_Currency_List()
        {
            List<DDL_Currency> DDL_Currency_List = new List<DDL_Currency>();
            DDL_Currency_List.Add(new DDL_Currency { Text = "Ruble", Value = "Ruble" });
            DDL_Currency_List.Add(new DDL_Currency { Text = "Dollar", Value = "Dollar" });
            DDL_Currency_List.Add(new DDL_Currency { Text = "Euro", Value = "Euro" });
            return DDL_Currency_List;
        }

    }
}
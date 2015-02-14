using System.Web;
using System.Web.Mvc;

namespace Phase2_Group2_selucmps383_sp15_p2_g2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Models.Repository;
using WebApplication.Models;
using System.Web.Routing;
using WebApplication.Pages.Helpers;

namespace WebApplication.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private Repository repository = new Repository();
        private int pageSize = 4;
        protected int CurrentPage
        {
            get
            {
                int page;
                page = GetPageFromRequest();
                return page > MaxPage ? MaxPage : page;
            }
        }
        protected int MaxPage
        {
            get
            {
                int prodCount = FilterItems().Count();
                return (int)Math.Ceiling((decimal)prodCount / pageSize);
            }
        }
        private int GetPageFromRequest()
        {
            int page;
            string reqValue = (string)RouteData.Values["pages"] ?? Request.QueryString["page"];
            return reqValue != null && int.TryParse(reqValue, out page) ? page : 1;
        }
        public IEnumerable<Item> GetItems()
        {
            return FilterItems().OrderBy(g => g.ItemId).Skip((CurrentPage - 1) * pageSize).Take(pageSize);
        }
        private IEnumerable<Item> FilterItems()
        {
            IEnumerable<Item> items = repository.Items;
            string currentCategory = (string)RouteData.Values["category"] ?? Request.QueryString["category"];
            return currentCategory == null ? items : items.Where(p => p.Category == currentCategory);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                int selectedItemId;
                if(int.TryParse(Request.Form["add"], out selectedItemId))
                {
                    Item selectedItem = repository.Items.Where(g => g.ItemId == selectedItemId).FirstOrDefault();
                    if(selectedItem!=null)
                    {
                        SessionHelper.GetCart(Session).AddItem(selectedItem, 1);
                        SessionHelper.Set(Session, SessionKey.RETURN_URL, Request.RawUrl);
                        Response.Redirect(RouteTable.Routes.GetVirtualPath(null, "cart", null).VirtualPath);
                    }
                }
            }
        }
    }
}
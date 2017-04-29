using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Models;
using WebApplication.Models.Repository;
using WebApplication.Pages.Helpers;

namespace WebApplication.Pages
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            checkoutForm.Visible = true;
            checkoutMessage.Visible = false;
            if (IsPostBack)
            {
                Order myOrder = new Order();
                if (TryUpdateModel(myOrder, new FormValueProvider(ModelBindingExecutionContext)))
                {
                    myOrder.OrderLines = new List<OrderLine>();
                    Cart myCart = SessionHelper.GetCart(Session);

                    foreach (CartLine line in myCart.Lines)
                    {
                        myOrder.OrderLines.Add(new OrderLine { Order = myOrder, Item = line.Item, Quantity = line.Quantity });
                    }
                    new Repository().SaveOrder(myOrder);
                    myCart.Clear();
                    checkoutForm.Visible = false;
                    checkoutMessage.Visible = true;
                }
            }
        }
    }
}
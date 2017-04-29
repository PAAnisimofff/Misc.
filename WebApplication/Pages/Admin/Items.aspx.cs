using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Models;
using WebApplication.Models.Repository;

namespace WebApplication.Pages.Admin
{
    public partial class Items : System.Web.UI.Page
    {
        private Repository repository = new Repository();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public IEnumerable<Item> GetITems()
        {
            return repository.Items;
        }
        public void UpdateItem(int ItemID)
        {
            Item myItem = repository.Items.Where(p => p.ItemId == ItemID).FirstOrDefault();
            if (myItem != null && TryUpdateModel(myItem, new FormValueProvider(ModelBindingExecutionContext)))
            {
                repository.SaveItem(myItem);
            }
        }
        public void DeleteItem(int ItemID)
        {
            Item myItem = repository.Items.Where(p => p.ItemId == ItemID).FirstOrDefault();
            if (myItem != null)
            {
                repository.DeleteItem(myItem);
            }
        }
        public void InsertItem()
        {
            Item myItem = new Item();
            if (TryUpdateModel(myItem, new FormValueProvider(ModelBindingExecutionContext)))
            {
                repository.SaveItem(myItem);
            }
        }
    }
}

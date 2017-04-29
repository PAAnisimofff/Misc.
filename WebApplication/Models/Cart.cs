using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Cart
    {
        private List<CartLine> LineCollection = new List<CartLine>();
        public void AddItem(Item item, int quantity)
        {
            CartLine line = LineCollection.Where(p => p.Item.ItemId == item.ItemId).FirstOrDefault();
            if (line == null)
            {
                LineCollection.Add(new CartLine { Item = item, Quantity = quantity });
            }
            else
                line.Quantity += quantity;
        }
        public void RemoveLine(Item item)
        {
            LineCollection.RemoveAll(l => l.Item.ItemId == item.ItemId);
        }
        public decimal ComputeTotalValue()
        {
           return LineCollection.Sum(e => e.Item.Price * e.Quantity);
        }
        public void Clear()
        {
            LineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return LineCollection; }
        }
    }
    public class CartLine
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}
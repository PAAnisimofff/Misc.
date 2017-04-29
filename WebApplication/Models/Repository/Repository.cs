﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication.Models.Repository
{
    public class Repository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Item> Items
        {
            get { return context.Items; }
        }
        public IEnumerable<Order> Orders
        {
            get
            {
                return context.Orders.Include(o => o.OrderLines.Select(ol => ol.Item));
            }
        }
        public void SaveItem(Item item)
        {
            if (item.ItemId == 0)
            {
                item = context.Items.Add(item);
            }
            else
            {
                Item dbItem = context.Items.Find(item.ItemId);
                if(dbItem != null)
                {
                    dbItem.Name = item.Name;
                    dbItem.Description = item.Description;
                    dbItem.Price = item.Price;
                    dbItem.Category = item.Category;
                }
            }
            context.SaveChanges();
        }
        public void DeleteItem(Item item)
        {
            IEnumerable<Order> orders = context.Orders.Include(o => o.OrderLines.Select(ol => ol.Item)).Where(o => o.OrderLines.Count(ol => ol.Item.ItemId == item.ItemId) > 0).ToArray();
            foreach (Order order in orders)
            {
                context.Orders.Remove(order);
            }
            context.Items.Remove(item);
            context.SaveChanges();
        }
        public void SaveOrder(Order order)
        {
            if (order.OrderId == 0)
            {
                order = context.Orders.Add(order);
                foreach (OrderLine line in order.OrderLines)
                {
                    context.Entry(line.Item).State = EntityState.Modified;
                }
            }
            else
            {
                Order dbOrder = context.Orders.Find(order.OrderId);
                if (dbOrder != null)
                {
                    dbOrder.Name = order.Name;
                    dbOrder.Line1 = order.Line1;
                    dbOrder.Line2 = order.Line2;
                    dbOrder.Line3 = order.Line3;
                    dbOrder.City = order.City;
                    dbOrder.GiftWrap = order.GiftWrap;
                    dbOrder.Dispatched = order.Dispatched;
                }
            }
            context.SaveChanges();
        }
    }
}
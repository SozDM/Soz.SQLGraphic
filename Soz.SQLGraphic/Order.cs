﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace Soz.SQLConsole
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public virtual UserManager User { get; set; }

        public Order()
        {
        }


        //returns order info

        public string GetInfo(int OrderId)
        {
            using (var context = new MyDBContext())
            {
                Order order = context.Orders.First(item => item.Id == OrderId);
                string OrderInfo = "";
                OrderInfo = $"#{order.Id}\tDate:{order.Date}\tUserId:{order.UserId}";
                OrderInfo += $"\tamount:{order.Amount}\tdescription: {order.Description}\n";

                return OrderInfo;
            }
        }


        //adds random orders into database

        public void AddRandom(int HowMany, List<int> UserIdList)
        {
            var rnd = new Random();
            using (var context = new MyDBContext())
            {
                for (int i = 0; i < HowMany; i++)
                {
                    Amount = rnd.Next(10, 100);
                    Description = "Order made by random";
                    UserId = rnd.Next(UserIdList.Count);

                    var order = new Order();
                    order.Amount = Amount;
                    order.Description = Description;
                    order.UserId = UserIdList[UserId];
                    order.Date = DateTime.Now;

                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }
        }


        // if there are no orders returns list[0] = 0
        // else returns list of order id

        public List<int> GetOrdersIdByUser(int userId)
        {
            var OrderIdList = new List<int>();
            using (var context = new MyDBContext())
            {
                var orders = context.Orders;

                int OrdersByThisUser = 0;

                foreach (var item in orders)
                {
                    if (item.UserId == userId)
                    {
                        OrdersByThisUser++;
                        OrderIdList.Add(item.Id);
                    }
                }
                if (OrdersByThisUser == 0) OrderIdList.Add(0);

                return OrderIdList;
            }
        }


        //add order to database

        public int Add(int userId, int amount, string description)
        {
            using (var context = new MyDBContext())     //saving user to database
            {
                var order = new Order
                {
                    Amount = amount,
                    Description = description,
                    Date = DateTime.Now,
                    UserId = userId
                };

                context.Orders.Add(order);
                context.SaveChanges();
                Id = order.Id;
            }
            return Id;
        }


        //places new info into order

        public void Edit(int orderId, int amount, string description)
        {
            using (var context = new MyDBContext())
            {
                var orders = context.Orders;

                var OrderToEdit = context.Orders.First(order => order.Id == orderId);

                OrderToEdit.Amount = amount;
                OrderToEdit.Description = description;
                context.SaveChanges();
            }
        }


        //delete order from database

        public void Delete(int orderId)
        {
            using (var context = new MyDBContext())
            {
                var orders = context.Orders;

                var OrderToDelete = context.Orders.First(order => order.Id == orderId);
                context.Orders.Remove(OrderToDelete);
                context.SaveChanges();
            }
        }
    }
}
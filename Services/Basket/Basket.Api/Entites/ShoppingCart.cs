﻿using System.Collections.Generic;
using System.Linq;

namespace Basket.Api.Entites
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public double TotalPrice
        {
            get
            {
                double totalPrice = 0;
                if( Items != null && Items.Any()) {
                    foreach (var item in Items)
                    {
                        totalPrice += item.Price * item.Quantity;

                    }
                }
              
                return totalPrice;
            }
        }

        public ShoppingCart()
        {
            
        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
            
        }
    }
}

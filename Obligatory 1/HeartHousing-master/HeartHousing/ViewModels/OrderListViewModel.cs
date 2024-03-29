﻿using HeartHousing.Models;
using Microsoft.AspNetCore.Mvc;

namespace HeartHousing.ViewModels
{
    public class OrderListViewModel
    {
        public IEnumerable<Order> Orders;
        public string? CurrentViewName;

        public OrderListViewModel(IEnumerable<Order> orders, string? currentViewName)
        {
            Orders = orders;
            CurrentViewName = currentViewName;
        }
    }
}

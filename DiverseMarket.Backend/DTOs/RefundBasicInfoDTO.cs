﻿using DiverseMarket.Backend.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.DTOs
{
    public class RefundBasicInfoDTO
    {
        public long Id { get; private set; }
        public string ProductName { get; private set; }
        public string CompanyName { get; private set; }
        public RefundStatus Status { get; private set; }
        public double TotalAmount { get; private set; }

        public RefundBasicInfoDTO(long id, string productName, string companyName, RefundStatus status, double totalAmount)
        {
            Id = id;
            ProductName = productName;
            CompanyName = companyName;
            Status = status;
            TotalAmount = totalAmount;
        }
    }
}

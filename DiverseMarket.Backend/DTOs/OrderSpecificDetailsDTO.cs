using DiverseMarket.Backend.Model.Companies;
using DiverseMarket.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.DTOs
{
    public class OrderSpecificDetailsDTO
    {
        public OrderBasicInfoDTO OrderInfo { get; set; }
        public User User { get; set; }
        public Address Address { get; set; }

        public OrderSpecificDetailsDTO(OrderBasicInfoDTO orderBasicInfo, User user, Address address)
        {
            OrderInfo = orderBasicInfo;
            User = user;
            Address = address;
        }
    }
}

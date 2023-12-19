using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.DomainLayer.Model.Transactions.Reviews
{
    internal abstract class Review
    {
        public string Id { get; private set; }
        public ReviewEnum ReviewEnum { get; private set; }
        public string? Comment { get; private set; }

        public Review(ReviewEnum reviewEnum, string? comment = null)
        {
            Id = Math.Abs(Guid.NewGuid().GetHashCode()).ToString();
            Comment = comment ?? string.Empty;
            ReviewEnum = reviewEnum;
        }
    }
}

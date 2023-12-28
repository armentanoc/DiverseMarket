using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.DomainLayer.Model.Transactions.Reviews
{
    public abstract class Review
    {
        public int Id { get; private set; }
        public ReviewEnum ReviewEnum { get; private set; }
        public string? Comment { get; private set; }

        public Review(ReviewEnum reviewEnum, string? comment = null)
        {
            Comment = comment ?? string.Empty;
            ReviewEnum = reviewEnum;
        }
        public void SetId(int newId)
        {
            Id = newId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace SI_Lab_02
{
    class DomainOrderRandom : IDomainOrder
    {
        public List<int> GetOrder(List<int> domain)
        {
            Random rnd = new Random();
            return domain.OrderBy(x => rnd.Next()).ToList();
        }
    }
}

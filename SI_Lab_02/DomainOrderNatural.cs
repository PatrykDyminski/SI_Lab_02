using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Lab_02
{
    class DomainOrderNatural : IDomainOrder
    {
        public List<int> GetOrder(List<int> domain)
        {
            return domain;
        }
    }
}

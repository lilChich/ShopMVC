using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.BLL.Models
{
    public enum SortType
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc,
    }

    public class SortViewModel
    {
        public SortType NameSort { get; private set; }
        public SortType PriceSort { get; private set; }
        public SortType Current { get; private set; }
        public SortViewModel(SortType sortOrder)
        {
            NameSort = sortOrder == SortType.NameAsc ? SortType.NameDesc : SortType.NameAsc;
            PriceSort = sortOrder == SortType.PriceAsc ? SortType.PriceDesc : SortType.PriceAsc;
            Current = sortOrder;
        }
    }
}

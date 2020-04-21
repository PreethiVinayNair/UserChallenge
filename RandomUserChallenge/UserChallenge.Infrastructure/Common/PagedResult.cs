namespace VMS.Infrastructure.Common
{
    public class PagedResult<TItem>
    {
        public PagedResult(long total, TItem[] items)
        {
            Total = total;
            Items = items;
        }

        public long Total { get; set; }
        public TItem[] Items { get; set; }

        public static PagedResult<TItem> Empty()
        {
            return new PagedResult<TItem>(0, new TItem[0]);
        }
    }
}
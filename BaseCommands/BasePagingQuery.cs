namespace BaseCommands
{
    public abstract class BasePagingQuery : BaseCommand
    {
        public abstract int PageIndex { get; set; }
        public abstract int PageSize { get; set; }
        public int Offset => PageIndex * PageSize;
        protected BasePagingQuery()
        {
            
        }
        protected BasePagingQuery(string processUid, int pageIndex = 0, int pageSize = 30) : base(processUid)
        {
            pageIndex = pageIndex;
            pageSize = pageSize;
        }
    }
}
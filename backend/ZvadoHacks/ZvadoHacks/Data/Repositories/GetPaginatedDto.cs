namespace ZvadoHacks.Data.Repositories
{
    public class GetPaginatedDto<T>
    {
        public List<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}

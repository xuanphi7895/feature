namespace Core.Specifications {
    public class ProductSpecParam {
        private const int MaxPageSize = 50;

        public int PageIndex { get; set; } = 10;

        private int _pageSize = 6;

        public int PageSize {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public string Sort { get; set; }
    }
}
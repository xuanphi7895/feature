using Core.Enum;

namespace Core.Entities {
    public class Category : BaseEntity {
        public int SortOrder { set; get; }
        public bool IsShowOnHome { set; get; }
        public int? ParentId { set; get; }
        public Status Status { set; get; }
    }
}
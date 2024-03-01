namespace Common.Domain.Entities
{
    public sealed class BaseList<T> where T : BaseEntity
    {
        #region Properties
        public uint? Page { get; set; }
        public uint? PageLength { get; set; }
        public IReadOnlyCollection<T>? List { get; set; }
        #endregion
    }
}

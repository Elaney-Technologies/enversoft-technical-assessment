namespace SupplierPortal.Domain.Entities;

public class Supplier
{
    public int Id { get; set; }
    public int Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TelephoneNo { get; set; } = string.Empty;
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}

namespace AutoCarERP.Core.Entities;

public abstract class Entity
{
    public int Codigo { get; set; }

    public DateTime Created_At { get; set; }
    public DateTime Updated_At { get; set; }
    public DateTime Deleted_At { get; set; }
}

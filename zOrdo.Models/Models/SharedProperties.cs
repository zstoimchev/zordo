namespace zOrdo.Models.Models;

public class SharedProperties
{
    /// <summary>
    /// Primary key identifier
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Insertion timestamp in UTC
    /// </summary>
    public DateTime InsertedOnUtc { get; set; }
    
    /// <summary>
    /// Update timestamp in UTC
    /// </summary>
    public DateTime? UpdatedOnUtc { get; set; }
    
    /// <summary>
    /// Deletion timestamp in UTC
    /// </summary>
    public DateTime DeletedOnUtc { get; set; }
    
    /// <summary>
    /// Deleted by identifier
    /// </summary>
    public string? DeletedBy { get; set; }
}
﻿namespace ShfMgmtEgApi.Core.Entities;

public class BaseEntity
{
    
    public string Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public string CreatedBy { get; set; }
    
    public string UpdatedBy { get; set; }

    public bool IsDeleted { get; set; } = false;
    
    public DateTime DeletedAt { get; set; }
    
    public string DeletedBy { get; set; }
}
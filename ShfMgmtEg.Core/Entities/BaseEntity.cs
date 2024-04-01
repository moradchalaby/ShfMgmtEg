﻿using System.ComponentModel.DataAnnotations;

namespace ShfMgmtEg.Core.Entities;

public class BaseEntity
{
    [Key] public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}
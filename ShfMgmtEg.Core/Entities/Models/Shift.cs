﻿using System.ComponentModel.DataAnnotations.Schema;
using ShfMgmtEg.Core.Enums;

namespace ShfMgmtEg.Core.Entities.Models;

public class Shift : BaseEntity
{
    
    public string Name { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public int TeamId { get; set; }
    
    [NotMapped]
    public Team Team { get; set; }
    
    public int ManagerId { get; set; }
    
    [NotMapped]
    public Employee Manager { get; set; }
    
    public string Description { get; set; }
    
    public Periot PeriodicType { get; set; } = Periot.AllDay;

}
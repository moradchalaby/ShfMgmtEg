﻿namespace ShfMgmtEg.Core.Entities.Models;

public class TeamEmployee
{
    public int TeamId { get; set; }
    public Team Team { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}
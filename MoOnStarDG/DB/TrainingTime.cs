using System;
using System.Collections.Generic;

namespace MoOnStarDG.DB;

public partial class TrainingTime
{
    public int Id { get; set; }

    public DateTime? DateTime { get; set; }

    public string? Duration { get; set; }

    public int? IdTraining { get; set; }

    public virtual ICollection<Training> Training { get; set; } = new List<Training>();
}

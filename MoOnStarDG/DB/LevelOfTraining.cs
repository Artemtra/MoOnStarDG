using System;
using System.Collections.Generic;

namespace MoOnStarDG.DB;

public partial class LevelOfTraining
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Sportsman> Sportsmen { get; set; } = new List<Sportsman>();
}

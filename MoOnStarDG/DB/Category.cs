using System;
using System.Collections.Generic;

namespace MoOnStarDG.DB;

public partial class Category
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Sportsman> Sportsmen { get; set; } = new List<Sportsman>();
}

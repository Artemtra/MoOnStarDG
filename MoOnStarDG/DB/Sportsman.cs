using System;
using System.Collections.Generic;

namespace MoOnStarDG.DB;

public partial class Sportsman
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? FirstName { get; set; }

    public string? DdataBirsDay { get; set; }

    public int? IdCategory { get; set; }

    public int? IdLevel { get; set; }

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual LevelOfTraining? IdLevelNavigation { get; set; }

    public virtual ICollection<Training> Training { get; set; } = new List<Training>();
}

using System;
using System.Collections.Generic;

namespace MoOnStarDG.DB;

public partial class Sportsman
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? FirstName { get; set; }

    public string? DataBirsDay { get; set; }

    public int? IdCategory { get; set; }

    public int? IdLevel { get; set; }

    public int? IdTraning { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual LevelOfTraining? IdLevelNavigation { get; set; }

    public virtual Training? IdTraningNavigation { get; set; }

}


using System;
using System.Collections.Generic;

namespace MoOnStarDG.DB;

public partial class Grade
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Comment { get; set; }

    public int? IdSportsman { get; set; }

    public virtual Sportsman? IdSportsmanNavigation { get; set; }
}

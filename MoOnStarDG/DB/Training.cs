using System;
using System.Collections.Generic;

namespace MoOnStarDG.DB;

public partial class Training
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? TypeId { get; set; }

    public string? TrainingTime { get; set; }

    public string? TrainingDate { get; set; }

    public int? IdSportsmen { get; set; }

    public int? IdGrade { get; set; }

    public virtual Grade? IdGradeNavigation { get; set; }

    public virtual Sportsman? IdSportsmenNavigation { get; set; }

    public virtual Type? Type { get; set; }
}

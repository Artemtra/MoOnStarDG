using System;
using System.Collections.Generic;

namespace MoOnStarDG.DB;

public partial class Training
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateTime TrainingDate { get; set; }


    public int? TypeId { get; set; }

    public int? IdTrainingTime { get; set; }

    public virtual TrainingTime? IdTrainingTimeNavigation { get; set; }

    public virtual ICollection<Sportsman> Sportsmen { get; set; } = new List<Sportsman>();

    public virtual Type? Type { get; set; }
}

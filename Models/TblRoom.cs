using System;
using System.Collections.Generic;

namespace GastroliverWithDb.Models;

public partial class TblRoom
{
    public int RoomId { get; set; }

    public string RoomNo { get; set; } = null!;

    public virtual ICollection<TblPatient> TblPatients { get; set; } = new List<TblPatient>();
}

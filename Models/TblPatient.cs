using System;
using System.Collections.Generic;

namespace GastroliverWithDb.Models;

public partial class TblPatient
{
    public int PatientId { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Nid { get; set; }

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public int RoomId { get; set; }

    public virtual TblRoom Room { get; set; } = null!;
}

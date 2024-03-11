using System;
using System.Collections.Generic;

namespace uszoeb_Gyurko_Levente_backend.Models;

public partial class Versenyzok
{
    public int? Id { get; set; }

    public string? Nev { get; set; }

    public int? OrszagId { get; set; }

    public string? Nem { get; set; }
}

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace uszoeb_Gyurko_Levente_backend.Models;

public class Szamok
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Nev { get; set; }

    [JsonIgnore]
    public int VersenyzoId { get; set; }
}
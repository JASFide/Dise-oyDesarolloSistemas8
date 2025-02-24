using System;
using System.Collections.Generic;

namespace administracionScoutsCR.Models;

public partial class ReqInsignium
{
    public int IdReqInsignia { get; set; }

    public string? Descripcion { get; set; }

    public int? IdInsignias { get; set; }

    public virtual Insignia? IdInsigniasNavigation { get; set; }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLCGen.ViewModels.Enums
{
    [Flags]
    public enum SynchronisatieTypeEnum
    {
        Conflict = 0x1,
        GarantieConflict = 0x2,
        Gelijkstart = 0x4,
        Voorstart = 0x8,
        Naloop = 0x10,
        SomeConflict = Conflict | GarantieConflict,
        SomeSynchronisatie = Gelijkstart | Voorstart | Naloop
    }
}

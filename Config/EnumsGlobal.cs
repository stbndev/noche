using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche
{
    public enum Action
    {
        CREATE,
        READID,
        READALL,
        UPDATE,
        DELETE,
        DELETEPHYSICAL,
    }
    public enum CSTATUS
    {
        ACTIVO = 1,
        INACTIVO = 2,
        ELIMINADO = 3,
        CANCELADO = 4,
    }
    public class EnumsGlobal
    {

    }
}

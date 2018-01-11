using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc
{
    public class Globals
    {
        //para avon public const string REFERENCIA_NAME_XLS_FONDEO = "[Número de Pedimento]";

        public const string REFERENCIA_NAME_XLS_FONDEO = "REFERENCIA";
        public const int EST_CAPTURA = 0;
        public const int EST_INV_SIN_APROBACION = 1;
        public const int EST_INV_CON_APROBACION = 2;
        public const int EST_MAQ_PAR_SIN_CERRAR = 3;
        public const int EST_MAQ_PAR_CERRADA = 4;
        public const int EST_MAQ_TOT_CERRADA = 5;
        public const int EST_REM_PARCIAL = 6;
        public const int EST_REM_TOTAL = 7;
        public const int EST_ORC_CAPTURADA = 8;

        public const int ROL_ADMNISTRADOR = 1;

        public const int AVON_FONDEO = 1;

        public const int ORDEN_CARGA_CANT_REM_X_HOJA = 16;
    }
}

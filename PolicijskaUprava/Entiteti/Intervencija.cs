using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaUprava.Entiteti
{
    public class Intervencija
    {
        public virtual string Opis { set; get; }
        public virtual DateTime Datum { set; get; }
        public virtual string Vreme { set; get; }
        public virtual int IdIntervencije { set; get; }
        public virtual string imeObjekta { set; get; }

        public virtual Patrola IntervencijaUPatroli { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaUprava.Entiteti
{
    public class Uprava
    {
        public virtual string Grad { get; set; }
        public virtual int IdUprave { get; set; }

        public virtual IList<Osoba> ZaposljenOsoba { get; set; }
        public virtual IList<Stanica> SastojiSeStanica { get; set; }
        public virtual Nacelnik RukovodiNacelnik { get; set; }

        public Uprava()
        {
            SastojiSeStanica = new List<Stanica>();
            ZaposljenOsoba = new List<Osoba>();
        }
    }
}

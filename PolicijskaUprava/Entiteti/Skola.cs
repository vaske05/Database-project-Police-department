using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaUprava.Entiteti
{
    public class Skola
    {
        public virtual string Naziv { get; set; }
        public virtual string Tip { get; set; }
        public virtual string OsobaZaKontakt { get; set; }
        public virtual string Adresa { get; set; }
        public virtual int Broj { get; set; }
        public virtual int IdSkole { get; set; }
        
        public virtual SkolskiPolicajac ZaduzenSkolskiPolicajac { get; set; }
    }
}

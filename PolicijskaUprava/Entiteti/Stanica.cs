using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaUprava.Entiteti
{
    public class Stanica
    {
        public virtual string Naziv { get; set; }
        public virtual int IdStanice { get; set; }
        public virtual string Adresa { get; set; }
        public virtual string Opstina { get; set; }
        public virtual DateTime DatumOsnivanja { get; set; }
        public virtual int BrojVozila { get; set; }

        public virtual Uprava SastojiSeUprava { get; set; }
        public virtual Sef SefujeSef { get; set; }
        public virtual IList<Objekat> ObezbedjujeObjekat { get; set; }

        public Stanica()
        {
            ObezbedjujeObjekat = new List<Objekat>();
        }


    }
}

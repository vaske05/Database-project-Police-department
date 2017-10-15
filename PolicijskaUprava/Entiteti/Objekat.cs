using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaUprava.Entiteti
{
    public class Objekat
    {
        public virtual string Tip { get; set; }
        public virtual int IdObjekta { get; set; }
        public virtual int Povrsina { get; set; }
        public virtual string Adresa { get; set; }
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }
        public virtual string Telefon { get; set; }

        public virtual Stanica ObezbedjujeStanica { get; set; }
        public virtual AlarmniSistem InstaliranJeAlarmniSistem { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaUprava.Entiteti
{
    public class Vozilo
    {
        public virtual string Model { set; get; }
        public virtual string Tip { set; get; }
        public virtual string Proizvodjac { set; get; }
        public virtual string Boja { set; get; }
        public virtual int Registracija { set; get; }

        public virtual Patrola VoziloPatrole { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaUprava.Entiteti
{
    public class AlarmniSistem
    {
        
        public virtual string Opis { get; set; }
        public virtual DateTime DatumAtesta { get; set; }
        public virtual DateTime DatumInstalacije { get; set; }
        public virtual string Proizvodjac { get; set; }
        public virtual DateTime PocetniDatum { get; set; }
        public virtual DateTime KrajnjiDatum { get; set; }
        public virtual int SerijskiBroj { get; set; }
        public virtual string Model { get; set; }
        public virtual int GodinaProizvodnje { get; set; }
        

        public virtual IList<Objekat> InstaliranJeObjekat { get; set; }

        public AlarmniSistem()
        {
            InstaliranJeObjekat = new List<Objekat>();
        }
    }

    public class DetekcijaToplotnogOdraza: AlarmniSistem
    {
        public virtual int VertikalnaRezolucija { get; set; }
        public virtual int HorizontalnaRezolucija { get; set; }
    }

    public class DetekcijaPokreta: AlarmniSistem
    {
        public virtual int Osetljivost { get; set; }
    }

    public class Ultrazvucni: AlarmniSistem
    {
        public virtual int OpsegFrekvencija { get; set; }
    }
}

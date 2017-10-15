using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaUprava.Entiteti
{
    public class Sektor
    {
        public virtual int IdSektora { set; get; }
        public virtual ZamenikNacelnika Zamenici { set; get; }
        public virtual Sektor RadiUZamenik { get; set; }
    }

    public class VanredneSituacije: Sektor
    {
        public virtual IList<VanredniPolicajac> AngazovanVanredniPolicajac { set; get; }

        public VanredneSituacije()
        {
            AngazovanVanredniPolicajac = new List<VanredniPolicajac>();
        }
    }

    public class Saobracaj : Sektor
    {
        public virtual string NazivUlice { set; get; }
        public virtual IList<Pozornik> RasporedjenPozornik { set; get; }

        public Saobracaj()
        {
            RasporedjenPozornik = new List<Pozornik>();
        }
    }
}

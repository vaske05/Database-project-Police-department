using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaUprava.Entiteti
{
    public class Patrola
    {
        public virtual int IdPatrole { get; set; }
        public virtual IList<Intervencija> EvidentiraIntervenciju { get; set; }
        public virtual IList<OstaliPolicajci> PatroliraOstaliPolicajac { get; set; }
        public virtual Vozilo ZaduzujeVozilo { get; set; }
        public virtual Vozilo VodiVodja { get; set; }
        public Patrola()
        {
            EvidentiraIntervenciju = new List<Intervencija>();
            PatroliraOstaliPolicajac = new List<OstaliPolicajci>();
        }
    }
}

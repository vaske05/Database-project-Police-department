using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicijskaUprava.Entiteti;
using FluentNHibernate.Mapping;
namespace PolicijskaUprava.Mapiranja
{
    public class SkolaMapiranje :ClassMap<Skola>
    {
        public SkolaMapiranje()
        {
            Table("SKOLA");
            Id(x => x.IdSkole).Column("ID_SKOLE").GeneratedBy.Assigned();

            Map(x => x.Naziv).Column("NAZIV");
            Map(x => x.Tip).Column("TIP");
            Map(x => x.OsobaZaKontakt).Column("OSOBA_ZA_KONTAKT");
            Map(x => x.Broj).Column("BROJ");
            Map(x => x.Adresa).Column("ADRESA");

            //References(x => x.ZaduzenSkolskiPolicajac).Column("ID_SKOLE").LazyLoad();
        }
    }
}

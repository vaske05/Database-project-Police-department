using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicijskaUprava.Entiteti;
using FluentNHibernate.Mapping;


namespace PolicijskaUprava.Mapiranja
{
    public class IntervencijaMapiranje:ClassMap<Intervencija>
    {
        public IntervencijaMapiranje() {

            Id(x => x.IdIntervencije).Column("ID_INTERVENCIJE").GeneratedBy.Assigned();

            Table("INTERVENCIJA");
            Map(x => x.Opis).Column("OPIS");
            Map(x => x.Datum).Column("DATUM");
            Map(x => x.imeObjekta).Column("IME_OBJEKTA");
            Map(x => x.Vreme).Column("VREME");

            References(x => x.IntervencijaUPatroli).Column("ID_PATROLE").LazyLoad();
        }
    }
}

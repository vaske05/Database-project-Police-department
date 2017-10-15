using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicijskaUprava.Entiteti;
using FluentNHibernate.Mapping;

namespace PolicijskaUprava.Mapiranja
{
    public class PatrolaMapiranje : ClassMap<Patrola>
    {
        public PatrolaMapiranje()
        {
            Id(x => x.IdPatrole).Column("ID_PATROLE").GeneratedBy.Assigned();
            Table("PATROLA");

            HasMany(x => x.PatroliraOstaliPolicajac).KeyColumn("ID_PATROLE").LazyLoad().Cascade.All().Inverse();
            HasMany(x => x.EvidentiraIntervenciju).KeyColumn("ID_PATROLE").LazyLoad().Cascade.All().Inverse();
            References(x => x.ZaduzujeVozilo).Column("REGISTRACIJA_VOZILA").LazyLoad();
            //References(x => x.VodiVodja).Column("JMBG").LazyLoad();
        }
    }
}

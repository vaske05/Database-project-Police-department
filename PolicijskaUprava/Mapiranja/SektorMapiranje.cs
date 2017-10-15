using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicijskaUprava.Entiteti;
using FluentNHibernate.Mapping;

namespace PolicijskaUprava.Mapiranja
{
    public class SektorMapiranje : ClassMap<Sektor>
    {
        public SektorMapiranje()
        {
            Table("SEKTOR");
            Id(x => x.IdSektora).Column("ID_SEKTORA").GeneratedBy.Assigned();
            //References(x => x.RadiUZamenik).Column("ID_SEKTORAZ").LazyLoad();
        }
    }
    public class VanredneSituacijeMapiranje :SubclassMap<VanredneSituacije>
    {
        public VanredneSituacijeMapiranje()
        {
            Table("VANREDNE_SITUACIJE");
            KeyColumn("ID_SEKTORA");
            HasMany(x => x.AngazovanVanredniPolicajac).KeyColumn("ID_SEKTORAV").Cascade.All().Inverse();
            //References(x => x.RadiUZamenik).Column("ID_SEKTORAZ").LazyLoad();
        }
    }

    public class SaobracajMapiranje : SubclassMap<Saobracaj>
    {
        public SaobracajMapiranje()
        {
            Table("SAOBRACAJ");
            KeyColumn("ID_SEKTORA");
            Map(x => x.NazivUlice).Column("NAZIV_ULICE");
            HasMany(x => x.RasporedjenPozornik).KeyColumn("ID_SEKTORAS").Cascade.All().Inverse();
            //References(x => x.RadiUZamenik).Column("ID_SEKTORAZ").LazyLoad();
        }
    }
}
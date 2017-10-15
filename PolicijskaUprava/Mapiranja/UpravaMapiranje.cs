using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicijskaUprava.Entiteti;
using FluentNHibernate.Mapping;
namespace PolicijskaUprava.Mapiranja
{
    public class UpravaMapiranje : ClassMap<Uprava>
    { 
        public UpravaMapiranje()
        {
            Table("POLICIJSKA_UPRAVA");
            Id(x => x.IdUprave).Column("ID_UPRAVE").GeneratedBy.Assigned();
            Map(x => x.Grad).Column("GRAD");

            HasMany(x => x.SastojiSeStanica).KeyColumn("ID_UPRAVE").LazyLoad().Inverse();
            HasMany(x => x.ZaposljenOsoba).KeyColumn("ID_UPRAVE_OSOBA").LazyLoad().Inverse();
            //References(x => x.RukovodiNacelnik).Column("ID_UPRAVE").LazyLoad();
        }
    }
}
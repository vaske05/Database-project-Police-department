using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicijskaUprava.Entiteti;
using FluentNHibernate.Mapping;
namespace PolicijskaUprava.Mapiranja
{
    public class VozloMapiranje : ClassMap<Vozilo>
    {
        public VozloMapiranje()
        {
            Table("VOZILO");
            Id(x => x.Registracija).Column("REGISTRACIJA").GeneratedBy.Assigned();

            Map(x => x.Tip).Column("TIP");
            Map(x => x.Model).Column("MODEL");
            Map(x => x.Proizvodjac).Column("PROIZVODJAC");
            Map(x => x.Boja).Column("BOJA");

          //  References(x => x.VoziloPatrole).Column("REGISTRACIJA").LazyLoad();
        }
    }
}

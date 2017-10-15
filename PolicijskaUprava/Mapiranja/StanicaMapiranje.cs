using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicijskaUprava.Entiteti;
using FluentNHibernate.Mapping;
namespace PolicijskaUprava.Mapiranja
{
    public class StanicaMapiranje : ClassMap<Stanica>
    {
        public StanicaMapiranje()
        {
            Table("POlICIJSKA_STANICA");
            Id(x => x.IdStanice).Column("ID_STANICE").GeneratedBy.Assigned();
            Map(x => x.Naziv).Column("NAZIV");
            Map(x => x.Adresa).Column("ADRESA");
            Map(x => x.BrojVozila).Column("BROJ_VOZILA");
            Map(x => x.DatumOsnivanja).Column("DATUM_OSNIVANJA");
            Map(x => x.Opstina).Column("OPSTINA");

            HasMany(x => x.ObezbedjujeObjekat).KeyColumn("ID_STANICE").LazyLoad().Cascade.All().Inverse();
            References(x => x.SastojiSeUprava).Column("ID_UPRAVE").LazyLoad();
            References(x => x.SefujeSef).Column("JMBG_SEFA").LazyLoad();
        }
    }
}

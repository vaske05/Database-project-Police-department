using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicijskaUprava.Entiteti;
using FluentNHibernate.Mapping;

namespace PolicijskaUprava.Mapiranja
{
    public class ObjekatMapiranje : ClassMap<Objekat>
    {
        public ObjekatMapiranje()
        {
            Id(x => x.IdObjekta).Column("ID_OBJEKTA").GeneratedBy.Assigned();
            Table("OBJEKAT");

            Map(x => x.Tip).Column("TIP");
            Map(x => x.Povrsina).Column("POVRSINA");
            Map(x => x.Ime).Column("IME");
            Map(x => x.Prezime).Column("PREZIME");
            Map(x => x.Adresa).Column("ADRESA");
            Map(x => x.Telefon).Column("TELEFON");

            References(x => x.InstaliranJeAlarmniSistem).Column("SERIJSKI_BROJ_ALARMA").LazyLoad();
            References(x => x.ObezbedjujeStanica).Column("ID_STANICE").LazyLoad();
        }
    }
}

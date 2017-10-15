using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicijskaUprava.Entiteti;
using FluentNHibernate.Mapping;

namespace PolicijskaUprava.Mapiranja
{
    public class TelefoniMapiranje : ClassMap<Telefoni>
    {
        public TelefoniMapiranje()
        {
            Table("TELEFON");
            Id(x => x.IdObjekta, "ID_OBJEKTA").GeneratedBy.TriggerIdentity();
            Map(x => x.Telefon, "TELEFON");
            References(x => x.ObjekatTelefon, "ID_OBJEKTA").LazyLoad();
        }
    }
}

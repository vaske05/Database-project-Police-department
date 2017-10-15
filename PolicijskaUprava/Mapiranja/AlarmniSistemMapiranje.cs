using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicijskaUprava.Entiteti;
using FluentNHibernate.Mapping;

/// </summary>
namespace PolicijskaUprava.Mapiranja
{
    public class AlarmniSistemMapiranje : ClassMap<AlarmniSistem>
    {
        public AlarmniSistemMapiranje()
        {
            

            Table("ALARMNI_SISTEM");
            Id(x => x.SerijskiBroj).Column("SERIJSKI_BROJ").GeneratedBy.Assigned();

            Map(x => x.Proizvodjac).Column("PROIZVODJAC");
            Map(x => x.DatumInstalacije).Column("DATUM_INSTALACIJE");
            Map(x => x.DatumAtesta).Column("DATUM_ATESTA");
            Map(x => x.Opis).Column("OPIS");
            Map(x => x.PocetniDatum).Column("POCETNI_DATUM");
            Map(x => x.KrajnjiDatum).Column("KRAJNJI_DATUM");
            //Map(x => x.SerijskiBroj).Column("SERIJSKI_BROJ");
            Map(x => x.Model).Column("MODEL");
            Map(x => x.GodinaProizvodnje).Column("GODINA_PROIZVODNJE");

            HasMany(x => x.InstaliranJeObjekat).KeyColumn("SERIJSKI_BROJ_ALARMA").LazyLoad().Inverse();

        }


    }
    public class DetekcijaToplotnogOdrazaMapiranje : SubclassMap<DetekcijaToplotnogOdraza>
    {
        public DetekcijaToplotnogOdrazaMapiranje()
        {
            Table("DETEKCIJA_TOPLOTNOG_ODRAZA");

            KeyColumn("SERIJSKI_BROJ");
            Map(x => x.VertikalnaRezolucija).Column("VERTIKALNA_REZOLUCIJA");
            Map(x => x.HorizontalnaRezolucija).Column("HORIZONTALNA_REZOLUCIJA");

            //HasMany(x => x.InstaliranJeObjekat).KeyColumn("SERIJSKI_BROJ_ALARMA").LazyLoad().Cascade.All().Inverse();

        }


    }

    public class DetekcijaPokretaMapiranje : SubclassMap<DetekcijaPokreta>
    {
        public DetekcijaPokretaMapiranje()
        {
            Table("DETEKCIJA_POKRETA");

            KeyColumn("SERIJSKI_BROJP");
            Map(x => x.Osetljivost).Column("OSETLJIVOST");

            //HasMany(x => x.InstaliranJeObjekat).KeyColumn("SERIJSKI_BROJ_ALARMA").LazyLoad().Cascade.All().Inverse();

        }

    }

   public class UltrazvucniMapiranje : SubclassMap<Ultrazvucni>
    {
        public UltrazvucniMapiranje()
        {
            Table("ULTRAZVUCNI");

            KeyColumn("SERIJSKI_BROJU");


            Map(x => x.OpsegFrekvencija).Column("OPSEG_FREKVENCIJA");

            //HasMany(x => x.InstaliranJeObjekat).KeyColumn("SERIJSKI_BROJ_ALARMA").LazyLoad().Inverse();

        }

    }
}
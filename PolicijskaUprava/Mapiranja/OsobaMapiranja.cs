using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicijskaUprava.Entiteti;
using FluentNHibernate.Mapping;

namespace PolicijskaUprava.Mapiranja
{
    public class OsobaMapiranja : ClassMap<Osoba>
    {
        public OsobaMapiranja()
        {
            
            Table("OSOBA");
            Id(x => x.Jmbg).Column("JMBG").GeneratedBy.Assigned();

            Map(x => x.Adresa, "ADRESA");
            //Map(x => x.Napredovanje, "NAPREDOVANJE");
            Map(x => x.Cin, "CIN");
            Map(x => x.DatumSticanja, "DATUM_STICANJA");
            Map(x => x.DatumPrijema, "DATUM_PRIJEMA");
            Map(x => x.NazivStanice, "NAZIV_STANICE");
            Map(x => x.Pol, "POL");
            Map(x => x.Ime, "IME");
            Map(x => x.Prezime, "PREZIME");
            Map(x => x.ImeRoditelja, "IME_RODITELJA");
            //Map(x => x.Obrazovanje, "OBRAZOVANJE");
            Map(x => x.NazivSkole, "NAZIV_SKOLE");
            Map(x => x.DatumZavrsetka, "DATUM_ZAVRSETKA");
            
            References(x => x.ZaposljenUUprava, "ID_UPRAVE_OSOBA").LazyLoad();
        }
    }
    
    public class NacelnikMapiranja : SubclassMap<Nacelnik>
    {
        public NacelnikMapiranja()
        {
            Table("NACELNIK");

            //Id(x => x.Jmbg).Column("JMBG").GeneratedBy.TriggerIdentity();
            KeyColumn("JMBG");

           

            //References(x => x.ZaposljenUUprava, "ID_UPRAVE_OSOBA").LazyLoad(); Sigurno ne treba
            References(x => x.RukovodiUprava, "ID_UPRAVE_NACELNIK").LazyLoad(); //Dodato 28.06
            HasMany(x => x.ZamenjujeNacelnikaZamenik).KeyColumn("JMBG_NACELNIKA").LazyLoad().Cascade.All().Inverse();
        }
    }

    public class ZamenikNacelnikaMapiranja : SubclassMap<ZamenikNacelnika>
    {
        public ZamenikNacelnikaMapiranja()
        {
            Table("ZAMENIK_NACELNIKA");

            //Id(x => x.Jmbg).Column("JMBG").GeneratedBy.TriggerIdentity();
            KeyColumn("JMBGZN");

            

            //References(x => x.ZaposljenUUprava, "ID_UPRAVE_OSOBA").LazyLoad();
            //References(x => x.RadiUSektor, "ID_SEKTORAZ").LazyLoad();
            References(x => x.ZamenjujeNacelnikaNacelnik, "JMBG_NACELNIKA").LazyLoad();
        }
    }

    public class VanredniPolicajacMapiranja : SubclassMap<VanredniPolicajac>
    {
        public VanredniPolicajacMapiranja()
        {
            Table("VANREDNI_POLICAJCI");

            
            KeyColumn("JMBGV");

            Map(x => x.NazivVestine, "NAZIV_VESTINE");
            Map(x => x.DatumZavrsetkaKursa, "DATUM_ZAVRSETKA_KURSA");
            Map(x => x.DatumSticanjaSertifikata, "DATUM_STICANJA_SERTIFIKATA");

            //References(x => x.ZaposljenUUprava, "ID_UPRAVE_OSOBA").LazyLoad();
            References(x => x.AngazovanUVandredneSituacije, "ID_SEKTORAV").LazyLoad();
        }
    }

    public class PozornikMapiranja : SubclassMap<Pozornik>
    {
        public PozornikMapiranja()
        {
            Table("POZORNIK");

            KeyColumn("JMBGP");

            //References(x => x.ZaposljenUUprava, "ID_UPRAVE_OSOBA").LazyLoad();
            References(x => x.RasporedjenUSaobracaj, "ID_SEKTORAS").LazyLoad();
        }
    }

    public class SkolskiPolicajacMapiranja : SubclassMap<SkolskiPolicajac>
    {
        public SkolskiPolicajacMapiranja()
        {
            Table("SKOLSKI_POLICAJCI");

            //Id(x => x.Jmbg).Column("JMBG").GeneratedBy.TriggerIdentity();
            KeyColumn("JMBGS");

            

            //References(x => x.ZaposljenUUprava, "ID_UPRAVE").LazyLoad();
            //References(x => x.ZaduzenUSkola, "JMBGS").LazyLoad();  //Ne treba valjda izmenjeno 28.06

            References(x => x.ZaduzenUSkola, "ID_SKOLE").LazyLoad(); //Dodato 28.06
        }
    }

    public class OstaliPolicajciMapiranje : SubclassMap<OstaliPolicajci>
    {
        public OstaliPolicajciMapiranje()
        {
            Table("OSTALI_POLICAJCI");

            //Id(x => x.Jmbg).Column("JMBG").GeneratedBy.TriggerIdentity();
            KeyColumn("JMBGO");

            

            //References(x => x.ZaposljenUUprava, "ID_UPRAVE_OSOBA").LazyLoad();
            References(x => x.Patrolira, "ID_PATROLE").LazyLoad();
        }
    }
    public class VodjaMapiranje : SubclassMap<Vodja>
    {
        public VodjaMapiranje()
        {
            Table("VODJA");
            //Id(x => x.Jmbg).Column("JMBG").GeneratedBy.TriggerIdentity();
            KeyColumn("JMBGVODJA");

            

            //References(x => x.ZaposljenUUprava, "ID_UPRAVE_OSOBA").LazyLoad();
            References(x => x.VodiPatrolu, "ID_PATROLEV").LazyLoad();
        }
    }

    public class ZamenikSefaMapiranje : SubclassMap<ZamenikSefa>
    {
        public ZamenikSefaMapiranje()
        {
            Table("ZAMENIK_SEFA");

            //Id(x => x.Jmbg).Column("JMBG").GeneratedBy.TriggerIdentity();
            KeyColumn("JMBGZSEF");

            

            //References(x => x.ZaposljenUUprava, "ID_UPRAVE_OSOBA").LazyLoad();
            //References(x => x.ZamenjujeSefa, "JMBG_ZAMENIKA").LazyLoad();

        }
    }

    public class SefMapiranje : SubclassMap<Sef>
    {
        public SefMapiranje()
        {
            Table("SEF");

            //Id(x => x.Jmbg).Column("JMBG").GeneratedBy.TriggerIdentity();
            KeyColumn("JMBGSEF");

            //References(x => x.ZaposljenUUprava, "ID_UPRAVE_OSOBA").LazyLoad();

            //References(x => x.Sefuje, "JMBG_SEFA").LazyLoad();

            References(x => x.ZamenjujeZamenika, "JMBG_ZAMENIKA").LazyLoad();

        }
    }
}
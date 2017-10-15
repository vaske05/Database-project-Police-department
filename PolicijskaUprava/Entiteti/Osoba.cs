using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaUprava.Entiteti
{
    public class Osoba
    {
        public virtual int Jmbg { set; get; }
        public virtual string Adresa { set; get; }
        //public virtual string Napredovanje { set; get; }
        public virtual string Cin { set; get; }
        public virtual DateTime DatumSticanja { set; get; }
        public virtual DateTime DatumPrijema { set; get; }
        public virtual string NazivStanice { set; get; }
        public virtual string Pol { set; get; }
        public virtual string Ime { set; get; }
        public virtual string Prezime { set; get; }
        public virtual string ImeRoditelja { set; get; }
        //public virtual string Obrazovanje { set; get; }
        public virtual string NazivSkole { set; get; }
        public virtual DateTime DatumZavrsetka { set; get; }


        public virtual Uprava ZaposljenUUprava { set; get; }
    }

        public class Nacelnik : Osoba
        {
            public virtual IList<ZamenikNacelnika> ZamenjujeNacelnikaZamenik { get; set; }
            public virtual Uprava RukovodiUprava { get; set; }

            public Nacelnik()
            {
                ZamenjujeNacelnikaZamenik = new List<ZamenikNacelnika>();
            }
        }

        public class ZamenikNacelnika : Osoba
        {
            public virtual Nacelnik ZamenjujeNacelnikaNacelnik { set; get; }
            public virtual Sektor RadiUSektor { set; get; }

        }

        public class VanredniPolicajac : Osoba
        {
            public virtual string NazivVestine { set; get; }
            public virtual string Kurs { set; get; }
            public virtual DateTime DatumZavrsetkaKursa { set; get; }
            public virtual DateTime DatumSticanjaSertifikata { set; get; }

            public virtual VanredneSituacije AngazovanUVandredneSituacije { set; get; }
        }

        public class Pozornik : Osoba
        {
            public virtual Saobracaj RasporedjenUSaobracaj { set; get; }
        }

        public class SkolskiPolicajac : Osoba
        {
            public virtual Skola ZaduzenUSkola { set; get; }
        }

        public class OstaliPolicajci : Osoba
        {
            public virtual Patrola Patrolira { set; get; }
        }

        public class Vodja : OstaliPolicajci
        {
            public virtual Patrola VodiPatrolu { set; get; }
        }

        public class ZamenikSefa : Osoba
        {
            public virtual Sef ZamenjujeSefa { set; get; }
        }

        public class Sef : Osoba
        {
            public virtual Stanica Sefuje { set; get; }
            public virtual ZamenikSefa ZamenjujeZamenika { set; get; }
        }
    
}



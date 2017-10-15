using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaUprava.Entiteti
{
    public class Telefoni
    {
        
       public virtual string Telefon { get; set; }
        public virtual int IdObjekta { get; set; }
       public virtual Objekat ObjekatTelefon { get; set; }
    }
        }
    

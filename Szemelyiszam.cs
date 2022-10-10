using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vasmegye1
{
    class Szemelyiszam
    {

        readonly string szam;

        public string Szam => szam;

        public Szemelyiszam(string szam)
        {

            this.szam = szam;

        }

        public int evSzam()
        {

            int ev = int.Parse(Szam.Substring(2, 2));
            ev = Szam[0] == '1' || Szam[0] == '2' ? 1900 + ev : 2000 + ev;

            return ev;
             
        } 

    }
}

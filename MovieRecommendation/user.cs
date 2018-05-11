using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendation
{
    class user
    {
        int userid = 0;
        List<Rated> moviewatched = new List<Rated>();

        public int Userid { get => userid; set => userid = value; }
        internal List<Rated> Moviewatched { get => moviewatched; set => moviewatched = value; }
    }
}

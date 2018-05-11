using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendation
{
    class Rated
    {
        int movieid = 0;
        float rate = 0;

        public int Movieid { get => movieid; set => movieid = value; }
        public float Rate { get => rate; set => rate = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendation
{
    class userpm
    {
        int userid = 0;
        float rate = 0.0f;

        public int Userid { get => userid; set => userid = value; }
        public float Rate { get => rate; set => rate = value; }
    }
}

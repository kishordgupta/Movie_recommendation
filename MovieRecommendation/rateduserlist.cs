using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendation
{
    class rateduserlist
    {
        int movieid = 0;
        List<userpm> userid;

        public int Movieid { get => movieid; set => movieid = value; }
        public List<userpm> Userid { get => userid; set => userid = value; }

   

    }
}

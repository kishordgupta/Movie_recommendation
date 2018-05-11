using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieRecommendation
{
    public partial class Form1 : Form
    {
        List<movilist> listMovies = new List<movilist>();
        List<movilist> listratedMovies = new List<movilist>();
        List<user> listusers = new List<user>();
        List<movilist> notwatched = new List<movilist>();
        List<movilist> movieintahtyear = new List<movilist>();
        List<Rated> watched = new List<Rated>();
        List<rateduserlist> ratedmovielist = new List<rateduserlist>();
        user seleteduser = new user();
        public Form1()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            using (var reader = new StreamReader(configs.nonsorted))
            {

                rateduserlist m = new rateduserlist();
                int Movieidte = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                

                    int Movieid = Int32.Parse(values[0]);
                    if (Movieidte != Movieid)
                    {
                        ratedmovielist.Add(m);
                         m = new rateduserlist();
                        m.Movieid = Int32.Parse(values[0]);
                        m.Userid = new List<userpm>();
                        Movieidte = Movieid;
                    }
                    else
                    {
                        userpm r = new userpm();
                        r.Userid = Int32.Parse(values[1]);
                        r.Rate = float.Parse(values[2]);
                        m.Userid.Add(r);
                    }

                }
                ratedmovielist.RemoveAt(0);
            }
       
            int pi = 0;
            using (var reader = new StreamReader(configs.movielist))
            {
                pi++;


                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    movilist m = new movilist();
                    m.Id = Int32.Parse(values[0]);
                    if (ratedmovielist.Where(r => r.Movieid == m.Id).Count() != 0)
                    {
                        try
                        {
                            m.Year = Int32.Parse(values[1]);
                        }
                        catch (Exception ex)
                        { m.Year = 0; }
                        m.Title = (values[2]);
                        listMovies.Add(m);
                    }
                   
                }
            }
           
            listBox1.Items.Clear();

            foreach (movilist m in listMovies)
                listBox1.Items.Add(m.Title + " , " + m.Year);

            listBox1.Invalidate();

            using (var reader = new StreamReader(configs.userdb))
            {
                int userid = 0;
                List<string> sl = new List<string>();

                user m = new user();
                pi = 0;

                while (!reader.EndOfStream)
                {
                    pi++;
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    int useridte = Int32.Parse(values[1]);
                    if (useridte != userid)
                    {
                        listusers.Add(m);
                        m = new user(); m.Userid = Int32.Parse(values[1]); m.Moviewatched = new List<Rated>();
                        userid = useridte;
                    }
                    else
                    {
                        Rated r = new Rated();
                        r.Movieid = Int32.Parse(values[0]);
                        r.Rate = float.Parse(values[2]);
                        m.Moviewatched.Add(r);
                    }
                   
                }
                listusers.RemoveAt(0);
            }

         
            listBox2.Items.Clear();
           
            foreach (user m in listusers)
                listBox2.Items.Add(m.Userid);
           
        }

   


        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            user m = listusers[listBox2.SelectedIndex];//.in.Select(listBox2.SelectedIndex);
            seleteduser = m;
            label1.Text = "userid: " + m.Userid + " selected";
            watched.Clear();
            watched = new List<Rated>(m.Moviewatched);
           
            foreach (Rated r in m.Moviewatched)
            {
                movilist mi = listMovies.Where(mm => mm.Id == r.Movieid).FirstOrDefault();
                listBox3.Items.Add(mi.Title + " " + r.Rate);

            }
            listBox3.Invalidate();
            notwatched.Clear();
            listBox4.Items.Clear();
            foreach (movilist ml in listMovies)
            {
                if (watched.Where(r => r.Movieid == ml.Id).Count() == 0)
                {
                    notwatched.Add(ml);
                    listBox4.Items.Add(ml.Id + " " + ml.Title + " , " + ml.Year);
                }


            }
            }

        float predrating(int movieid, user userid)
        {
            int k = 1;
            float predracting = 0.0f;
            float sumpredracting = 0.0f;
            rateduserlist userwhowatchthismovie = ratedmovielist.Where(r => r.Movieid == movieid).First();
          
            int co = 0;
            int simi = 0;
            foreach (userpm id in userwhowatchthismovie.Userid)
            {
                user userothermovies;
                if (listusers.Where(r => r.Userid == id.Userid).Count() != 0)
                {
                    userothermovies = listusers.Where(r => r.Userid == id.Userid).FirstOrDefault();


                    foreach (Rated r in userothermovies.Moviewatched)
                    {

                        if (userid.Moviewatched.Where(ka => ka.Movieid == r.Movieid).Count() != 0)
                        {
                            Rated rk = userid.Moviewatched.Where(ka => ka.Movieid == r.Movieid).First();
                            simi++;
                            sumpredracting = sumpredracting + (r.Rate + rk.Rate) / 2;
                            co++;
                            if (k == simi)
                                break;
                        }
                    }
                }

                   
                
            }
     
            predracting = sumpredracting / co;
            return predracting;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Length <= 2)
                comboBox1.SelectedText = ""+2000;
            int year = Int32.Parse(comboBox1.SelectedItem.ToString());
            listBox5.Items.Clear(); movieintahtyear.Clear();
             movieintahtyear = new List<movilist>(listMovies.Where(mm => mm.Year == year).ToList());
            listBox5.Items.Add("total " + movieintahtyear.Count);
            foreach (movilist m in movieintahtyear)
              listBox5.Items.Add(m.Title +" "+ predrating(m.Id, seleteduser));

            
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            movilist m = notwatched[listBox4.SelectedIndex];
          
               label6.Text = seleteduser.Userid+ " Predicintg score for " + m.Title +" " + predrating(m.Id,seleteduser);
        }
    }
    }




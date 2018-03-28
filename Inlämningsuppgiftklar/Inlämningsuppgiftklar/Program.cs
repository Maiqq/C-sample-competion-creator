using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Inlämningsuppgift
{
    class Program
    {
        static void Main(string[] args)
        {

            //tävlings information
            string compname;
            DateTime compdate = new DateTime(DateTime.Today.Year,DateTime.Today.Month, DateTime.Today.Day);




            Console.WriteLine("Would you like to create a new competion? \n Please enter the name of the competion:");
            compname = Console.ReadLine();
            Console.WriteLine("Please enter the date of the competion:");
            bool done = false;
            while(!done)
            {
            try
            {
                compdate = DateTime.Parse(Console.ReadLine());
                done = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("The competion date must be written as day.month.year.. Please try again:)");
            }}
            Competion comp1 = new Competion(compname, compdate);



            //registrerar tävlanden

            Console.WriteLine("Registration for " + comp1.Cname + " on " + comp1.Cdate.ToShortDateString() + ". \n Please enter your name:");

            string name;
            double pr;
            int nr = 1;
            name = Console.ReadLine();


            while (name != "")
            {
                Console.WriteLine("Please enter your personal best:");
                pr = double.Parse(Console.ReadLine());
                comp1.StartList.Add(new Registration(name, pr, nr));
                nr++;
                Console.WriteLine("Registration for " + comp1.Cname + " on " + comp1.Cdate.ToShortDateString() + " (To start competion please press enter). \n Please enter your name:");
                name = Console.ReadLine();

            }
            //tävling börjar
            Console.WriteLine(comp1.Cname + " is about to start!\n Following competitors:");
            foreach (Registration t in comp1.StartList)
            {
                Console.WriteLine("Startnumber: " + t.Nr + ", Name " + t.Name + ", Personal record: " + t.Pr);
            }

            Console.WriteLine("Please enter results: (if you like to see standings during the competion please enter a negative number in results)");
            int round = 1;
            double results;
            double max = 0;

            while (round < 4)
            {



                foreach (Registration t in comp1.StartList)
                {



                    Console.WriteLine("Startnumber: " + t.Nr + " Name " + t.Name + ", Personal record: " + t.Pr + "\n");
                    Console.WriteLine("Round: " + round + " Results: ");
                    results = double.Parse(Console.ReadLine());
                    while (results < 0)
                    {
                        comp1.StartList.ForEach(a => Console.WriteLine("Results: " + a));
                        Console.WriteLine("Startnumber: " + t.Nr + " Name " + t.Name + ", Personal record: " + t.Pr + "\n");
                        Console.WriteLine("Round: " + round + " Results: ");
                        results = double.Parse(Console.ReadLine());

                    }

                    if (results > t.max)
                    {
                        t.max = results;
                    }
                    if (results > max)
                    {
                        max = results;
                        Console.WriteLine(t.Name + " is now in the lead!!");
                    }
                    if (results > t.Pr)
                    {
                        t.Pr = results;
                        Console.WriteLine("New Personal record by " + t.Name + "!");
                    }

                    t.Resultlist.Add(new Results(round, results, t.Name));

                }


                round++;



            }

            //streamwriter
            StreamWriter sw1 = new StreamWriter("C:\\Users\\Mikael\\Desktop\\Skola\\Stick kopia\\test\\inlamtest", false);
            sw1.WriteLine(comp1.Cname + ";" + comp1.Cdate.ToShortDateString());
            //slutgiltig resultatlista
            int place = 1;
            comp1.StartList.Sort();
            Console.WriteLine(comp1.Cname + comp1.Cdate + " is now over. Following results have been registred: \n");
            comp1.StartList.ForEach(a =>
            {
                sw1.WriteLine(place.ToString("##0.00") + ";" + a.Name + ";" + a.max.ToString("##0.00"));
                Console.WriteLine(place + " place: " + a);
                place++;

            });


            sw1.Close();
            Console.ReadLine();






        }


    }
    class Competion
    {
        public Competion(string cname, DateTime date)
        {
            Cname = cname;
            Cdate = date;
            StartList = new List<Registration>();



        }


        public DateTime Cdate { get; set; }
        public string Cname { get; set; }

        public List<Registration> StartList { get; set; }



    }
    class Registration : IComparable<Registration>
    {

        public Registration(string name, double pr, int nr)
        {
            Name = name;
            Pr = pr;
            Nr = nr;
            Resultlist = new List<Results>();
            max = 0;

        }

        public double max;
        public string Name { get; set; }
        public double Pr { get; set; }
        public int Nr { get; set; }
        public List<Results> Resultlist { get; set; }


        public override string ToString()
        {
            string output = "Startnumber: " + Nr + ", Name " + Name + ", Personal record: " + Pr;
            foreach (Results r in Resultlist)
            {
                output += "\n\t" + "Round: " + r.Round + " Results: " + r.Result;
            }
            return output;
        }


        public int CompareTo(Registration other)
        {
            return other.max.CompareTo(this.max);
        }
    }
    class Results : IComparable<Results>
    {



        public Results(int round, double result, string name)
        {
            Round = round;
            Result = result;
            Name = name;

        }

        public int Round { get; set; }
        public double Result { get; set; }

        public string Name { get; set; }
        public override string ToString()
        {
            string output = "Round: " + Round + ", Results: " + Result;

            return output;
        }

        public int CompareTo(Results other)
        {
            return other.Result.CompareTo(this.Result);
        }
    }

}

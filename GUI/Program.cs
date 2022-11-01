using BL;
using ENTITY;
using System;
using System.Linq;

namespace GUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //BusinessLogic bl = new BusinessLogic();

            //Console.WriteLine(bl.GetTracks(new Track()).Count);
            var data  = Convert.ToDateTime("11/03/1983").ToString("yyyy-MM-dd");
          
            Console.WriteLine(data);

            Console.ReadLine();
        }
    }
}

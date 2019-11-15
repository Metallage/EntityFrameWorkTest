using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFrameWorkTest.DBContext;
using EntityFrameWorkTest.Entities;

namespace EntityFrameWorkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbName = "test.sqlite";
            if(!DBCreator.DBExists(dbName))
                DBCreator.CreateDb(dbName);

            using (MyDB mydb = new MyDB())
            {
                //mydb.Users.Add(new User() { Fio = "Ололошечка", Telephone = "+668-777" });
                //mydb.SaveChanges();
                Console.WriteLine("{0}", mydb.Users.Where(x => x.Id == 1).ToList().Select(x => new {Fio=x.Fio, Tel=x.Telephone}).Single().Tel); //It WORKS!!
                Console.ReadLine();

            }
        }
    }
}

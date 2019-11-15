using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using EntityFrameWorkTest.DBContext;
using EntityFrameWorkTest.Entities;

namespace EntityFrameWorkTest
{
    class Program
    {

        

        static void GenerateSomeData()
        {
            using (MyDB mydb = new MyDB())
            {

                mydb.Users.Add(new User() { Fio = "Ололошечка", Telephone = "111-111" });
                mydb.Users.Add(new User() { Fio = "Бонд Джеймс Бонд", Telephone = "007-007" });
                mydb.Users.Add(new User() { Fio = "Матёрый", Telephone = "999-999" });
                mydb.Users.Add(new User() { Fio = "Няшечка", Telephone = "777-777" });

                mydb.Computers.Add(new Computer() { NsName = "Slogno-01", Ip = "192.168.0.2", UserId = 2 });
                mydb.Computers.Add(new Computer() { NsName = "Server-99", Ip = "192.168.0.1", UserId = 3 });
                mydb.Computers.Add(new Computer() { NsName = "Rozovenki-02", Ip = "192.168.0.5", UserId = 4 });
                mydb.Computers.Add(new Computer() { NsName = "Tetris-01", Ip = "192.168.0.99", UserId = 1 });

                mydb.Softwares.Add(new Software() { Name = "Rabotaet", Version = "10.2" });
                mydb.Softwares.Add(new Software() { Name = "Gluchit", Version = "0.004-beta" });
                mydb.Softwares.Add(new Software() { Name = "Neponatnoe", Version = "1" });

                mydb.Installs.Add(new Install() { CompId = 1, SoftId = 1, Version = "10.2" });
                mydb.Installs.Add(new Install() { CompId = 2, SoftId = 1, Version = "10.0" });
                mydb.Installs.Add(new Install() { CompId = 3, SoftId = 1, Version = "10.2" });
                mydb.Installs.Add(new Install() { CompId = 4, SoftId = 2, Version = "pre-alpha0.0001" });
                mydb.Installs.Add(new Install() { CompId = 2, SoftId = 2, Version = "0.004-beta" });
                mydb.Installs.Add(new Install() { CompId = 3, SoftId = 3, Version = "1" });

                mydb.SaveChanges();
            }
        }


        static void PrintSummary(MyDB myDb)
        {
            Console.WriteLine("Всего компьютеров - {0}", myDb.Computers.Count());
            Console.WriteLine("Всего пользователей - {0}", myDb.Users.Count());
            Console.WriteLine("Всего ПО - {0}", myDb.Softwares.Count());
            Console.WriteLine("Всего установок - {0}", myDb.Installs.Count());
        }

        static void PrintUpdate(MyDB mydb)
        {
            var result = from install in mydb.Installs
                         join comp in mydb.Computers on install.CompId equals comp.Id
                         join user in mydb.Users on comp.UserId equals user.Id
                         join soft in mydb.Softwares on install.SoftId equals soft.Id
                         where soft.Version!=install.Version
                         select new { ComputerName=comp.NsName,  UserFio = user.Fio, OldVersion=install.Version, NewVersion=soft.Version, SoftName = soft.Name };
            foreach (var rs in result)
                Console.WriteLine("На компьютере {0} пользователя {1} не совпадает версии ПО {2}.\n Должно быть {3}, установлено {4}",rs.ComputerName, 
                    rs.UserFio, rs.SoftName, rs.NewVersion, rs.OldVersion);
        }

        static void Main(string[] args)
        {
            string dbName = "test.sqlite";
            if (!DBCreator.DBExists(dbName))
            {
                DBCreator.CreateDb(dbName);
                GenerateSomeData();
            }

            using (MyDB mydb = new MyDB())
            {
                PrintSummary(mydb);
                PrintUpdate(mydb);

                //mydb.Users.Add(new User() { Fio = "Ололошечка", Telephone = "+668-777" });
                //mydb.SaveChanges();
                // Console.WriteLine("{0}", mydb.Users.Where(x => x.Id == 1).ToList().Select(x => new {Fio=x.Fio, Tel=x.Telephone}).Single().Tel); //It WORKS!!
                Console.ReadLine();

            }
        }
    }
}

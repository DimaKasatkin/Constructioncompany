using ConstructionСompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.Data
{
    public class DbInitializer
    {
        private static Random randObj = new Random(1);
        public static void Initialize(ConstructionCompanyContext db)
        {
            db.Database.EnsureCreated();

            int brigadeCount = 50;
            int customerCount = 100;
            int employeeCount = 300;
            int materialsCount = 40;
            int orderCount = 600;
            int typeOfJobCount = 15;

            CustomerGenerate(db, customerCount);
            PositionGenerate(db);
            BrigadeGenerate(db, brigadeCount);
            TypeOfJobGenerate(db, typeOfJobCount);
            OrderGenerate(db, orderCount);
            EmployeeGenerate(db, employeeCount);
            MaterialGenerate(db, materialsCount);
        }

        public static void OrderGenerate(ConstructionCompanyContext db, int orderCount)
        {
            if (db.Orders.Any())
            {
                return;
            }
            int countCustomers = db.Customers.Count();
            int countTypeOfJobs = db.TypeOfJobs.Count();
            int countBrigades = db.Brigades.Count();

            string[] namesVoc = { "Cofe", "Hostel", "Restaurant", "Cinema", "Shop" };
            bool[] markVoc = { true, false };

            for(var i = 0; i < orderCount; i++)
            {
                var cost = randObj.Next(30, 600);
                var startDate = DateTime.Now.AddDays(-randObj.Next(5000, 10000));
                var endDate = startDate.AddDays(randObj.Next(1000, 5000));
                var completionMark = markVoc[randObj.Next(0, 2)];
                var paymentMark = markVoc[randObj.Next(0, 2)];
                var customerId = randObj.Next(1, countCustomers + 1);
                var typeOfJobId = randObj.Next(1, countTypeOfJobs + 1);
                var brigadesId = randObj.Next(1, countBrigades + 1);

                db.Orders.Add(new Order()
                {
                    Cost = cost,
                    StartDate = startDate,
                    EndDate = endDate,
                    CompletionMark = completionMark,
                    PaymentMark = paymentMark,
                    CustomerId = customerId,
                    TypeOfJobId = typeOfJobId,
                    BrigadeId = brigadesId
                });
            }
            db.SaveChanges();
        }

        public static void TypeOfJobGenerate(ConstructionCompanyContext db, int typeOfJobCount)
        {
            if (db.TypeOfJobs.Any())
            {
                return;
            }

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string[] nameVoc = { "Excavation", "Asphalt production", "Technological design", "Technological design",
                                 "Architectural design", "Quality control of work"};

            for(int i = 0; i < typeOfJobCount; i++)
            {
                var name = nameVoc[randObj.Next(nameVoc.GetLength(0))] + randObj.Next(typeOfJobCount);
                var description = new string(Enumerable.Repeat(chars, 10)
                                .Select(s => s[randObj.Next(s.Length)]).ToArray());
                var cost = randObj.Next(70, 350);

                db.TypeOfJobs.Add(new TypeOfJob()
                {
                    Name = name,
                    Description = description,
                    Cost = cost
                });
            }
            db.SaveChanges();
        }

        public static void MaterialGenerate(ConstructionCompanyContext db, int materialCount)
        {
            if (db.Materials.Any())
            {
                return;
            }

            int countTypeOfJob = db.TypeOfJobs.Count();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string[] packagingVoc = {"Stretch paper", "Thermoresistant paper", "Corrugated packaging", "Strapping tape",
                                     "Air bubble wrap" };
            for(var i = 0; i < materialCount; i++)
            {
                var name = new string(Enumerable.Repeat(chars, 7)
                                .Select(s => s[randObj.Next(s.Length)]).ToArray());
                var packaging = packagingVoc[randObj.Next(packagingVoc.GetLength(0))] + randObj.Next(materialCount);
                var description = new string(Enumerable.Repeat(chars, 20)
                                .Select(s => s[randObj.Next(s.Length)]).ToArray());
                var cost = randObj.Next(10, 73);
                var typeOfJobId = randObj.Next(1, countTypeOfJob + 1);

                db.Materials.Add(new Materials()
                {
                    Name = name,
                    Packaging = packaging,
                    Description = description,
                    Cost = cost,
                    TypeOfJobId = typeOfJobId
                });
            }
            db.SaveChanges();
        }

        public static void EmployeeGenerate(ConstructionCompanyContext db, int employeeCount)
        {
            if (db.Employees.Any())
            {
                return;
            }

            int positionCount = db.Positions.Count();
            int brigadesCount = db.Brigades.Count();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string[] fullNamesVoc = { "Yunetova K.A", "Gramovich A.A", "Semenov D.S.", "Trofimov E.W", "Gerasimenko D.A",
                                  "Kolos W.D", "Bartnovskaya A.A", "Dashkevich D.A", "Karkozov V.V." };

            string[] addressVoc = {"Mozyr, per.Zaslonova, ", "Gomel, st.Gastelo, ", "Minsk, st.Poleskay, ", "Grodno, pr.Rechetski, ", "Vitebsk, st, International, ",
                                    "Brest, pr.October, ", "Minsk, st.Basseinaya, ", "Mozyr, boulevard Youth, " };

            string[] sexsVoc = { "Man", "Woman" };

            for (int i = 0; i < employeeCount; i++)
            {
                var fullName = fullNamesVoc[randObj.Next(fullNamesVoc.GetLength(0))] + randObj.Next(employeeCount);
                var age = randObj.Next(20, 60);
                var sexEmployee = sexsVoc[randObj.Next(0, 2)];
                var address = addressVoc[randObj.Next(addressVoc.GetLength(0))] + randObj.Next(employeeCount);
                var phoneNumber = "+375 (29) " + randObj.Next(100, 999) + "-" + randObj.Next(10, 99) + "-" + randObj.Next(10, 99);
                var passportData = new string(Enumerable.Repeat(chars, 2).Select(s => s[randObj.Next(s.Length)]).ToArray()).ToUpper() + randObj.Next(100000, 999999);
                var positionId = randObj.Next(1, positionCount + 1);
                var brigadeId = randObj.Next(1, brigadesCount + 1);

                db.Employees.Add(new Employee()
                {
                    FullName = fullName,
                    Age = age,
                    Sex = sexEmployee,
                    Address = address,
                    Phone = phoneNumber,
                    PassportData = passportData,
                    PositionId = positionId,
                    BrigadeId = brigadeId
                });
            }
            db.SaveChanges();
        }

        public static void PositionGenerate(ConstructionCompanyContext db)
        {
            if (db.Positions.Any())
            {
                return;
            }
            db.Positions.AddRange(new Position[]
            {
                new Position()
                {
                    Name = "Engineer",
                    Salary = 700,
                    Duties = "Technical education",
                    Requirements = "Project design"
                },
                new Position()
                {
                    Name = "Constructor",
                    Salary = 850,
                    Duties = "Technical education",
                    Requirements = "Making calculations"
                },
                new Position()
                {
                    Name = "Accountant",
                    Salary = 500,
                    Duties = "Completed higher education",
                    Requirements = "Finance management"
                },
                new Position()
                {
                    Name = "Builder",
                    Salary = 350,
                    Duties = "Secondary special education",
                    Requirements = "Performing various works"
                },
                new Position()
                {
                    Name = "HR",
                    Salary = 1100,
                    Duties = "Higher education, communicativeness",
                    Requirements = "Hiring"
                }
            });
            db.SaveChanges();
        }

        public static void CustomerGenerate(ConstructionCompanyContext db, int customerCount)
        {
            try
            {
                if (db.Customers.Any())
                {
                    return;
                }
            }
            catch { 
            
            }
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string[] fullNamesVoc = { "Yunetova K.A", "Gramovich A.A", "Semenov D.S.", "Trofimov E.W", "Gerasimenko D.A",
                                  "Kolos W.D", "Bartnovskaya A.A", "Dashkevich D.A", "Karkozov V.V." };

            string[] addressVoc = {"Mozyr, per.Zaslonova, ", "Gomel, st.Gastelo, ", "Minsk, st.Poleskay, ", "Grodno, pr.Rechetski, ", "Vitebsk, st, International, ",
                                    "Brest, pr.October, ", "Minsk, st.Basseinaya, ", "Mozyr, boulevard Youth, " };
            for(var i = 0; i < customerCount; i++)
            {
                var fullName = fullNamesVoc[randObj.Next(fullNamesVoc.GetLength(0))] + randObj.Next(customerCount);
                var address = addressVoc[randObj.Next(addressVoc.GetLength(0))] + randObj.Next(customerCount);
                var phoneNumber = "+375 (29) " + randObj.Next(100, 999) + "-" + randObj.Next(10, 99) +
                              "-" + randObj.Next(10, 99);
                var passportData = chars[randObj.Next(chars.Length)].ToString() + chars[randObj.Next(chars.Length)].ToString() + randObj.Next(100000, 999999);

                db.Customers.Add(new Customer()
                {
                    FullName = fullName,
                    Address = address,
                    Phone = phoneNumber,
                    PassportData = passportData
                });
            }
            db.SaveChanges();
        }

        private static void BrigadeGenerate(ConstructionCompanyContext db, int brigadeCount)
        {
            if (db.Brigades.Any())
            {
                return;
            }

            for (var i = 0; i < brigadeCount; i++)
            {
                var name = "Brigade" + randObj.Next(1, 10000);
                db.Brigades.Add(new Brigade()
                {
                    Name = name
                });
            }
            db.SaveChanges();
        }
    }
}

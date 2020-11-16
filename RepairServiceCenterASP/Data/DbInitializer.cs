using RepairServiceCenterASP.Models;
using System;
using System.Linq;

namespace RepairServiceCenterASP.Data
{
    public static class DbInitializer
    {
        private static Random randObj = new Random(1);

        public static void Initialize(RepairServiceCenterContext db)
        {
            db.Database.EnsureCreated();
            
            int repairedModelsNumber = 100;
            int sparePartsNumber = 500;
            int typeOfFaultsNumber = 100;
            int serviceStoreNumber = 100;
            int countOrders = 100;

            RepairedModelsGenerate(repairedModelsNumber, ref db);
            TypeOfFaultGeneration(typeOfFaultsNumber, repairedModelsNumber, ref db);
            SparePartsGeneration(sparePartsNumber, repairedModelsNumber, typeOfFaultsNumber, ref db);
            ServiceStoreGeneration(serviceStoreNumber, ref db);
            PostGenerate(ref db);
            EmployeeGenerate(ref db);
            OrdersGenerate(countOrders, ref db);
        }

        private static void OrdersGenerate(int count, ref RepairServiceCenterContext db)
        {
            if (db.Orders.Any())
            {
                return;
            }

            int countTypeOfFault = db.TypeOfFaults.Count();
            int countServicedStore = db.ServicedStores.Count();
            int countEmployee = db.Employees.Count();
            int countRepairedModel = db.RepairedModels.Count();

            DateTime dateOrder;
            DateTime returnDate;
            string fullNameCust;
            int repairedModelId;
            int typeOfFaultId;
            int serviceStoreId;
            bool guaranteeMark;
            int guaranteePeriod;
            int employeeId;
            double price;

            string[] namesVoc = { "Жмайлик А.В.", "Сетко А.И.", "Семёнов С.А.", "Давыдчик А.Е.", "Пискун Е.А.",
                                  "Дракула В.А.", "Ястребов А.В.", "Степаненко Ю.А.", "Башаримов Ю.И.", "Каркозов В.В." };

            for (int i = 0; i < count; i++)
            {
                dateOrder = DateTime.Now.AddDays(-randObj.Next(1000));
                returnDate = dateOrder.AddDays(randObj.Next(1, 40));
                fullNameCust = namesVoc[randObj.Next(namesVoc.GetLength(0))];
                repairedModelId = randObj.Next(1, countRepairedModel - 1);
                typeOfFaultId = randObj.Next(1, countTypeOfFault - 1);
                serviceStoreId = randObj.Next(1, countServicedStore - 1);
                employeeId = randObj.Next(1, countEmployee - 1);
                guaranteeMark = randObj.Next(1000) > 500 ? true : false;
                guaranteePeriod = randObj.Next(120);

                var typeOfFaults = db.TypeOfFaults.Where(t => t.TypeOfFaultId == typeOfFaultId)
                                                   .FirstOrDefault();
                price = (double)typeOfFaults.WorkPrice * 2 * randObj.NextDouble(); ;

                db.Orders.Add(new Order()
                {
                    DateOrder = dateOrder,
                    ReturnDate = returnDate,
                    FullNameCustumer = fullNameCust,
                    RepairedModelId = repairedModelId,
                    TypeOfFaultId = typeOfFaultId,
                    ServicedStoreId = serviceStoreId,
                    GuaranteeMark = guaranteeMark,
                    GuaranteePeriod = guaranteePeriod,
                    EmployeeId = employeeId,
                    Price = price
                });
            }
            db.SaveChanges();
        }

        private static void PostGenerate(ref RepairServiceCenterContext db)
        {
            if (db.Posts.Any())
            {
                return;
            }

            db.Posts.Add(new Post()
            {
                Name = "Директор",
                Money = 5000
            });
            db.Posts.Add(new Post()
            {
                Name = "Зам. директора",
                Money = 2500
            });
            db.Posts.Add(new Post()
            {
                Name = "Программист",
                Money = 2000
            });
            db.Posts.Add(new Post()
            {
                Name = "Инженер",
                Money = 1500
            });
            db.Posts.Add(new Post()
            {
                Name = "Главный-инженер",
                Money = 2100
            });
            db.Posts.Add(new Post()
            {
                Name = "ИТ-директор",
                Money = 3500
            });
            db.Posts.Add(new Post() 
            {
                Name = "ИТ-менеджер",
                Money = 2500
            });
            db.Posts.Add(new Post() //9
            {
                Name = "Сантехник",
                Money = 500
            });
            db.Posts.Add(new Post() //10
            {
                Name = "Уборщик",
                Money = 300
            });
            db.Posts.Add(new Post() //11
            {
                Name = "Мед. сестра",
                Money = 550
            });
            db.Posts.Add(new Post() //12
            {
                Name = "Продавец",
                Money = 400
            });
            db.SaveChanges();
        }

        private static void EmployeeGenerate(ref RepairServiceCenterContext db)
        {
            if (db.Employees.Any())
            {
                return;
            }

            db.Employees.Add(new Employee()
            {
                FullName = "Трофимов Е.В.",
                Experience = 10,
                Post = db.Posts.Where(p => p.Name == "Директор")
                               .FirstOrDefault(),
            });
            db.Employees.Add(new Employee()
            {
                FullName = "Солодков Е.В.",
                Experience = 8,
                Post = db.Posts.Where(p => p.Name == "Программист")
                               .FirstOrDefault(),
            });
            db.Employees.Add(new Employee()
            {
                FullName = "Ропот И.В.",
                Experience = 10,
                Post = db.Posts.Where(p => p.Name == "Инженер")
                               .FirstOrDefault(),
            });
            db.Employees.Add(new Employee()
            {
                FullName = "Липский Д.Ю.",
                Experience = 10,
                Post = db.Posts.Where(p => p.Name == "ИТ-менеджер")
                               .FirstOrDefault(),
            });
            db.Employees.Add(new Employee()
            {
                FullName = "Межейников А.С.",
                Experience = 10,
                Post = db.Posts.Where(p => p.Name == "ИТ-директор")
                               .FirstOrDefault(),
            });
            db.Employees.Add(new Employee()
            {
                FullName = "Михайлов А.С.",
                Experience = 10,
                Post = db.Posts.Where(p => p.Name == "Главный-инженер")
                               .FirstOrDefault(),
            });
            db.Employees.Add(new Employee()
            {
                FullName = "Козаченко М.А.",
                Experience = 10,
                Post = db.Posts.Where(p => p.Name == "Уборщик")
                               .FirstOrDefault(),
            });
            db.Employees.Add(new Employee()
            {
                FullName = "Главич Д.Ю.",
                Experience = 10,
                Post = db.Posts.Where(p => p.Name == "Мед. сестра")
                               .FirstOrDefault(),
            });
            db.Employees.Add(new Employee()
            {
                FullName = "Стольный С.В.",
                Experience = 10,
                Post = db.Posts.Where(p => p.Name == "Инженер")
                               .FirstOrDefault(),
            });
            db.SaveChanges();
        }

        private static void ServiceStoreGeneration(int num, ref RepairServiceCenterContext db)
        {
            if (db.ServicedStores.Any())
            {
                return;
            }

            string name;
            string address;
            string phoneNumber;

            string[] namesVoc = {"PriceRent", "RentalCars", "ServiceTransportOnline", "PhonesOne", "BestSpendTime",
                                  "БелГосСтах", "SAMSUNG STORE", "Бай-Бак", "Zeon"};
            string[] addressVoc = {"пер.Заслонова, ", "ул.Гастело, ", "ул.Полесская, ", "пр.Речецкий, ", "ул, Интерноциональная, ",
                                    "пр.Октября, ", "ул.Бассейная, ", "бул.Юности, " };

            for (int i = 0; i < num; i++)
            {
                name = namesVoc[randObj.Next(namesVoc.GetLength(0))] + " " + randObj.Next(0, num + 50);
                address = addressVoc[randObj.Next(addressVoc.GetLength(0))] + randObj.Next(0, 250);
                phoneNumber = "+375 (29) " + randObj.Next(100, 999) + "-" + randObj.Next(10, 99) +
                              "-" + randObj.Next(10, 99);
                db.ServicedStores.Add(new ServicedStore()
                {
                    Name = name,
                    Address = address,
                    PhoneNumber = phoneNumber
                });
                db.SaveChanges();
            }
        }
        
        private static void TypeOfFaultGeneration(int num, int rModelsNum, ref RepairServiceCenterContext db)
        {
            if (db.TypeOfFaults.Any())
            {
                return;
            }

            int repairedModelId;
            string name;
            string methodRepair;
            double workPrice;

            string[] namesVoc = { "Повреждён дисплей", "Повреждение электроники", "Визуальные повреждения",
                                  "Повреждена проводка" };
            string[] methodRepairVoc = { "Полная замена деталей", "Частичная замена", "Незначительный ремонт" };

            int count = 100;
            for (int i = 0; i < num; i++)
            {
                repairedModelId = randObj.Next(1, rModelsNum - 1);
                name = namesVoc[randObj.Next(namesVoc.GetLength(0))] + " " + randObj.Next(1, rModelsNum - 1);
                methodRepair = methodRepairVoc[randObj.Next(methodRepairVoc.GetLength(0))];

                var RepairedModel = db.RepairedModels.Where(r => r.RepairedModelId == repairedModelId)
                                                     .FirstOrDefault();
                workPrice = repairedModelId * 2 * randObj.NextDouble();

                db.TypeOfFaults.Add(new TypeOfFault
                {
                    RepairedModelId = repairedModelId,
                    Name = name,
                    MethodRepair = methodRepair,
                    WorkPrice = workPrice
                });
                if (i - count == 0)
                {
                    db.SaveChanges();
                    count += 100;
                }
            }
            db.SaveChanges();

        }

        private static void SparePartsGeneration(int num, int rModelsNum, int typeNum, ref RepairServiceCenterContext db)
        {
            if (db.SpareParts.Any())
            {
                return;
            }

            string name;
            string function;
            double price;
            int repairedModelId;
            int typeOfFaultId;

            string[] namesVoc = { "Дисплей-", "Процессор-", "Проводка-", "Корпус-", "Комплектующие-" };
            string[] functionVoc = { "Create", "Read", "Update", "Delete" };

            int count = 100;
            for (int i = 0; i < num; i++)
            {
                name = namesVoc[randObj.Next(namesVoc.GetLength(0))] + i.ToString();
                price = 30 * randObj.NextDouble();
                function = functionVoc[randObj.Next(functionVoc.GetLength(0))];
                repairedModelId = randObj.Next(1, rModelsNum - 1);
                typeOfFaultId = randObj.Next(1, typeNum - 1);

                db.SpareParts.Add(new SparePart()
                {
                    Name = name,
                    Functions = function,
                    Price = price,
                    RepairedModelId = repairedModelId,
                    TypeOfFaultId = typeOfFaultId
                });
                if (i - count == 0)
                {
                    db.SaveChanges();
                    count += 100;
                }
            }
            db.SaveChanges();
        }

        private static void RepairedModelsGenerate(int num, ref RepairServiceCenterContext db)
        {
            if (db.RepairedModels.Any())
            {
                return;
            }

            string name;
            string type;
            string manafacture;
            string techSpecification;
            string features;

            string[] namesVoc = { "FE-", "AM-", "S-", "A-", "PES-", "CAT-", "DOG-", "T-" };
            string[] typesVoc = { "Производственное", "Медицинское", "Строительное", "Военное" };
            string[] manafacturesVoc = { "SAMSUNG", "PHILIPS", "HONDA", "Google", "ASUS", "БелТех", "МозырьСтрой" };
            string[] featuresVoc = { "Полный ремонт", "Полная функциональность", "Обновление", "Приемлемый вид" };

            for (int i = 0; i < num; i++)
            {
                name = namesVoc[randObj.Next(namesVoc.GetLength(0))] + i.ToString();
                type = typesVoc[randObj.Next(typesVoc.GetLength(0))];
                manafacture = manafacturesVoc[randObj.Next(typesVoc.GetLength(0))];
                techSpecification = "CF18" + randObj.Next(100000, 999999);
                features = featuresVoc[randObj.Next(featuresVoc.GetLength(0))];

                db.RepairedModels.Add(new RepairedModel()
                {
                    Name = name,
                    Type = type,
                    Manufacturer = manafacture,
                    TechSpecification = techSpecification,
                    Features = features
                });
            }

            db.SaveChanges();
        }
    }
}

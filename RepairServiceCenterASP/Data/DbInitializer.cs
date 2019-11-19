using RepairServiceCenterASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.Data
{
    public static class DbInitializer
    {
        private static Random randObj = new Random(1);

        public static void Initialize(RepairServiceCenterContext db)
        {
            db.Database.EnsureCreated();
            
            int repairedModelsNumber = 35;
            int sparePartsNumber = 200;
            int typeOfFaultsNumber = 35;

            RepairedModelsGenerate(repairedModelsNumber, ref db);
            TypeOfFaultGeneration(typeOfFaultsNumber, repairedModelsNumber, ref db);
            SparePartsGeneration(sparePartsNumber, repairedModelsNumber, typeOfFaultsNumber, ref db);

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

            for (int i = 0; i < num; i++)
            {
                repairedModelId = randObj.Next(1, rModelsNum - 1);
                name = namesVoc[randObj.Next(namesVoc.GetLength(0))];
                methodRepair = methodRepairVoc[randObj.Next(methodRepairVoc.GetLength(0))];
                workPrice = randObj.NextDouble();

                db.TypeOfFaults.Add(new TypeOfFault
                {
                    RepairedModelId = repairedModelId,
                    Name = name,
                    MethodRepair = methodRepair,
                    WorkPrice = workPrice
                });
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
            
            for (int i = 0; i < num; i++)
            {
                name = namesVoc[randObj.Next(namesVoc.GetLength(0))] + i.ToString();
                price = 1500 * randObj.NextDouble();
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

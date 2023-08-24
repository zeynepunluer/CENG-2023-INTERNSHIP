using System;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBExample
{
    class IdInfo
    {
        public ObjectId Id { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string SerialNumber { get; set; }
        public string ExpDate { get; set; }
        public string Nationality { get; set; }
    }

    class Operations
    {
        private MongoClient client;
        private IMongoCollection<IdInfo> collection;

        public Operations(string connectionString, string databaseName)
        {
            client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            collection = database.GetCollection<IdInfo>("IdInfo");
            collection.DeleteMany(_ => true);
        }


        public void DisplayPerson(IdInfo person)
        {
            Type type = typeof(IdInfo);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                object propertyValue = property.GetValue(person);

                Console.WriteLine($"{propertyName}: {propertyValue}");
            }

            Console.WriteLine();
        }
        public void InsertInfo(IdInfo info)
        {
            collection.InsertOne(info);
            DisplayPerson(info);

        }
        public void FindInfo(string id)
        {
            var filter = Builders<IdInfo>.Filter.Eq("ID", id);
            var result = collection.Find(filter).FirstOrDefault();

            if (result != null)
            {
                Console.WriteLine("Founded ID Information: ");
                DisplayPerson(result);
            }
            else
            {
                Console.WriteLine("Cannot find the ID info.");
            }

        }
        public void UpdateInfo(string id, string context, string newValue)
        {
            var filter = Builders<IdInfo>.Filter.Eq("ID", id);
            var update = Builders<IdInfo>.Update.Set(context, newValue);
            var updatedInfo = collection.UpdateOne(filter, update);


            if (updatedInfo.ModifiedCount > 0)
            {
                Console.WriteLine("Updated successfully");
                var result = collection.Find<IdInfo>(filter).FirstOrDefault();
                DisplayPerson(result);


            }
            else
            {
                Console.WriteLine("Update Failed");
            }


        }

        public void DeleteInfo(string id)
        {
            var delete = Builders<IdInfo>.Filter.Eq("ID", id);
            var deletedPerson = collection.Find(delete).FirstOrDefault();

            if (deletedPerson != null)
            {
                Console.WriteLine("Person to be deleted is: ");
                DisplayPerson(deletedPerson);
                var deleted = collection.DeleteOne(delete);
                if (deleted.DeletedCount > 0)
                {
                    Console.WriteLine("Person is deleted successfully");
                }
                else
                {
                    Console.WriteLine("Deletion failed.");
                }
            }
            else
            {
                Console.WriteLine("Person with the specified ID not found or already deleted.");
            }

        }

        class Program
        {
            public static void Main(string[] args)
            {
                var connectionString = "mongodb://localhost:27017";
                var databaseName = "database1";
                var operations = new Operations(connectionString, databaseName);

                var Info1 = new IdInfo
                {
                    ID = "21070901312",
                    Name = "Melissa",
                    Surname = "Vargas",
                    Birthday = "16/10/1999",
                    Gender = "Kadın",
                    SerialNumber = "A10B01C10",
                    ExpDate = "10/08/2030",
                    Nationality = "CUBA"
                };

                var Info2 = new IdInfo
                {
                    ID = "20070902509",
                    Name = "Ebrar",
                    Surname = "Karakurt",
                    Birthday = "17/01/2000",
                    Gender = "Kadın",
                    SerialNumber = "A20B02C20",
                    ExpDate = "20/08/2030",
                    Nationality = "TURKIYE"
                };

                var Info3 = new IdInfo
                {
                    ID = "20070902510",
                    Name = "İlkin",
                    Surname = "Aydın",
                    Birthday = "5/1/2000",
                    Gender = "Kadın",
                    SerialNumber = "A30B03C30",
                    ExpDate = "25/08/2030",
                    Nationality = "TURKIYE"
                };
                operations.InsertInfo(Info1);
                operations.InsertInfo(Info2);
                operations.InsertInfo(Info3);
                operations.FindInfo("20070902510");
                operations.UpdateInfo("21070901312", "Nationality", "TURKIYE");
                operations.DeleteInfo("20070902509");


          


            }
        }
    }
}

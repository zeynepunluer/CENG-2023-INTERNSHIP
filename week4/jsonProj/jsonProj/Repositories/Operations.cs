using jsonProj.DTOs;
using jsonProj.Models;
using MongoDB.Driver;
using System.Reflection;


namespace jsonProj.Repositories
{
    public class Operations
    {
        private MongoClient client;
        private IMongoCollection<IdInfo> collection;
        public Operations(MongoDBSettings mongoDBSettings)
        {
            client = new MongoClient(mongoDBSettings.ConnectionString);
            var database = client.GetDatabase(mongoDBSettings.DatabaseName);
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
        public IdInfo? FindInfo(string id)
        {
            var filter = Builders<IdInfo>.Filter.Eq("ID", id);
            var result = collection.Find(filter).FirstOrDefault();

            if (result != null)
            {
                Console.WriteLine("Founded ID Information: ");
                DisplayPerson(result);
                return result;
            }
            else
            {
                Console.WriteLine("Cannot find the ID info.");
                return null;

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

    }
}

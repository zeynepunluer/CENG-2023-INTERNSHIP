using MongoDB.Bson;

namespace jsonProj.Models
{
    public class IdInfo
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

        public IdInfo(string ID, string Name, string Surname, string Birthday, string Gender, string SerialNumber, string ExpDate, string Nationality)
        {
            this.ID = ID;
            this.Name = Name;
            this.Surname = Surname;
            this.Birthday = Birthday;
            this.Gender = Gender;
            this.SerialNumber = SerialNumber;
            this.ExpDate = ExpDate;
            this.Nationality = Nationality;
        }
    }

}

using Newtonsoft.Json;

namespace SQASpeakerService.Models
{
    public class Speaker
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("surname")]
        public string Surname { get; set; }
        
        [JsonProperty("company")]
        public string Company { get; set; }

        public Speaker(int id, string name, string surname, string company)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Company = company;
        }

        private bool Equals(Speaker other)
        {
            return Equals(Id, other.Id) && string.Equals(Name, other.Name) && string.Equals(Surname, other.Surname) && string.Equals(Company, other.Company);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Speaker) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Surname != null ? Surname.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Company != null ? Company.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}

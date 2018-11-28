using Realms;

namespace App1.Models
{
    public class Movie : RealmObject
    {
        [PrimaryKey]
        public string MovieID { get; set; }
        public string Title { get; set; }
    }
}
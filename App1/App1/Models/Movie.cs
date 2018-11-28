using Realms;

namespace App1.Models
{
    public class Movie : RealmObject
    {
        [PrimaryKey]
        public int MovieID { get; set; }
        public string Title { get; set; }
    }
}
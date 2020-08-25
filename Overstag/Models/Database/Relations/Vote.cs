using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Overstag.Models.Database.Relations
{
    //User - Suggestion
    [Table("user_suggestion")]
    public class Vote
    {
        [JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public Suggestion Suggestion { get; set; }
        public int SuggestionId { get; set; }

        public bool Upvote { get; set; }
    }
}

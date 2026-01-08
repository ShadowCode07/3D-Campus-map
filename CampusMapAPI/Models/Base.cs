using System.ComponentModel.DataAnnotations;

namespace CampusMapAPI.Models
{
    public abstract class Base
    {
        [Key]
        public int Id { get; set; }

        [Timestamp]
        public byte[] Version { get; set; } = Array.Empty<byte>();
    }
}

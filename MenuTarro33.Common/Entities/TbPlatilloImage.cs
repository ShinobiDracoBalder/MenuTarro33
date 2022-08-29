using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MenuTarro33.Common.Entities
{
    public class TbPlatilloImage
    {
        [Key]
        public int PlatilloImageId { get; set; }
        public Guid ImageId { get; set; }
        public string ImagePath { get; set; }
        public Guid VideoId { get; set; }
        public string VideoPath { get; set; }
        [ForeignKey("TbPlatillo")]
        public int PlatilloId { get; set; }
        [JsonIgnore]
        public TbPlatillo TbPlatillo { get; set; }
    }
}

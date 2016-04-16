using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//make sure the namespace is equal to the other partial class ItemRequest
namespace DARE.Models
{
    [MetadataType(typeof(ClientValidation))]
    public partial class Client
    {
    }

    public class ClientValidation
    {
        public int ClientID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage ="Only 50 characters allowed.")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Only 50 characters allowed.")]
        public string Type { get; set; }
        public string ClientCode { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Only 200 characters allowed.")]
        public string Description { get; set; }

    }
}
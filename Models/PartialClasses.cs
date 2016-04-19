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

    [MetadataType(typeof(ActionValidation))]
    public partial class Action
    {
    }

    public class ActionValidation
    {
        [Required]
        [StringLength(50, ErrorMessage ="Only 50 characters allowed.")]
        public string Name { get; set; }
        [StringLength(200, ErrorMessage = "Only 200 characters allowed.")]
        [Required]
        public string Description { get; set; }
    }

    [MetadataType(typeof(NoteValidation))]
    public partial class Note
    {
    }

    public class NoteValidation
    {
        [Required]
        [StringLength(200, ErrorMessage = "Only 200 characters allowed.")]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }

    [MetadataType(typeof(ScheduleValidation))]
    public partial class Schedule
    {
    }

    public class ScheduleValidation
    {
        [Required]
        [Display(Name = "Action")]
        public int ActionID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Only 50 characters allowed.")]
        public string Name { get; set; }
        [Required]
        public string CronExpression { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Only 100 characters allowed.")]
        public string Description { get; set; }
    }

    [MetadataType(typeof(ThingValidation))]
    public partial class Thing
    {
    }

    public class ThingValidation
    {
        [Required]
        [StringLength(100, ErrorMessage = "Only 100 characters allowed.")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [StringLength(50, ErrorMessage = "Only 50 characters allowed.")]
        [Required]
        public string Type { get; set; }
    }

    [MetadataType(typeof(UserValidation))]
    public partial class User
    {
    }

    public class UserValidation
    {
        [Required]
        [StringLength(50, ErrorMessage = "Only 50 characters allowed.")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(255, ErrorMessage = "Only 255 characters allowed.")]
        public string Email { get; set; }
        [Required]
        public string PasswordQuestion { get; set; }
        [Required]
        public string PasswordAnswer { get; set; }
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
        [StringLength(50, ErrorMessage = "Only 50 characters allowed.")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "Only 50 characters allowed.")]
        public string LastName { get; set; }
    }
}
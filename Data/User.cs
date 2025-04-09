using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyMvcApp.Data
{
    [Table("tbl_users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        [StringLength(200)]
        public string? Image {  get; set; }
        [StringLength(20)]
        public string? Phone { get; set; }
        public bool Sex { get; set; }
    }
}

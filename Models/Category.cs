using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace Shop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Display Order")]
        [Range(1,int.MaxValue, ErrorMessage ="Value must be more than zero.")]
        public int DisplayOrder { get; set; }
    }
}

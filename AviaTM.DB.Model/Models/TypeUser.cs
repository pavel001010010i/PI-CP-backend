using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AviaTM.DB.Model.Models
{
    public class TypeUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        
        [Required]
        public bool isActive { get; set; }
    }
}

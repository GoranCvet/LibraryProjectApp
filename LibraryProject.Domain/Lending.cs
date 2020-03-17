using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryProject.Domain
{
    public class Lending
    {
        public int Id { get; set; }
        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatumZajmuvanje { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime? DatumVratena { get; set; }

        public Client Client { get; set; }
        [Display(Name = "Client Name")]
        public int ClientId { get; set; }
        public Book Book { get; set; }
        [Display(Name ="Book Title")]
        public int BookId { get; set; }
        public Library Library { get; set; }
        public int LibraryId { get; set; }
    }
}

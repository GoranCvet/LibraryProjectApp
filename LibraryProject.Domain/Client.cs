using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryProject.Domain
{
    public class Client
    {
        public Client()
        {
            Lendings = new List<Lending>();
        }
        public int Id { get; set; }
        [Required, StringLength(50, ErrorMessage = "Name length can't be more than 50")]
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public List<Lending> Lendings { get; set; }
    }
}

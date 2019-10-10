using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
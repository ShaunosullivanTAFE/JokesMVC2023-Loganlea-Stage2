using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JokesMVC2023.Models.Data
{
    public class Joke
    {
        public int Id { get; set; }

        [Display(Name="Joke Question")]
        [Required]
        [StringLength(100)]
        public string JokeQuestion { get; set; }

        [Display(Name="Joke Answer")]
        public string JokeAnswer { get; set; }
    }
}

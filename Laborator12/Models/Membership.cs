using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laborator12.Models
{
    public class Membership
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Titlul abonamentului este obligatoriu.")]
        public string Titlu { get; set; }

        [Required(ErrorMessage = "Valoarea abonamentului este obligatorie.")]
        [Range(1, int.MaxValue, ErrorMessage = "Valoarea abonamentului trebuie să fie un număr pozitiv.")]
        public int Valoare { get; set; }

        [Required(ErrorMessage = "Data emiterii abonamentului este obligatorie.")]
        public DateTime DataEmitere { get; set; } = DateTime.Now; // Setează data curentă ca valoare implicită

        [Required(ErrorMessage = "Sala de sport este obligatorie.")]
        public int GymId { get; set; }

        [ForeignKey("GymId")]
        public Gym Gym { get; set; }
    }

    public class Gym
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele sălii de sport este obligatoriu.")]
        public string Nume { get; set; }

        public ICollection<Membership> Memberships { get; set; } // Relația one-to-many
    }
}
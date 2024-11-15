using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TD1.Models.EntityFramework
{
    [Table("marque")]
    public class Marque
    {
        [Key]
        [Column("idmarque")]

        public int Idmarque { get; set; }

        [Column("nommarque")]

        public String? Nommarque { get; set; }

        [InverseProperty(nameof(Produit.IdmarqueNavigation))]
        public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // Conversion de l'objet pour le comparer
            Marque other = (Marque)obj;

            // On compare ici uniquement l'Idmarque qui est une clé unique
            return Idmarque == other.Idmarque && Nommarque == other.Nommarque;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Idmarque, Nommarque, Produits);
        }
    }
}

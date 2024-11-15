using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TD1.Models.EntityFramework
{
    [Table("typeproduit")]
    public class TypeProduit
    {
        [Key]
        [Column("idtypeproduit")]

        public int Idtypeproduit { get; set; }

        [Column("nomtypeproduit")]

        public String? Nomtypeproduit { get; set; }

        [InverseProperty(nameof(Produit.IdtypeproduitNavigation))]
        public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            TypeProduit other = (TypeProduit)obj;

            // On compare ici uniquement l'Idmarque qui est une clé unique
            return Idtypeproduit == other.Idtypeproduit && Nomtypeproduit == other.Nomtypeproduit;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Idtypeproduit, Nomtypeproduit, Produits);
        }
    }
}

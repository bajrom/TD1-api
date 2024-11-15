using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TD1.Models.EntityFramework
{
    [Table("produit")]
    public class Produit
    {
        [Key]
        [Column("idproduit")]

        public int Idproduit { get; set; }

        [Column("nomProduit")]

        public String? Nomproduit { get; set; }

        [Column("description")]

        public String? Description { get; set; }

        [Column("nomphoto")]

        public String? Nomphoto { get; set; }

        [Column("uriphoto")]

        public String? Uriphoto { get; set; }

        [Column("stockreel")]

        public int Stockreel { get; set; }

        [Column("stockmin")]

        public int Stockmin { get; set; }

        [Column("stockmax")]

        public int Stockmax { get; set; }



        [ForeignKey("idmarque")]
        [InverseProperty(nameof(Marque.Produits))]
        public int? Idmarque { get; set; }

        [ForeignKey("idtypeproduit")]
        [InverseProperty(nameof(TypeProduit.Produits))]
        public int? Idtypeproduit { get; set; }




        [ForeignKey(nameof(Idmarque))]
        [InverseProperty(nameof(Marque.Produits))]
        public virtual Marque IdmarqueNavigation { get; set; } = null!;

        [ForeignKey(nameof(Idtypeproduit))]
        [InverseProperty(nameof(TypeProduit.Produits))]
        public virtual TypeProduit IdtypeproduitNavigation { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var produit = (Produit)obj;

            return Idproduit == produit.Idproduit &&
                   Nomproduit == produit.Nomproduit &&
                   Description == produit.Description &&
                   Nomphoto == produit.Nomphoto &&
                   Uriphoto == produit.Uriphoto &&
                   Stockreel == produit.Stockreel &&
                   Stockmin == produit.Stockmin &&
                   Stockmax == produit.Stockmax &&
                   Idmarque == produit.Idmarque &&
                   Idtypeproduit == produit.Idtypeproduit;
        }
    }
}

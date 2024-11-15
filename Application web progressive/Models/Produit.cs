using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application_Webassembly_Blazor.Models
{
    public class Produit
    {

        public int Idproduit { get; set; }

        public String? Nomproduit { get; set; }

        public String? Description { get; set; }

        public String? Nomphoto { get; set; }

        public String? Uriphoto { get; set; }

        public int Stockreel { get; set; }

        public int Stockmin { get; set; }

        public int Stockmax { get; set; }

        public int? Idmarque { get; set; }

        public int? Idtypeproduit { get; set; }

        public virtual Marque IdmarqueNavigation { get; set; } = null!;

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

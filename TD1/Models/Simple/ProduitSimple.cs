namespace TD1.Models.Simple
{
    public class ProduitSimple
    {
        public int Idproduit {  get; set; }
        public string? NomProduit { get; set; }

        public string? Description { get; set; }

        public string? Nomphoto { get; set; }

        public string? Uriphoto { get; set; }

        public int Stockreel { get; set; }

        public int Stockmin { get; set; }

        public int Stockmax { get; set; }

        public int IdMarque { get; set; }
        public int IdTypeproduit { get; set; }
    }
}

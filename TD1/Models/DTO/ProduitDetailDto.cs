namespace TD1.Models.DTO
{
    public class ProduitDetailDto
    {
        public int Idproduit { get; set; }
        public string? Nomproduit { get; set; }
        public string? Idmarque { get; set; }
        public string? Idtypeproduit { get; set; }
        public string? Description { get; set; }
        public string? Nomphoto { get; set; }
        public string? Uriphoto { get; set; }
        public int? Stockreel { get; set; }
        public bool EnReappro { get; set; }
    }
}

namespace Application_Webassembly_Blazor.Models.Services
{
    public interface IService
    {
        Task<List<Produit>> GetProduitsAsync(string nomControleur);
    }
}

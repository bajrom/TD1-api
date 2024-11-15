using AutoMapper;
using TD1.Models.EntityFramework;

namespace TD1.Models.DTO
{
    public class ProduitProfile : Profile
    {
        public ProduitProfile()
        {
            CreateMap<Produit, ProduitDto>();
            CreateMap<Produit, ProduitDetailDto>()
                .ForMember(dest => dest.Nomproduit, opt => opt.MapFrom(src => src.Nomproduit))
                .ForMember(dest => dest.Idtypeproduit, opt => opt.MapFrom(src => src.IdtypeproduitNavigation.Idtypeproduit))  // Adapt if necessary
                .ForMember(dest => dest.Idmarque, opt => opt.MapFrom(src => src.IdmarqueNavigation.Idmarque))  // Adapt if necessary
                .ForMember(dest => dest.Stockreel, opt => opt.MapFrom(src => src.Stockreel))
                .ForMember(dest => dest.EnReappro, opt => opt.MapFrom(src => src.Stockreel < src.Stockmin));
        }
    }
}

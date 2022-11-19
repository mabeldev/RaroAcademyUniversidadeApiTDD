using AutoMapper;
using demys_universidade.Domain.Contracts.Request;
using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Entities;

namespace demys_universidade.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Entity to Request
            CreateMap<UsuarioRequest, Usuario>();
            CreateMap<CursoRequest, Curso>();
            CreateMap<DepartamentoRequest, Departamento>();
            CreateMap<EnderecoRequest, Endereco>();
            CreateMap<PerfilRequest, Perfil>();



            #endregion

            #region Response to Entity
            CreateMap<Curso, CursoResponse>().ReverseMap();
            CreateMap<Departamento, DepartamentoResponse>().ReverseMap();
            CreateMap<Usuario, UsuarioResponse>().ReverseMap();
            CreateMap<Endereco, EnderecoResponse>().ReverseMap();
            CreateMap<Perfil, PerfilResponse>().ReverseMap();


            #endregion

            #region HttpApi Response to Entity
            CreateMap<BrasilCep, EnderecoResponse>()
                .ForMember(p => p.CEP, map => map.MapFrom(s => s.Cep))
                .ForMember(p => p.Rua, map => map.MapFrom(s => s.Street))
                .ForMember(p => p.Cidade, map => map.MapFrom(s => s.City))
                .ForMember(p => p.Estado, map => map.MapFrom(s => s.State));

            #endregion

        }
    }
}

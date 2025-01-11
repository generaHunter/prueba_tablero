using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Estado.Commands.DefaultModel;
using tablero.Application.DataBase.Tablero.Commands.UpdateTablero;
using tablero.Application.DataBase.Tablero.DefaultModel;
using tablero.Application.DataBase.Tarea.Commands.DefultModel;
using tablero.Application.DataBase.Tarea.Commands.UpdateTarea;
using tablero.Application.DataBase.Usuario.Commands.UpdateUsuario;
using tablero.Application.DataBase.Usuario.DefaultModel;
using tablero.Domain.Entities.Estado;
using tablero.Domain.Entities.Tablero;
using tablero.Domain.Entities.Tarea;
using tablero.Domain.Entities.Usuario;

namespace tablero.Application.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Estado
            CreateMap<EstadoEntity, DefaultEstadoModel>().ReverseMap();
            #endregion

            #region Tablero
            CreateMap<TableroEntity, DefaultTableroModel>().ReverseMap();
            CreateMap<TableroEntity, UpdateTableroModel>().ReverseMap();
            #endregion

            #region Tarea
            CreateMap<TareaEntity, DefaultTareaModel>().ReverseMap();
            CreateMap<TareaEntity, UpdateTareaModel>().ReverseMap();


            //CreateMap<BookingEntity, BookingTestModel>().ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));
            #endregion

            #region Usuario
            CreateMap<UsuarioEntity, DefaultUsuarioModel>().ReverseMap();
            CreateMap<UsuarioEntity, UpdateUsuarioModel>().ReverseMap();
            #endregion
        }
    }
}

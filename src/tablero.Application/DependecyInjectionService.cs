using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.Configuration;
using tablero.Application.DataBase.Estado.Commands.CreateEstado;
using tablero.Application.DataBase.Tablero.Commands.CreateTablero;
using tablero.Application.DataBase.Tablero.Commands.DeleteTablero;
using tablero.Application.DataBase.Tablero.Commands.UpdateTablero;
using tablero.Application.DataBase.Tarea.Commands.CreateTarea;
using tablero.Application.DataBase.Tarea.Commands.DeleteTarea;
using tablero.Application.DataBase.Tarea.Commands.UpdateTarea;
using tablero.Application.DataBase.Usuario.Commands.CreateUsuario;
using tablero.Application.DataBase.Usuario.Commands.DeleteUsuario;
using tablero.Application.DataBase.Usuario.Commands.UpdateUsuario;

namespace tablero.Application
{
    public static class DependecyInjectionService
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var mapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new MapperProfile());
            });

            services.AddSingleton(mapper.CreateMapper());

            #region Servicios Estado
            services.AddTransient<ICreateEstadoCommand, CreateEstadoCommand>();
            #endregion

            #region Servicios Tablero
            services.AddTransient<ICreateTableroCommand, CreateTableroCommand>();
            services.AddTransient<IDeleteTableroCommand, DeleteTableroCommand>();
            services.AddTransient<IUpdateTableroCommand, UpdateTableroCommand>();
            #endregion

            #region Servicios Tarea
            services.AddTransient<ICreateTareaCommand, CreateTareaCommand>();
            services.AddTransient<IDeleteTareaCommand, DeleteTareaCommand>();
            services.AddTransient<IUpdateTareaCommand, UpdateTareaCommand>();
            #endregion

            #region Servicios Usuario
            services.AddTransient<ICreateUsuarioCommand, CreateUsuarioCommand>();
            services.AddTransient<IDeleteUsuarioCommand, DeleteUsuarioCommand>();
            services.AddTransient<IUpdateUsuarioCommand, UpdateUsuarioCommand>();
            #endregion

            return services;
        }
    }
}

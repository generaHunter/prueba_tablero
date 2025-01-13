using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.Configuration;
using tablero.Application.DataBase.Estado.Commands.CreateEstado;
using tablero.Application.DataBase.Estado.Queries.GetAllEstadosQuery;
using tablero.Application.DataBase.Reportes.ReporteTareas;
using tablero.Application.DataBase.SeedData.Commands.GenerateSeedData;
using tablero.Application.DataBase.Tablero.Commands.CreateTablero;
using tablero.Application.DataBase.Tablero.Commands.DeleteTablero;
using tablero.Application.DataBase.Tablero.Commands.UpdateTablero;
using tablero.Application.DataBase.Tablero.Queries.GetAllTableros;
using tablero.Application.DataBase.Tablero.Queries.GetTableroById;
using tablero.Application.DataBase.Tarea.Commands.CreateTarea;
using tablero.Application.DataBase.Tarea.Commands.DeleteTarea;
using tablero.Application.DataBase.Tarea.Commands.UpdateEstadoTarea;
using tablero.Application.DataBase.Tarea.Commands.UpdateTarea;
using tablero.Application.DataBase.Tarea.Queries.GetAllTareas;
using tablero.Application.DataBase.Tarea.Queries.GetTareaById;
using tablero.Application.DataBase.Tarea.Queries.GetTareasByTableroId;
using tablero.Application.DataBase.Usuario.Commands.CreateUsuario;
using tablero.Application.DataBase.Usuario.Commands.DeleteUsuario;
using tablero.Application.DataBase.Usuario.Commands.UpdateUsuario;
using tablero.Application.DataBase.Usuario.Queries.GetUserByCredentials;
using tablero.Application.Validators.User;

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
            services.AddTransient<IGetAllEstadosQuery, GetAllEstadosQuery>();
            #endregion

            #region Servicios Tablero
            services.AddTransient<ICreateTableroCommand, CreateTableroCommand>();
            services.AddTransient<IDeleteTableroCommand, DeleteTableroCommand>();
            services.AddTransient<IUpdateTableroCommand, UpdateTableroCommand>();
            services.AddTransient<IGetAllTablerosQuery, GetAllTablerosQuery>();
            services.AddTransient<IGetTableroByIdQuery, GetTableroByIdQuery>();
            #endregion

            #region Servicios Tarea
            services.AddTransient<ICreateTareaCommand, CreateTareaCommand>();
            services.AddTransient<IDeleteTareaCommand, DeleteTareaCommand>();
            services.AddTransient<IUpdateTareaCommand, UpdateTareaCommand>();
            services.AddTransient<IUpdateTareaEstadoCommand, UpdateTareaEstadoCommand>();
            services.AddTransient<IGetAllTareasQuery, GetAllTareasQuery>();
            services.AddTransient<IGetTareaByIdQuery, GetTareaByIdQuery>();
            services.AddTransient<IGetTareasByTableroId, GetTareasByTableroId>();
            #endregion

            #region Servicios Usuario
            services.AddTransient<ICreateUsuarioCommand, CreateUsuarioCommand>();
            services.AddTransient<IDeleteUsuarioCommand, DeleteUsuarioCommand>();
            services.AddTransient<IUpdateUsuarioCommand, UpdateUsuarioCommand>();
            services.AddTransient<IGetUserByCredentialsQuery, GetUserByCredentialsQuery>();

            #region Validator
            services.AddScoped<IValidator<(string userName, string userPassword)>, GetUserByCrendetialsValidator>();
            #endregion
            #endregion

            #region Reportes
            services.AddTransient<IReporteTareasQuery, ReporteTareasQuery>();
            #endregion

            #region SeedData
            services.AddTransient<IGenerateSeedDataCommand, GenerateSeedDataCommand>();
            #endregion

            return services;
        }
    }
}

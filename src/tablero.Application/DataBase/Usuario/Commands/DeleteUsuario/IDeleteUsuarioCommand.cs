namespace tablero.Application.DataBase.Usuario.Commands.DeleteUsuario
{
    public interface IDeleteUsuarioCommand
    {
        Task<bool> Execute(int idUsuario);
    }
}

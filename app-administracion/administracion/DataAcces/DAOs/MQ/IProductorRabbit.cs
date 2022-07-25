namespace  administracion.DataAccess.rabbit
{
    
    public interface IProductorRabbit
    {
        public bool SendMessage(string instruccion, string contenido);
    }


}

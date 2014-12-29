namespace Douglas.Domain.Application.Commands
{
    public class CreatePerson
    {
        public string Nombres { get; set; }

        public string Apellido { get; set; }

        public CreatePerson(string nombres, string apellido)
        {
            Nombres = nombres;
            Apellido = apellido;
        }
    }
}
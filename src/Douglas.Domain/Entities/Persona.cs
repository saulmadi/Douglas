using System;

namespace Douglas.Domain.Entities
{
    public class Persona : IEntity
    {
        protected Persona()
        {
        }

        public Persona(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
        }

        public virtual string Nombre { get; protected set; }

        public virtual string Apellido { get; protected set; }

        public virtual string Telefono { get; protected set; }

        public virtual Guid Id { get; protected set; }
    }
}
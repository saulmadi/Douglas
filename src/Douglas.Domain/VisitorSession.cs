using System;
using AcklenAvenue.Commands;
using Douglas.Domain.Services;


namespace Douglas.Domain
{
    public class VisitorSession : IUserSession
    {
        #region IUserSession Members

        public Guid Id { get; private set; }

        #endregion
    }
}
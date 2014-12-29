using System;

namespace Douglas.Domain.Services
{
    public interface ITimeProvider
    {
        DateTime Now();
    }
}
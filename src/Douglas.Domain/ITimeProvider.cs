using System;

namespace Douglas.Domain
{
    public interface ITimeProvider
    {
        DateTime Now();
    }
}
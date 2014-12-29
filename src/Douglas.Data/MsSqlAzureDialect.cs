using NHibernate.Dialect;

namespace Douglas.Data
{
    public class MsSqlAzureDialect : MsSql2008Dialect
    {
        public override string PrimaryKeyString
        {
            get
            {
                return "primary key CLUSTERED";
            }
        }
    }
}
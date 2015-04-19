using System.Linq.Expressions;

namespace Microsoft.Linq.Dynamic
{
    public interface IIdentifierResolver
    {
        bool HasValue( string identifier );

        object GetValue( ParameterExpression iterator, string identifier );
    }
}

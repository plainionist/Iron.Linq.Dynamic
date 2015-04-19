using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;

namespace Microsoft.Linq.Dynamic
{
    public class MapIdentifierResolver : IIdentifierResolver
    {
        private IDictionary<string, object> identifierValueMap;
        private int posArgumentPos = 0;

        public MapIdentifierResolver()
            : this( new Dictionary<string, object>() )
        {
        }

        public MapIdentifierResolver( IDictionary<string, object> identifierValueMap )
        {
            this.identifierValueMap = identifierValueMap;
        }

        public void AddNamedParameter( ParameterExpression expr )
        {
            identifierValueMap[ expr.Name ] = expr;
        }

        public void AddPositionalArgument( object value )
        {
            var identifier = "@" + posArgumentPos.ToString( CultureInfo.InvariantCulture );
            identifierValueMap[ identifier ] = value;
            posArgumentPos++;
        }

        public void AddPositionalArguments( params object[] values )
        {
            foreach ( var value in values )
            {
                AddPositionalArgument( value );
            }
        }

        public object this[ string identifier ]
        {
            get
            {
                return identifierValueMap[ identifier ];
            }
            set
            {
                identifierValueMap[ identifier ] = value;
            }
        }

        public bool HasValue( string identifier )
        {
            return identifierValueMap.ContainsKey( identifier );
        }

        public object GetValue( ParameterExpression iterator, string identifier )
        {
            return identifierValueMap[ identifier ];
        }
    }
}

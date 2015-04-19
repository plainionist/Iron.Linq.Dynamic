//Copyright (C) Microsoft Corporation.  All rights reserved.

using System.Linq.Expressions;

namespace Microsoft.Linq.Dynamic
{
    internal class DynamicOrdering
    {
        public Expression Selector;
        public bool Ascending;
    }
}

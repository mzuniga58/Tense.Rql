using System.Collections.Generic;
using System.Linq.Expressions;

namespace Tense.Rql
{
    internal class MemberTranslation
    {
        public List<string> EntityMembers { get; set; }
        public Expression TranslationExpression { get; set; }

        public MemberTranslation(Expression e, List<string> l)
        {
            TranslationExpression = e;
            EntityMembers = l;  
        }
    }
}

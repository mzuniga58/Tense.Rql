using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace Tense.Rql
{
    internal class MemberExpressionVistor : ExpressionVisitor, IDisposable
    {
        private bool disposedValue;

        private List<string> DestinationNames { get; set; } = new List<string>();   
        private string SourceName { get; set; } = string.Empty;
        private Type TSource { get; set; }
        private Type TDestination { get; set; }
        private Expression? AssignExpression { get; set; }


        public Dictionary<string, MemberTranslation> MemberTranslations { get; set; }

        public MemberExpressionVistor(Type s, Type d)
        {
            TSource = s;
            TDestination = d;
            MemberTranslations = new Dictionary<string, MemberTranslation>();
            SourceName = string.Empty;
            DestinationNames = new List<string>();
        }

        public override Expression? Visit(Expression node)
        {
            if (node != null)
            {
                switch ( node.NodeType)
                {
                    case ExpressionType.Conditional:
                        {
                        }
                        break;

                    case ExpressionType.Equal:
                        {
                        }
                        break;

                    case ExpressionType.Constant:
                        {
                        }
                        break;

                    case ExpressionType.Default:
                        {
                        }
                        break;

                    case ExpressionType.Block:
                        {
                        }
                        break;

                    case ExpressionType.Parameter:
                        {
                        }
                        break;

                    case ExpressionType.Assign:
                        {
                            if (AssignExpression == null)
                                AssignExpression = node;
                        }
                        break;
                        
                    case ExpressionType.MemberAccess:
                        {
                            if (node is MemberExpression e)
                            {
                                if (e.Member.DeclaringType == TSource)
                                {
                                    SourceName = e.Member.Name;
                                }
                                else if (e.Member.DeclaringType == TDestination)
                                {
                                    if (!DestinationNames.Contains(e.Member.Name))
                                        DestinationNames.Add(e.Member.Name);
                                }
                            }
                        }
                        break;

                    case ExpressionType.Lambda:
                        {
                        }
                        break;

                    default:
                        break;
                }

                return base.Visit(node);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(SourceName))
                {
                    if (!MemberTranslations.ContainsKey(SourceName) && AssignExpression != null)
                        MemberTranslations.Add(SourceName, new MemberTranslation(AssignExpression, DestinationNames));

                    SourceName = string.Empty;
                    DestinationNames = new List<string>();
                    AssignExpression = null;
                }

                return null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

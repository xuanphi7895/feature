using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Core.Specifications {
    public class BaseSpecification<T> : ISpecification<T> {
        public BaseSpecification () { }

        public BaseSpecification (Expression<Func<T, bool>> criteria) {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>> ();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        protected void AddInclude (Expression<Func<T, object>> includeExpression) {
            Includes.Add (includeExpression);
        }

        protected void AddOrderBy (Expression<Func<T, object>> orderByExpression) {
            OrderBy = orderByExpression;
        }
        protected void AddOrderByDescending (Expression<Func<T, object>> orderByDescendingExpression) {
            OrderByDescending = orderByDescendingExpression;
        }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnable { get; private set; }
        /*
        Let say Page Number = PN and Number Of Records Per Page = NRP, then you need to use the following formula
        Result = DataSource.Skip((PN – 1) * NRP).Take(NRP)
        */
        protected void ApplyPaging (int skip, int take) {
            Skip = skip;
            Take = take;
            IsPagingEnable = true;
        }

    }
}
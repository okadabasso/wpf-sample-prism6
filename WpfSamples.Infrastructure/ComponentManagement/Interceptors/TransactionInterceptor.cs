using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using System.Diagnostics;
using System.Reflection;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using System.Data.Entity;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;

namespace WpfSamples.Infrastructure.ComponentManagement.Interceptors
{
    public class TransactionInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (!InterceptionEnabled(invocation))
            {
                invocation.Proceed();
                return;
            }

            var logger = LogManager.GetLogger(invocation.TargetType.FullName);
            this.Trace("transaction start", invocation, logger);
            var db = GetDbContext(invocation);
            using (var transaction = db.Database.BeginTransaction())
            {
                invocation.Proceed();
                transaction.Commit();
            }
            this.Trace("transaction end", invocation, logger);


        }
        private bool InterceptionEnabled(IInvocation invocation)
        {
            if (invocation.MethodInvocationTarget.GetCustomAttribute<TransactionMethodAttribute>() == null)
            {
                return false;
            }
            return true;
        }
        static readonly ConcurrentDictionary<Type, Func<object, DbContext>> DbContextGetFunctions = new ConcurrentDictionary<Type, Func<object, DbContext>>();
        DbContext GetDbContext(IInvocation invocation)
        {
            var function = DbContextGetFunctions.GetOrAdd(invocation.TargetType, t => {
                var property = t.GetProperties().Where(x => x.PropertyType.IsSubclassOf(typeof(DbContext))).FirstOrDefault();
                var parameter = Expression.Parameter(typeof(object), "component");
                var convert = Expression.Convert(parameter, t);
                var propertyAccess = Expression.Property(convert, property);
                var lambda = Expression.Lambda<Func<object, DbContext>>(propertyAccess, parameter);

                return lambda.Compile();
            });

            return function(invocation.InvocationTarget);

        }
    }
}

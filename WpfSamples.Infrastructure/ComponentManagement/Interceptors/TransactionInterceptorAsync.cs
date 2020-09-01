using Castle.DynamicProxy;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfSamples.Infrastructure.ComponentManagement.Attributes;
using WpfSamples.Infrastructure.Logging;

namespace WpfSamples.Infrastructure.ComponentManagement.Interceptors
{
    public class TransactionInterceptorAsync : IAsyncInterceptor
    {

        public TransactionInterceptorAsync()
        {
        }
        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            var logger = LogManager.GetLogger(invocation.TargetType.FullName);
            try
            {
                if (!InterceptEnabled(invocation))
                {
                    invocation.Proceed();
                    return;
                }

                var context = GetDbContext(invocation);
                using (var transaction = context.Database.BeginTransaction())
                {
                    this.Trace("transaction start", invocation, logger);
                    invocation.Proceed();
                    transaction.Commit();
                    this.Trace("transaction end", invocation, logger);
                }
            }
            catch (Exception ex)
            {
                this.Trace("transaction failed", invocation, logger);
                logger.ErrorRecursive(ex);
                throw;

            }
        }
        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            var logger = LogManager.GetLogger(invocation.TargetType.FullName);
            try
            {
                if (!InterceptEnabled(invocation))
                {
                    await Invoke(invocation);
                    return;
                }

                var context = GetDbContext(invocation);
                using (var transaction = context.Database.BeginTransaction())
                {
                    this.Trace("transaction start", invocation, logger);
                    await Invoke(invocation);
                    transaction.Commit();
                    this.Trace("transaction end", invocation, logger);
                }

            }
            catch (Exception ex)
            {
                this.Trace("transaction failed", invocation, logger);
                logger.ErrorRecursive(ex);
                throw;

            }
        }


        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {

            var logger = LogManager.GetLogger(invocation.TargetType.FullName);
            try
            {

                if (!InterceptEnabled(invocation))
                {
                    return await InvokeAsync<TResult>(invocation);
                }

                var context = GetDbContext(invocation);
                using (var transaction = context.Database.BeginTransaction())
                {
                    this.Trace("transaction start", invocation, logger);
                    TResult result = await InvokeAsync<TResult>(invocation);

                    transaction.Commit();
                    this.Trace("transaction end", invocation, logger);
                    return result;
                }
            }
            catch (Exception ex)
            {
                this.Trace("transaction failed", invocation, logger);
                logger.ErrorRecursive(ex);
                throw;

            }
        }
        private bool InterceptEnabled(IInvocation invocation)
        {
            if (invocation.TargetType.GetCustomAttribute<DependencyObjectAttribute>() == null)
            {
                return false;
            }
            if (invocation.MethodInvocationTarget.GetCustomAttribute<TransactionMethodAttribute>() == null)
            {
                return false;
            }
            return true;
        }
        private async Task Invoke(IInvocation invocation)
        {
            invocation.Proceed();
            var task = (Task)invocation.ReturnValue;
            await task;
        }

        private async Task<TResult> InvokeAsync<TResult>(IInvocation invocation)
        {
            invocation.Proceed();
            var task = (Task<TResult>)invocation.ReturnValue;
            TResult result = await task;
            return result;
        }

        static readonly ConcurrentDictionary<Type, Func<object, DbContext>> DbContextGetters = new ConcurrentDictionary<Type, Func<object, DbContext>>();
        DbContext GetDbContext(IInvocation invocation)
        {
            var getter = DbContextGetters.GetOrAdd(invocation.TargetType, t => {
                var property = t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.PropertyType.IsSubclassOf(typeof(DbContext))).FirstOrDefault();
                var parameter = Expression.Parameter(typeof(object), "component");
                var convert = Expression.Convert(parameter, t);
                var propertyAccess = Expression.Property(convert, property);
                var lambda = Expression.Lambda<Func<object, DbContext>>(propertyAccess, parameter);

                return lambda.Compile();
            });

            return getter(invocation.InvocationTarget);

        }
    }
}

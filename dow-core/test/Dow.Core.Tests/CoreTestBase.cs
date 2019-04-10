using System;
using System.Threading.Tasks;
using Abp.TestBase;
using Dow.Core.EntityFrameworkCore;
using Dow.Core.Tests.TestDatas;

namespace Dow.Core.Tests
{
    public class CoreTestBase : AbpIntegratedTestBase<CoreTestModule>
    {
        public CoreTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<CoreDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<CoreDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<CoreDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<CoreDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<CoreDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<CoreDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<CoreDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<CoreDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}

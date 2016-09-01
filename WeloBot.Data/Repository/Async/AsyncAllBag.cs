using System;
using System.Threading.Tasks;

namespace WeloBot.Data.Repository.Async
{
    internal class AsyncAllBag : IDisposable
    {
        private TaskScheduler _scheduler;

         public AsyncAllBag( Int32 maxConurrentTasks = 0 )
        {
            _scheduler = maxConurrentTasks <= 0 ? TaskScheduler.Current : new LimitedConcurrencyLevelTaskScheduler( maxConurrentTasks );
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
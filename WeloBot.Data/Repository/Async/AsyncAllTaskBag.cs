using System;
using System.Threading.Tasks;

namespace WeloBot.Data.Repository.Async
{
     internal class AsyncAllTaskBag
    {
          private AsyncAllTaskBag( )
        {
            Factory = Task.Factory;
        }

        public static readonly Lazy<AsyncAllTaskBag> Instance = new Lazy<AsyncAllTaskBag>( ( ) => new AsyncAllTaskBag( ) );

         public TaskFactory Factory { get; set; }
    }
}
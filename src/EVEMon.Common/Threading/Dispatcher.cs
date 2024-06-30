using EVEMon.Common.Scheduling;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EVEMon.Common.Threading
{
    public static class Dispatcher
    {
        private static TaskScheduler s_mainThreadScheduler;
        private static TaskFactory s_mainTaskFactory;
        private static Timer s_oneSecondTimer;

        private static readonly TimeSpan oneSecond = TimeSpan.FromSeconds(1);

        /// <summary>
        /// Starts the dispatcher on the main thread.
        /// </summary>
        /// <param name="thread">The thread.</param>
        /// <remarks>
        /// If the method has already been called previously, this new call will silently fail.
        /// </remarks>
        internal static void Run(TaskScheduler scheduler)
        {
            s_mainThreadScheduler = scheduler;
            s_mainTaskFactory = new TaskFactory(s_mainThreadScheduler);

            var e = new AutoResetEvent(false);
            s_oneSecondTimer = new Timer(OneSecondTickTimer_Tick, e, oneSecond, oneSecond);
        }

        /// <summary>
        /// Shutdowns the dispatcher.
        /// </summary>
        internal static void Shutdown()
        {
            if (s_oneSecondTimer == null)
                return;

            s_oneSecondTimer.Dispose();
            s_oneSecondTimer = null;
        }

        /// <summary>
        /// Invoke the provided delegate on the underlying actor and wait for completion.
        /// </summary>
        /// <param name="action">The action to invoke</param>
        public static void Invoke(Action action)
        {
            if (s_mainTaskFactory == null)
                action.Invoke();
            else
                s_mainTaskFactory.StartNew(action).Wait();            
        }

        /// <summary>
        /// Schedule an action to invoke on the actor, by specifying the time it will be executed.
        /// </summary>
        /// <param name="time">The time at which the action will be executed.</param>
        /// <param name="action">The action to execute.</param>
        public static void Schedule(TimeSpan time, Action action)
        {;
            //timer.Tick += (sender, args) =>
            //{
            //    timer.Stop();
            //    Invoke(action);
            //};

            Timer timer = null;

            timer = new Timer(_ =>
            {
                Invoke(action);
                timer.Dispose();
            }, null, time, TimeSpan.FromMilliseconds(-1));
        }
        private static void OneSecondTickTimer_Tick(object sender)
        {
            EveMonClient.UpdateOnOneSecondTick();
        }
    }
}

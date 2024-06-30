using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using ThreadDispatcher = System.Windows.Threading.Dispatcher;

namespace EVEMon.Common.Threading
{
    public static class Dispatcher
    {
        private static ThreadDispatcher s_mainThreadDispather;
        private static TaskScheduler s_mainThreadScheduler;
        private static TaskFactory s_mainThreadFactory;

        private static Timer s_oneSecondTimer;

        /// <summary>
        /// Starts the dispatcher on the main thread.
        /// </summary>
        /// <param name="thread">The thread.</param>
        /// <remarks>
        /// If the method has already been called previously, this new call will silently fail.
        /// </remarks>
        internal static void Run(Thread thread, TaskScheduler scheduler)
        {
            if (s_mainThreadDispather != null || s_mainThreadScheduler != null)
                return;

            s_mainThreadDispather = ThreadDispatcher.FromThread(thread) ?? ThreadDispatcher.CurrentDispatcher;
            s_mainThreadScheduler = scheduler;
            s_mainThreadFactory = new TaskFactory(s_mainThreadScheduler);

            var e = new AutoResetEvent(false);
            s_oneSecondTimer = new Timer(OneSecondTickTimer_Tick, e, 0, 1000);
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
            if (s_mainThreadDispather == null || s_mainThreadDispather.CheckAccess())
                action.Invoke();

            else
                s_mainThreadFactory.StartNew(action).Wait();                
        }

        /// <summary>
        /// Schedule an action to invoke on the actor, by specifying the time it will be executed.
        /// </summary>
        /// <param name="time">The time at which the action will be executed.</param>
        /// <param name="action">The action to execute.</param>
        public static void Schedule(TimeSpan time, Action action)
        {

            DispatcherTimer timer = new DispatcherTimer { Interval = time };
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                Invoke(action);
            };
            timer.Start();
        }

        /// <summary>
        /// Occurs on every second, when the timer ticks.
        /// </summary>
        //
        private static void OneSecondTickTimer_Tick(object sender, EventArgs e)
        {
            EveMonClient.UpdateOnOneSecondTick();
        }

        private static void OneSecondTickTimer_Tick(object sender)
        {
            EveMonClient.UpdateOnOneSecondTick();
        }
    }
}

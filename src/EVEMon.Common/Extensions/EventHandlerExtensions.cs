using System;
using System.ComponentModel;
using System.Linq;

namespace EVEMon.Common.Extensions
{
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Helper method to fire events in a thread safe manner.
        /// </summary>
        /// <param name="eventHandler">The <see cref="System.EventHandler" /> for the event to be raised.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">An <see cref="System.EventArgs" /> to be passed with the event invocation.</param>
        /// <remarks>
        /// Checks whether subscribers implement <see cref="System.ComponentModel.ISynchronizeInvoke" /> to ensure we raise the
        /// event on the correct thread.
        /// </remarks>
        public static void ThreadSafeInvoke(this EventHandler eventHandler, object sender, EventArgs e)
        {
            // Make sure we have some subscribers
            if (eventHandler == null)
                return;

            // Get each subscriber in turn
            foreach (EventHandler handler in eventHandler.GetInvocationList().Cast<EventHandler>())
            {
                // Get the object containing the subscribing method
                // If the target doesn't implement ISyncronizeInvoke, this will be null
                ISynchronizeInvoke sync = handler.Target as ISynchronizeInvoke;

                // Check if our target requires an Invoke
                if (sync != null && sync.InvokeRequired)
                {
                    // Yes it does, so invoke the handler using the target's BeginInvoke method, but wait for it to finish
                    // This is preferable to using Invoke so that if an exception is thrown its presented
                    // in the context of the handler, not the current thread
                    IAsyncResult result = sync.BeginInvoke(handler, new[] { sender, e });

                    try
                    {
                        sync.EndInvoke(result);
                    }
                    catch (ObjectDisposedException)
                    {
                        // Ignore, already cleaned up -- code was likely changed to use `using`.
                    }

                    continue;
                }

                // No it doesn't, so invoke the handler directly
                handler.Invoke(sender, e);
            }
        }

        /// <summary>
        /// Helper method to fire events in a thread safe manner.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler">The <see cref="System.EventHandler" /> for the event to be raised.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">An <see cref="System.EventArgs" /> to be passed with the event invocation.</param>
        /// <remarks>
        /// Checks whether subscribers implement <see cref="System.ComponentModel.ISynchronizeInvoke" /> to ensure we raise the
        /// event on the correct thread.
        /// </remarks>
        public static void ThreadSafeInvoke<T>(this EventHandler<T> eventHandler, object sender, T e) where T : EventArgs
        {
            // Make sure we have some subscribers
            if (eventHandler == null)
                return;

            // Get each subscriber in turn
            foreach (EventHandler<T> handler in eventHandler.GetInvocationList().Cast<EventHandler<T>>())
            {
                // Get the object containing the subscribing method
                // If the target doesn't implement ISyncronizeInvoke, this will be null
                ISynchronizeInvoke sync = handler.Target as ISynchronizeInvoke;

                // Check if our target requires an Invoke
                if (sync != null && sync.InvokeRequired)
                {
                    // Yes it does, so invoke the handler using the target's BeginInvoke method, but wait for it to finish
                    // This is preferable to using Invoke so that if an exception is thrown its presented
                    // in the context of the handler, not the current thread
                    IAsyncResult result = sync.BeginInvoke(handler, new[] { sender, e });
                    sync.EndInvoke(result);
                    continue;
                }

                // No it doesn't, so invoke the handler directly
                handler(sender, e);
            }
        }
    }
}

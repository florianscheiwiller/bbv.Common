//-------------------------------------------------------------------------------
// <copyright file="Publisher.cs" company="bbv Software Services AG">
//   Copyright (c) 2008-2011 bbv Software Services AG
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//   Contains software or other content adapted from 
//   Smart Client � Composite UI Application Block, 
//   2005 Microsoft Corporation. All rights reserved.
//-------------------------------------------------------------------------------

namespace bbv.Common.EventBroker.Handlers
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Handler that executes the subscription on the same thread the publisher is currently running (synchronous).
    /// </summary>
    public class Publisher : IHandler
    {
        /// <summary>
        /// Gets the kind of the handler, whether it is a synchronous or asynchronous handler.
        /// </summary>
        /// <value>The kind of the handler (synchronous or asynchronous).</value>
        public HandlerKind Kind
        {
            get { return HandlerKind.Synchronous; }
        }

        /// <summary>
        /// Initializes the handler.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="handlerMethod">Name of the handler method on the subscriber.</param>
        public void Initialize(object subscriber, MethodInfo handlerMethod)
        {
            // there is nothing to initialize
        }

        /// <summary>
        /// Executes the subscription synchronously on the same thread as the publisher is currently running.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <param name="subscriptionHandler">The subscription handler.</param>
        /// <returns>
        /// The exception that occurred during handling of the event. Null if no exception occurred
        /// </returns>
        public Exception Handle(object sender, EventArgs e, Delegate subscriptionHandler)
        {
            try
            {
                subscriptionHandler.DynamicInvoke(sender, e);
            }
            catch (TargetInvocationException ex)
            {
                return ex.InnerException;
            }

            return null;
        }
    }
}
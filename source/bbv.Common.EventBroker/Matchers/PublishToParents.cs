//-------------------------------------------------------------------------------
// <copyright file="PublishToParents.cs" company="bbv Software Services AG">
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
//-------------------------------------------------------------------------------

namespace bbv.Common.EventBroker.Matchers
{
    using System;
    using Internals;

    /// <summary>
    /// Matcher for publications to parents only.
    /// </summary>
    public class PublishToParents : IPublicationMatcher
    {
        /// <summary>
        /// Returns whether the publication and subscription match and the event published by the
        /// publisher will be relayed to the subscriber.
        /// <para>
        /// This is the case if the name of the subscriber is a prefix to the name of the publisher.
        /// </para>
        /// </summary>
        /// <param name="publication">The publication.</param>
        /// <param name="subscription">The subscription.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <returns><code>true</code> if event has to be sent to the subscriber.</returns>
        public bool Match(IPublication publication, ISubscription subscription, EventArgs e)
        {
            object publisher = publication.Publisher;
            object subscriber = subscription.Subscriber;

            string publisherName = publisher is INamedItem ? ((INamedItem)publisher).EventBrokerItemName : string.Empty;
            string subscriberName = subscriber is INamedItem ? ((INamedItem)subscriber).EventBrokerItemName : string.Empty;
            
            return publisherName.StartsWith(subscriberName);
        }

        /// <summary>
        /// Describes this scope matcher.
        /// </summary>
        /// <param name="writer">The writer the description is written to.</param>
        public void DescribeTo(System.IO.TextWriter writer)
        {
            writer.Write("publisher name starts with subscriber name");
        }
    }
}
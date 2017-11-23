// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Azure.WebJobs.Script
{
    public class SlidingWindow<TItem>
    {
        private readonly TimeSpan _window;
        private List<Event> _events = new List<Event>();
        private object _syncLock = new object();

        public SlidingWindow(TimeSpan window)
        {
            _window = window;
        }

        public IEnumerable<TItem> GetEvents(Func<TItem, bool> filter)
        {
            lock (_syncLock)
            {
                // remove expired events
                _events.RemoveAll(p => (DateTime.Now - p.TimeStamp) > _window);

                return _events.Select(p => p.Item).Where(filter).ToList();
            }
        }

        public void AddEvent(TItem item)
        {
            var evt = new Event
            {
                TimeStamp = DateTime.Now,
                Item = item
            };
            lock (_syncLock)
            {
                _events.Add(evt);
            }
        }

        private class Event
        {
            public DateTime TimeStamp { get; set; }

            public TItem Item { get; set; }
        }
    }
}

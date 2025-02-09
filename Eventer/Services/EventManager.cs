using System;
using System.Collections.Generic;
using System.Linq;
using Eventer.Models;

namespace Eventer.Services
{
    public class EventManager
    {
        private List<Event> _events;
        private int _nextId;

        public EventManager()
        {
            _events = new List<Event>();
            _nextId = 1;
        }

        public Event AddEvent(string title, string description, DateTime dateTime, string location)
        {
            var newEvent = new Event(title, description, dateTime, location)
            {
                Id = _nextId++
            };
            _events.Add(newEvent);
            return newEvent;
        }

        public bool RemoveEvent(int id)
        {
            var eventToRemove = _events.FirstOrDefault(e => e.Id == id);
            if (eventToRemove != null)
            {
                return _events.Remove(eventToRemove);
            }
            return false;
        }

        public bool MarkAsCompleted(int id)
        {
            var eventToUpdate = _events.FirstOrDefault(e => e.Id == id);
            if (eventToUpdate != null)
            {
                eventToUpdate.IsCompleted = true;
                return true;
            }
            return false;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _events.OrderBy(e => e.DateTime);
        }

        public IEnumerable<Event> GetUpcomingEvents()
        {
            return _events.Where(e => !e.IsCompleted && e.DateTime > DateTime.Now)
                         .OrderBy(e => e.DateTime);
        }
    }
} 
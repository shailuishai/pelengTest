using System;

namespace Eventer.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public bool IsCompleted { get; set; }

        public Event(string title, string description, DateTime dateTime, string location)
        {
            Title = title;
            Description = description;
            DateTime = dateTime;
            Location = location;
            IsCompleted = false;
        }

        public override string ToString()
        {
            return $"[{(IsCompleted ? "X" : " ")}] {DateTime:g} - {Title} ({Location})\n    {Description}";
        }
    }
} 
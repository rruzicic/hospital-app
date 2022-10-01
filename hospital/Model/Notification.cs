using System;

namespace Model
{
    public class Notification
    {
        private string _username;
        private DateTime _startTime = DateTime.MinValue;
        private DateTime _endTime = DateTime.MinValue;
        private int _interval;
        private string _text;


        public Notification() { }
        public Notification(string username, string text)
        {
            _username = username;
            _text = text;
        }
        public Notification(string username, DateTime startTime, DateTime endTime, int interval, string text)
        {
            _username = username;
            _startTime = startTime;
            _endTime = endTime;
            _interval = interval;
            _text = text;
        }

        public Notification(string username, DateTime startTime, string text)
        {
            _username = username;
            _startTime = startTime;
            _text = text;
        }

        public string Username { get => _username; set => _username = value; }
        public DateTime StartTime { get => _startTime; set => _startTime = value; }
        public DateTime EndTime { get => _endTime; set => _endTime = value; }
        public int Interval { get => _interval; set => _interval = value; }
        public string Text { get => _text; set => _text = value; }
    }
}

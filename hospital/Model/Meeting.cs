using System;
using System.Collections.Generic;

namespace Model
{
    public class Meeting
    {
        private List<string> _doctors;
        private DateTime _date;
        private string _roomId;
        private string _meetingTopic;
        private int _id;

        public Meeting(List<string> doctors, DateTime date, string roomId, string meetingTopic)
        {
            Doctors = doctors;
            Date = date;
            Room = roomId;
            MeetingTopic = meetingTopic;
        }

        public List<string> Doctors { get => _doctors; set => _doctors = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public string Room { get => _roomId; set => _roomId = value; }
        public string MeetingTopic { get => _meetingTopic; set => _meetingTopic = value; }
        public int Id { get => _id; set => _id = value; }
    }
}

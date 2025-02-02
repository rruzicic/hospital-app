﻿using System.Collections.Generic;
using System.Linq;

namespace hospital.Model
{
    public enum PollType
    {
        HOSPITAL_POLL,
        DOCTOR_POLL
    }
    public class PollBlueprint
    {
        private PollType _type;
        private string _pollName;
        private string _username;
        private int _appointmentId;
        private List<PollCategory> _categories;

        public PollType Type { get => _type; set => _type = value; }
        public string PollName { get => _pollName; set => _pollName = value; }
        public string Username { get => _username; set => _username = value; }
        public int AppointmentId
        {
            get => _appointmentId;
            set
            {
                if (_type == PollType.DOCTOR_POLL)
                {
                    _appointmentId = value;
                }
            }
        }
        public List<PollCategory> Categories { get => _categories; set => _categories = value; }

        public PollCategory FindPollCategoryById(int categoryId)
        {
            return _categories.FirstOrDefault(c => c.Id == categoryId);
        }
    }
}

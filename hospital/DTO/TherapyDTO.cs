using System;

namespace hospital.DTO
{
    public class TherapyDTO
    {
        private readonly string _name;
        private readonly DateTime _date;
        public TherapyDTO(string name, DateTime date)
        {
            _name = name;
            _date = date;
        }

        public DateTime Date => _date;
        public string Name => _name;
    }
}

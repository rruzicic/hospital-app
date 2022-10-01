using hospital.Model;
using hospital.Repository;
using Model;
using System.Collections.Generic;

namespace hospital.Service
{

    public class TimeSchedulerService
    {

        private readonly TimeSchedulerRepository timeSchedulerRepository;

        public TimeSchedulerService(TimeSchedulerRepository timeSchedulerRepository)
        {
            this.timeSchedulerRepository = timeSchedulerRepository;
        }

        public List<TimeInterval> FindFreeTimeIntervals(Room room, int renovationDuration)
        {
            return timeSchedulerRepository.FindFreeTimeIntervals(room, renovationDuration);
        }

        public List<TimeInterval> FindRelocationIntervals(int relocationDuration)
        {
            return timeSchedulerRepository.FindRelocationIntervals(relocationDuration);
        }

        public List<TimeInterval> FindTimeIntervalsForMergingRooms(List<Room> rooms, int renovationDuration)
        {
            return timeSchedulerRepository.FindTimeIntervalsForMergingRooms(rooms, renovationDuration);
        }

        public List<TimeInterval> FindTimeIntervalsForSplitingRoom(Room room, int renovationDuration)
        {
            return timeSchedulerRepository.FindTimeIntervalsForSplitingRoom(room, renovationDuration);
        }

    }
}

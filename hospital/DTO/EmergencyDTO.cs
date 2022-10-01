using Model;

namespace DTO
{
    public class EmergencyDTO
    {
        private string _patientUsername;
        private Specialization _requiredSpecialization;
        private bool _isOperation;


        public EmergencyDTO(string patientUsername, Specialization requiredSpecialization, bool isOperation)
        {
            _patientUsername = patientUsername;
            _requiredSpecialization = requiredSpecialization;
            _isOperation = isOperation;
        }

        public string PatientUsername { get => _patientUsername; set => _patientUsername = value; }
        public Specialization RequiredSpecialization { get => _requiredSpecialization; set => _requiredSpecialization = value; }
        public bool IsOperation { get => _isOperation; set => _isOperation = value; }

    }
}

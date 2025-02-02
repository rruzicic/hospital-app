﻿using Controller;
using hospital.Controller;
using hospital.Repository;
using hospital.Service;
using Repository;
using Service;
using System;
using System.Threading;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace hospital
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IRoomRepository roomRepository;
        public IScheduledRelocationRepository scheduledRelocationRepository;
        public IScheduledBasicRenovationRepository scheduledBasicRenovationRepository;
        public IScheduledAdvancedRenovationRepository scheduledAdvancedRenovationRepository;
        public NotificationRepository notificationRepository;
        public RoomController roomController { get; set; }
        public PatientController patientController { get; set; }
        public AppointmentManagementController appointmentController { get; set; }
        public AvailableAppointmentController availableAppointmentController { get; set; }
        public NotificationController notificationController { get; set; }
        public MedicalRecordsController medicalRecordsController { get; set; }
        public DoctorController doctorController { get; set; }
        public UserController userController { get; set; }
        public OrderController orderController { get; set; }
        public MeetingController meetingController { get; set; }
        public ScheduledRelocationController scheduledRelocationController { get; set; }
        public ScheduledBasicRenovationController scheduledBasicRenovationController { get; set; }
        public ScheduledAdvancedRenovationController scheduledAdvancedRenovationController { get; set; }
        public MedicineController medicineController { get; set; }
        public VacationRequestController vacationRequestController { get; set; }
        public InvalidMedicineReportController invalidMedicineReportController { get; set; }
        public EmergencyController emergencyController { get; set; }
        public RecommendedAppointmentController recommendedAppointmentController { get; set; }

        public PollController pollBlueprintController { get; set; }

        public IngridientsController ingridientsController { get; set; }

        public Notifier Notifier { get; set; }

        private readonly Thread orderThread;
        public App()
        {
            // consider sorting App() so that it contains all repositories, file handlers, services and controllers IN THAT ORDER
            UserRepository userRepository = new UserRepository();
            UserService userService = new UserService(userRepository);
            userController = new UserController(userService);

            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);


            AppointmentRepository appointmentRepository = new AppointmentRepository();
            scheduledBasicRenovationRepository = new ScheduledBasicRenovationRepository();
            // TimeSchedulerService timeSchedulerService = new TimeSchedulerService(appointmentRepository, scheduledBasicRenovationRepository);

            scheduledBasicRenovationRepository = new ScheduledBasicRenovationRepository();
            scheduledAdvancedRenovationRepository = new ScheduledAdvancedRenovationRepository();
            TimeSchedulerRepository timeSchedulerRepository = new TimeSchedulerRepository(appointmentRepository, scheduledBasicRenovationRepository, scheduledAdvancedRenovationRepository);
            TimeSchedulerService timeSchedulerService = new TimeSchedulerService(timeSchedulerRepository);

            ScheduledBasicRenovationService scheduledBasicRenovationService = new ScheduledBasicRenovationService(scheduledBasicRenovationRepository, timeSchedulerService);
            scheduledBasicRenovationController = new ScheduledBasicRenovationController(scheduledBasicRenovationService);
            // ScheduledBasicRenovationService scheduledBasicRenovationService = new ScheduledBasicRenovationService(scheduledBasicRenovationRepository, timeSchedulerService);
            // scheduledBasicRenovationController = new ScheduledBasicRenovationController(scheduledBasicRenovationService);

            roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository, appointmentRepository, scheduledBasicRenovationService);
            roomController = new RoomController(roomService);

            PatientRepository patientRepository = new PatientRepository();
            MedicalRecordsRepository medicalRecordsRepository = new MedicalRecordsRepository();
            PatientService patientService = new PatientService(patientRepository, medicalRecordsRepository, userRepository);
            patientController = new PatientController(patientService, userService);


            OrderRepository orderRepository = new OrderRepository();
            OrderService orderService = new OrderService(orderRepository, roomRepository);
            orderController = new OrderController(orderService);

            NotificationRepository notificationRepository = new NotificationRepository();
            NotificationService notificationService = new NotificationService(notificationRepository);
            notificationController = new NotificationController(notificationService);


            AppointmentManagementService appointmentService = new AppointmentManagementService(appointmentRepository);
            MeetingRepository meetingRepository = new MeetingRepository();
            MeetingService meetingService = new MeetingService(meetingRepository);
            meetingController = new MeetingController(meetingService, notificationService);

            appointmentController = new AppointmentManagementController(appointmentService);
            AvailableAppointmentService availableAppointmentService = new AvailableAppointmentService(appointmentService, doctorRepository, meetingService);
            availableAppointmentController = new AvailableAppointmentController(availableAppointmentService);

            MedicalRecordsService medicalRecordsService = new MedicalRecordsService(medicalRecordsRepository);
            medicalRecordsController = new MedicalRecordsController(medicalRecordsService);


            ScheduledAdvancedRenovationService scheduledAdvancedRenovationService = new ScheduledAdvancedRenovationService(scheduledAdvancedRenovationRepository, timeSchedulerService, roomService);
            scheduledAdvancedRenovationController = new ScheduledAdvancedRenovationController(scheduledAdvancedRenovationService);

            scheduledRelocationRepository = new ScheduledRelocationRepository();
            ScheduledRelocationService scheduledRelocationService = new ScheduledRelocationService(scheduledRelocationRepository, timeSchedulerService);
            scheduledRelocationController = new ScheduledRelocationController(scheduledRelocationService);

            MedicineRepository medicineRepository = new MedicineRepository();
            MedicineService medicineService = new MedicineService(medicineRepository);
            medicineController = new MedicineController(medicineService);

            VacationRequestRepository vacationRequestRepository = new VacationRequestRepository();
            VacationRequestService vacationRequestService = new VacationRequestService(vacationRequestRepository, notificationRepository);
            vacationRequestController = new VacationRequestController(vacationRequestService);


            InvalidMedicineReportRepository invalidMedicineReportRepository = new InvalidMedicineReportRepository(medicineRepository);
            InvalidMedicineReportService invalidMedicineReportService = new InvalidMedicineReportService(invalidMedicineReportRepository);
            invalidMedicineReportController = new InvalidMedicineReportController(invalidMedicineReportService);


            RecommendedAppointmentService recommendedAppointmentService = new RecommendedAppointmentService(appointmentService, notificationRepository, doctorRepository, availableAppointmentService);
            recommendedAppointmentController = new RecommendedAppointmentController(recommendedAppointmentService);

            EmergencyService emergencyService = new EmergencyService(appointmentService, notificationRepository, doctorService, roomService, recommendedAppointmentService, availableAppointmentService);
            emergencyController = new EmergencyController(emergencyService);


            PollBlueprintRepository pollBlueprintRepository = new PollBlueprintRepository();
            PollResultRepository pollResultRepository = new PollResultRepository(appointmentRepository);
            PollService pollBlueprintService = new PollService(pollBlueprintRepository, pollResultRepository);
            pollBlueprintController = new PollController(pollBlueprintService);

            IngridientsRepository ingridientsRepository = new IngridientsRepository();
            IngridientsService ingridientsService = new IngridientsService(ingridientsRepository);
            ingridientsController = new IngridientsController(ingridientsService);

            roomRepository.LoadRoomData();
            scheduledRelocationRepository.LoadRelocationData();
            scheduledBasicRenovationRepository.LoadRenovationData();
            medicineRepository.LoadMedicineData();


            scheduledAdvancedRenovationRepository.LoadRenovationData();
            ingridientsRepository.LoadIngridientsData();

            orderThread = new Thread(orderService.OrderTracker);
            orderThread.Start();

            Notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
            SystemTimer systemTimer = new SystemTimer(scheduledAdvancedRenovationService, scheduledBasicRenovationService, scheduledRelocationService);
        }

        private void App_Closing(object sender, ExitEventArgs e)
        {
            roomRepository.WriteRoomData();
            scheduledRelocationRepository.WriteRelocationData();
            scheduledBasicRenovationRepository.WriteRenovationData();
            orderThread.Abort();
            scheduledAdvancedRenovationRepository.WriteRenovationData();
        }
    }
}

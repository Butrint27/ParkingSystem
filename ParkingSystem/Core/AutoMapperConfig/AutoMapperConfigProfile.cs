using AutoMapper;
using ParkingSystem.Core.Entities;
using ParkingSystem;
using ParkingSystem.Core.Dtos.User;
using ParkingSystem.Core.Dtos.UserProfile;
using ParkingSystem.Core.Dtos.ParkingSpot;
using ParkingSystem.Core.Dtos.Authentication;
using ParkingSystem.Core.Dtos.PaymentMethod;
using ParkingSystem.Core.Dtos.Invoice;
using ParkingSystem.Core.Dtos;
using ParkingSystem.Core.Dtos.Reservation;
using ParkingSystem.Core.Dtos.ParkingReservationManager;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
 
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();

        CreateMap<AuthenticationDto, Authentication>();
        CreateMap<Authentication, AuthenticationDto>();
      
        CreateMap<UserProfileDto, UserProfile>();
        CreateMap<UserProfile, UserProfileDto>();

   
        CreateMap<PaymentDto, Payment>();
        CreateMap<Payment, PaymentDto>();

        CreateMap<PaymentMethodDto, PaymentMethod>();
        CreateMap<PaymentMethod, PaymentMethodDto>();

        CreateMap<InvoiceDto, Invoice>();
        CreateMap<Invoice, InvoiceDto>();

        CreateMap<ParkingSpaceManagerDto, ParkingSpaceManager>();
        CreateMap<ParkingSpaceManager, ParkingSpaceManagerDto>();

        CreateMap<ParkingSpaceDto, ParkingSpace>();
        CreateMap<ParkingSpace, ParkingSpaceDto>();

        CreateMap<ParkingSpotDto, ParkingSpot>();
        CreateMap<ParkingSpot, ParkingSpotDto>();

        CreateMap<ReservationDto, Reservation>();
        CreateMap<Reservation, ReservationDto>();

        CreateMap<ParkingReservationManagerDto, ParkingReservationManager>();
        CreateMap<ParkingReservationManager, ParkingReservationManagerDto>();
      
        CreateMap<AvailabilityMonitorDto, AvailabilityMonitor>();
        CreateMap<AvailabilityMonitor, AvailabilityMonitorDto>();
            
    }
}
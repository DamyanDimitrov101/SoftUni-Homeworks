using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;
using CarDealer.DTO;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {

            this.CreateMap<Customer, GetOrderedCustomersDTO>()
                .ForMember(c => c.IsYoungDriver, y
                  => y.MapFrom(x => x.IsYoungDriver))
                .ForMember(c=>c.BirthDate,y
                  =>y.MapFrom(x=>x.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(c=>c.Date, y
                  =>y.MapFrom(x=>x.BirthDate));

            this.CreateMap<Car, GetCarsFromMakeToyotaDTO>();

            this.CreateMap<Supplier, GetLocalSuppliersDTO>()
                .ForMember(s => s.PartsCount, y
                  => y.MapFrom(x => x.Parts.Count));


            
            this.CreateMap<Sale, GetSalesWithAppliedDiscountDTO>()
                .ForMember(s => s.customerName, y
                  => y.MapFrom(x => x.Customer.Name))
                .ForMember(s => s.DiscountToDec, y
                  => y.MapFrom(x => x.Discount))
                .ForMember(s => s.priceToDec, y
                  => y.MapFrom(x => x.Car.PartCars.Sum(pc => pc.Part.Price)))
                .ForMember(s => s.priceWithDiscountToDec, y
                  => y.MapFrom(x => (x.Car.PartCars.Sum(pc => pc.Part.Price))));


            this.CreateMap<Car, CarDTO>();
        }
    }
}

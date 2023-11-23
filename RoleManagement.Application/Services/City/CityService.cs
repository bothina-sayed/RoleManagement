using FluentValidation;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using RoleManagement.Application.Utils;
using RoleManagement.Application.ViewModels;
using RoleManagement.Domain.Abstractions;
using RoleManagement.Domain.Models;
using RoleManagement.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Application.Services
{
    internal class CityService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<City> _logger;
        private readonly IGenericRepository<City> _cityRepo;

        public CityService(IMapper mapper, ILogger<City> logger, IGenericRepository<City> cityRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _cityRepo = cityRepo;
        }

        public async Task<ResponseModel<string>> Create(CreateCityViewModel viewModel)
        {
            try
            {
                var role = _mapper.Map<City>(viewModel);
                await _cityRepo.Add(role);
                await _cityRepo.Save();
                return ResponseModel<string>.Success();

            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return ResponseModel<string>.Error();
        }
        public async Task<ResponseModel<string>> Delete(int id)
        {
            try
            {
                var city = await _cityRepo.GetById(id);

                if (city == null)
                    return ResponseModel<string>.Error("not found");


                _cityRepo.Delete(city);

                await _cityRepo.Save();

                return ResponseModel<string>.Success();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }
            return ResponseModel<string>.Error();
        }
        public ResponseModel<List<CityViewModel>> Get()
        {
            try
            {
                var result = _cityRepo.Get();

                var cities = _mapper.Map<List<CityViewModel>>(result);

                return ResponseModel<List<CityViewModel>>.Success(cities);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }
            return ResponseModel<List<CityViewModel>>.Error();

        }
        public async Task<ResponseModel<CityViewModel>> Update(UpdateCityViewModel viewModel)
        {
            try
            {

                var city = await _cityRepo.GetById(viewModel.Id);
                city!.Name = viewModel.Name;

                _cityRepo.Update(city);

                await _cityRepo.Save();

                return ResponseModel<CityViewModel>.Success(_mapper.Map<CityViewModel>(city));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }
            return ResponseModel<CityViewModel>.Error();
        }
    }
}

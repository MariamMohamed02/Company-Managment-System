using AutoMapper;
using Company.Project.DataLayer.Models;
using Company.Project.PresentationLayer.DTOs;

namespace Company.Project.PresentationLayer.Mapping
{
    public class EmployeeProfile:Profile 
    {
        public EmployeeProfile()
        {
            // map from createemployeedto TO employee and no the opposite
            CreateMap<CreateEmployeeDto, Employee>();


        }

    }
}

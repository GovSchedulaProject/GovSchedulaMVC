using System.Collections.Generic; // Make sure List is included

namespace GovSchedulaWeb.Models
{
    public class ServiceViewModel
    {
        // Add '?' to make string properties nullable
        public string? Name { get; set; } 
        public string? Link { get; set; } 
    }

    public class DepartmentViewModel
    {
        // Add '?' to make string properties nullable
        public string? Id { get; set; } 
        public string? Name { get; set; } 
        public string? Hours { get; set; } 
        public string? LogoUrl { get; set; } 

        // Initialize List properties with '= new()'
        public List<ServiceViewModel> Services { get; set; } = new List<ServiceViewModel>(); 
    }

    public class DepartmentSelectViewModel
    {
        // Initialize List properties with '= new()'
        public List<DepartmentViewModel> Departments { get; set; } = new List<DepartmentViewModel>();
    }
}
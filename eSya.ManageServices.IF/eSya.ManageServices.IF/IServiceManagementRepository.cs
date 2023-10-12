using eSya.ManageServices.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ManageServices.IF
{
    public interface IServiceManagementRepository
    {
        Task<List<DO_ServiceType>> GetServiceTypes();
        Task<List<DO_ServiceGroup>> GetServiceGroups();
        Task<List<DO_ServiceClass>> GetServiceClasses();
        Task<List<DO_ServiceCode>> GetServiceCodes();
        Task<DO_ServiceCode> GetServiceCodeByID(int ServiceID);
        Task<DO_ReturnParameter> AddOrUpdateServiceCode(DO_ServiceCode obj);
        Task<List<DO_ServiceCode>> GetServiceBusinessLink(int businessKey);
        Task<List<DO_ServiceBusinessLink>> GetBusinessLocationServices(int businessKey);
        Task<DO_ReturnParameter> AddOrUpdateBusinessLocationServices(List<DO_ServiceBusinessLink> obj);
        Task<List<DO_ServiceBusinessLink>> GetServiceBusinessLocations(int ServiceId);
        Task<DO_ReturnParameter> UpdateServiceBusinessLocations(List<DO_ServiceBusinessLink> obj);
    }
}

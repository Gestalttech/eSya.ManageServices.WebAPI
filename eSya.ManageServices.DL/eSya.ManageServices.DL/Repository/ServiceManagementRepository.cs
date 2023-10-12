using eSya.ManageServices.DL.Entities;
using eSya.ManageServices.DO;
using eSya.ManageServices.IF;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ManageServices.DL.Repository
{
    public class ServiceManagementRepository: IServiceManagementRepository
    {
        private readonly IStringLocalizer<ServiceManagementRepository> _localizer;
        public ServiceManagementRepository(IStringLocalizer<ServiceManagementRepository> localizer)
        {
            _localizer = localizer;
        }
        public async Task<List<DO_ServiceType>> GetServiceTypes()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEssrties
                                 .Select(x => new DO_ServiceType
                                 {
                                     ServiceTypeId = x.ServiceTypeId,
                                     ServiceTypeDesc = x.ServiceTypeDesc,
                                     PrintSequence = x.PrintSequence,
                                     ActiveStatus = x.ActiveStatus
                                 }
                        ).OrderBy(o => o.PrintSequence).ToListAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ServiceGroup>> GetServiceGroups()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEssrgrs
                                 .Select(x => new DO_ServiceGroup
                                 {
                                     ServiceTypeId = x.ServiceTypeId,
                                     ServiceGroupId = x.ServiceGroupId,
                                     ServiceGroupDesc = x.ServiceGroupDesc,
                                     ServiceCriteria = x.ServiceCriteria,
                                     PrintSequence = x.PrintSequence,
                                     ActiveStatus = x.ActiveStatus
                                 }
                        ).OrderBy(g => g.PrintSequence).ToListAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ServiceClass>> GetServiceClasses()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEssrcls
                                 .Select(x => new DO_ServiceClass
                                 {
                                     ServiceGroupId = x.ServiceGroupId,
                                     ServiceClassId = x.ServiceClassId,
                                     ServiceClassDesc = x.ServiceClassDesc,
                                     IsBaseRateApplicable = x.IsBaseRateApplicable,
                                     ParentId = x.ParentId,
                                     PrintSequence = x.PrintSequence,
                                     ActiveStatus = x.ActiveStatus
                                 }
                        ).OrderBy(g => g.PrintSequence).ToListAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ServiceCode>> GetServiceCodes()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEssrms
                                 .Select(x => new DO_ServiceCode
                                 {
                                     ServiceId = x.ServiceId,
                                     // ServiceTypeId=x.ServiceTypeId,
                                     // ServiceGroupId = x.ServiceGroupId,
                                     ServiceClassId = x.ServiceClassId,
                                     ServiceDesc = x.ServiceDesc,
                                     ServiceShortDesc = x.ServiceShortDesc,
                                     ActiveStatus = x.ActiveStatus
                                 }
                        ).ToListAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ServiceCode> GetServiceCodeByID(int ServiceID)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEssrms
                        .Where(i => i.ServiceId == ServiceID)
                                 .Select(x => new DO_ServiceCode
                                 {
                                     ServiceId = x.ServiceId,
                                     ServiceDesc = x.ServiceDesc,
                                     ServiceShortDesc = x.ServiceShortDesc,
                                     InternalServiceCode = x.InternalServiceCode,
                                     Gender = x.Gender,
                                     IsServiceBillable = x.IsServiceBillable,
                                     ActiveStatus = x.ActiveStatus,
                                     l_ServiceParameter = x.GtEspasms.Select(p => new DO_eSyaParameter
                                     {
                                         ParameterID = p.ParameterId,
                                         ParmAction = p.ParmAction,
                                         ParmPerc = p.ParmPerc,
                                         ParmValue = p.ParmValue,
                                     }).ToList()
                                 }
                        ).FirstOrDefaultAsync();

                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> AddOrUpdateServiceCode(DO_ServiceCode obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (obj.ServiceId == 0)
                        {
                            var RecordExist = db.GtEssrms.Where(w => w.ServiceDesc == obj.ServiceDesc || (w.ServiceShortDesc == obj.ServiceShortDesc && w.ServiceShortDesc != null && w.ServiceShortDesc != "")).Count();
                            if (RecordExist > 0)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0151", Message = string.Format(_localizer[name: "W0151"]) };
                            }
                            else
                            {
                                var internalcode = obj.InternalServiceCode;
                                var newServiceId = db.GtEssrms.Select(a => (int)a.ServiceId).DefaultIfEmpty().Max() + 1;
                                // If internal service code pattern is defined
                                var pattern = db.GtEssrcgs.Where(w => w.ServiceClassId == obj.ServiceClassId && w.ActiveStatus).FirstOrDefault();
                                if (pattern != null)
                                {
                                    var internalserId = db.GtEssrms.Where(w => w.ServiceClassId == obj.ServiceClassId).Count() + 1;
                                    string digits = (Math.Pow(10, pattern.IntSccode)).ToString();
                                    digits = digits + internalserId.ToString();
                                    digits = digits.Substring(digits.Length - pattern.IntSccode, pattern.IntSccode);
                                    internalcode = pattern.IntScpattern + digits;
                                }


                                var servicecode = new GtEssrm
                                {
                                    ServiceId = newServiceId,
                                    // ServiceTypeId = obj.ServiceTypeId,
                                    // ServiceGroupId = obj.ServiceGroupId,
                                    ServiceClassId = obj.ServiceClassId,
                                    ServiceDesc = obj.ServiceDesc,
                                    ServiceShortDesc = obj.ServiceShortDesc,
                                    Gender = obj.Gender,
                                    InternalServiceCode = internalcode,
                                    IsServiceBillable = obj.IsServiceBillable,
                                    ActiveStatus = obj.ActiveStatus,
                                    FormId = obj.FormId,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = obj.CreatedOn,
                                    CreatedTerminal = obj.TerminalID
                                };
                                db.GtEssrms.Add(servicecode);
                                foreach (DO_eSyaParameter sp in obj.l_ServiceParameter)
                                {
                                    var sParameter = new GtEspasm
                                    {
                                        ServiceId = newServiceId,
                                        ParameterId = sp.ParameterID,
                                        ParmPerc = sp.ParmPerc,
                                        ParmAction = sp.ParmAction,
                                        ParmValue = sp.ParmValue,
                                        ActiveStatus = sp.ActiveStatus,
                                        FormId = obj.FormId,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID,
                                    };
                                    db.GtEspasms.Add(sParameter);

                                }
                            }
                        }
                        else
                        {
                            var updatedServiceCode = db.GtEssrms.Where(w => w.ServiceId == obj.ServiceId).FirstOrDefault();
                            if (updatedServiceCode.ServiceDesc != obj.ServiceDesc)
                            {
                                var RecordExist = db.GtEssrms.Where(w => w.ServiceDesc == obj.ServiceDesc).Count();
                                if (RecordExist > 0)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0152", Message = string.Format(_localizer[name: "W0152"]) };
                                }
                            }
                            if (updatedServiceCode.ServiceShortDesc != obj.ServiceShortDesc)
                            {
                                var RecordExist = db.GtEssrms.Where(w => w.ServiceShortDesc == obj.ServiceShortDesc).Count();
                                if (RecordExist > 0)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0153", Message = string.Format(_localizer[name: "W0153"]) };
                                }
                            }
                            if (updatedServiceCode.InternalServiceCode != obj.InternalServiceCode)
                            {
                                var RecordExist = db.GtEssrms.Where(w => w.InternalServiceCode == obj.InternalServiceCode).Count();
                                if (RecordExist > 0)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0154", Message = string.Format(_localizer[name: "W0154"]) };
                                }
                            }
                            updatedServiceCode.ServiceDesc = obj.ServiceDesc;
                            updatedServiceCode.ServiceShortDesc = obj.ServiceShortDesc;
                            updatedServiceCode.Gender = obj.Gender;
                            updatedServiceCode.IsServiceBillable = obj.IsServiceBillable;
                            updatedServiceCode.InternalServiceCode = obj.InternalServiceCode;
                            updatedServiceCode.ActiveStatus = obj.ActiveStatus;
                            updatedServiceCode.ModifiedBy = obj.UserID;
                            updatedServiceCode.ModifiedOn = obj.CreatedOn;
                            updatedServiceCode.ModifiedTerminal = obj.TerminalID;

                            foreach (DO_eSyaParameter sp in obj.l_ServiceParameter)
                            {
                                var sPar = db.GtEspasms.Where(x => x.ServiceId == obj.ServiceId && x.ParameterId == sp.ParameterID).FirstOrDefault();
                                if (sPar != null)
                                {
                                    sPar.ParmAction = sp.ParmAction;
                                    sPar.ParmPerc = sp.ParmPerc;
                                    sPar.ParmValue = sp.ParmValue;
                                    sPar.ActiveStatus = obj.ActiveStatus;
                                    sPar.ModifiedBy = obj.UserID;
                                    sPar.ModifiedOn = System.DateTime.Now;
                                    sPar.ModifiedTerminal = obj.TerminalID;
                                }
                                else
                                {
                                    var sParameter = new GtEspasm
                                    {
                                        ServiceId = obj.ServiceId,
                                        ParameterId = sp.ParameterID,
                                        ParmPerc = sp.ParmPerc,
                                        ParmAction = sp.ParmAction,
                                        ParmValue = sp.ParmValue,
                                        ActiveStatus = sp.ActiveStatus,
                                        FormId = obj.FormId,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID,

                                    };
                                    db.GtEspasms.Add(sParameter);
                                }

                            }
                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<List<DO_ServiceCode>> GetServiceBusinessLink(int businessKey)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    //var result = db.GtEssrms
                    //    .GroupJoin(db.GtEssrbls.Where(w => w.BusinessKey == businessKey),
                    //    s => s.ServiceId,
                    //    b => b.ServiceId,
                    //    (s, b) => new { s, b = b.FirstOrDefault() })
                    //             .Select(x => new DO_ServiceCode
                    //             {
                    //                 ServiceId = x.s.ServiceId,
                    //                 ServiceClassId = x.s.ServiceClassId,
                    //                 ServiceDesc = x.s.ServiceDesc,
                    //                 ActiveStatus = x.s.ActiveStatus,
                    //                 BusinessLinkStatus = x.b != null ? x.b.ActiveStatus : false
                    //             }
                    //    ).ToListAsync();
                    //return await result;
                    var result = db.GtEssrms
                    .GroupJoin(db.GtEssrbls.Where(w => w.BusinessKey == businessKey),
                      s => s.ServiceId,
                      b => b.ServiceId,
                     (s, b) => new { s, b })
                    .SelectMany(z => z.b.DefaultIfEmpty(),
                     (a, b) => new DO_ServiceCode
                     {
                         ServiceId = a.s.ServiceId,
                         ServiceClassId = a.s.ServiceClassId,
                         ServiceDesc = a.s.ServiceDesc,
                         ActiveStatus = a.s.ActiveStatus,
                         BusinessLinkStatus = b == null ? false : b.ActiveStatus
                     }).ToListAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ServiceBusinessLink>> GetBusinessLocationServices(int businessKey)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEssrbls.Where(w => w.BusinessKey == businessKey)
                        .Join(db.GtEssrms,
                        b => b.ServiceId,
                        s => s.ServiceId,
                        (b, s) => new { b, s }
                        )
                        .Join(db.GtEssrcls,
                        bs => bs.s.ServiceClassId,
                        c => c.ServiceClassId,
                        (bs, c) => new { bs, c })
                                 .Select(x => new DO_ServiceBusinessLink
                                 {
                                     ServiceId = x.bs.b.ServiceId,
                                     ServiceDesc = x.bs.s.ServiceDesc,
                                     ServiceClassDesc = x.c.ServiceClassDesc,
                                     InternalServiceCode = x.bs.b.InternalServiceCode,
                                     ServiceCost = x.bs.b.ServiceCost,
                                     NightLinePercentage = x.bs.b.NightLinePercentage,
                                     HolidayPercentage = x.bs.b.HolidayPercentage,
                                     ActiveStatus = x.bs.b.ActiveStatus
                                 }
                        ).ToListAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> AddOrUpdateBusinessLocationServices(List<DO_ServiceBusinessLink> obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var ser_bl in obj)
                        {
                            var ServiceExist = db.GtEssrbls.Where(w => w.ServiceId == ser_bl.ServiceId && w.BusinessKey == ser_bl.BusinessKey).FirstOrDefault();
                            if (ServiceExist != null)
                            {
                                ServiceExist.InternalServiceCode = ser_bl.InternalServiceCode;
                                ServiceExist.ServiceCost = ser_bl.ServiceCost.Value;
                                ServiceExist.NightLinePercentage = ser_bl.NightLinePercentage;
                                ServiceExist.HolidayPercentage = ser_bl.HolidayPercentage;
                                ServiceExist.ActiveStatus = ser_bl.ActiveStatus;
                                ServiceExist.ModifiedBy = ser_bl.UserID;
                                ServiceExist.ModifiedOn = ser_bl.CreatedOn;
                                ServiceExist.ModifiedTerminal = ser_bl.TerminalID;
                            }
                            else
                            {
                                var businesslocationservice = new GtEssrbl
                                {
                                    BusinessKey = ser_bl.BusinessKey,
                                    ServiceId = ser_bl.ServiceId,
                                    InternalServiceCode = ser_bl.InternalServiceCode,
                                    ServiceCost = ser_bl.ServiceCost.Value,
                                    NightLinePercentage = ser_bl.NightLinePercentage,
                                    HolidayPercentage = ser_bl.HolidayPercentage,
                                    ActiveStatus = ser_bl.ActiveStatus,
                                    FormId = ser_bl.FormId,
                                    CreatedBy = ser_bl.UserID,
                                    CreatedOn = ser_bl.CreatedOn,
                                    CreatedTerminal = ser_bl.TerminalID
                                };
                                db.GtEssrbls.Add(businesslocationservice);
                            }
                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        return new DO_ReturnParameter() { Status = false, Message = ex.Message };
                    }
                }
            }
        }
        public async Task<List<DO_ServiceBusinessLink>> GetServiceBusinessLocations(int ServiceId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    //var bk = db.GtEcbslns.Where(w => w.ActiveStatus)
                    //    .GroupJoin(db.GtEssrbls.Where(w => w.ServiceId == ServiceId),
                    //    b => b.BusinessKey,
                    //    l => l.BusinessKey,
                    //    (b, l) => new { b, l = l.FirstOrDefault() })
                    //    .Select(r => new DO_ServiceBusinessLink
                    //    {
                    //        ServiceId = ServiceId,
                    //        BusinessKey = r.b.BusinessKey,
                    //        LocationDescription = r.b.LocationDescription,
                    //        ActiveStatus = r.l != null ? r.l.ActiveStatus : false
                    //    }).ToListAsync();

                    //return await bk;

                    var bk = db.GtEcbslns.Where(w => w.ActiveStatus)
                   .GroupJoin(db.GtEssrbls.Where(w => w.ServiceId == ServiceId),
                    b => b.BusinessKey,
                    l => l.BusinessKey,
                    (b, l) => new { b, l })
                   .SelectMany(z => z.l.DefaultIfEmpty(),
                    (a, s) => new DO_ServiceBusinessLink
                    {
                        ServiceId = ServiceId,
                        BusinessKey = a.b.BusinessKey,
                        LocationDescription = a.b.LocationDescription,
                        ActiveStatus = s == null ? false : s.ActiveStatus
                    }).ToListAsync();
                    return await bk;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> UpdateServiceBusinessLocations(List<DO_ServiceBusinessLink> obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var ser_bl in obj)
                        {
                            var ServiceExist = db.GtEssrbls.Where(w => w.ServiceId == ser_bl.ServiceId && w.BusinessKey == ser_bl.BusinessKey).FirstOrDefault();
                            if (ServiceExist != null)
                            {
                                if (ServiceExist.ActiveStatus != ser_bl.ActiveStatus)
                                {
                                    ServiceExist.ActiveStatus = ser_bl.ActiveStatus;
                                    ServiceExist.ModifiedBy = ser_bl.UserID;
                                    ServiceExist.ModifiedOn = ser_bl.CreatedOn;
                                    ServiceExist.ModifiedTerminal = ser_bl.TerminalID;
                                }

                            }
                            else
                            {
                                if (ser_bl.ActiveStatus)
                                {
                                    var businesslocationservice = new GtEssrbl
                                    {
                                        BusinessKey = ser_bl.BusinessKey,
                                        ServiceId = ser_bl.ServiceId,
                                        ActiveStatus = ser_bl.ActiveStatus,
                                        FormId = ser_bl.FormId,
                                        CreatedBy = ser_bl.UserID,
                                        CreatedOn = ser_bl.CreatedOn,
                                        CreatedTerminal = ser_bl.TerminalID
                                    };
                                    db.GtEssrbls.Add(businesslocationservice);
                                }

                            }
                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        return new DO_ReturnParameter() { Status = false, Message = ex.Message };
                    }
                }
            }
        }
    }
}

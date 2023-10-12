using eSya.ManageServices.DL.Entities;
using eSya.ManageServices.DO;
using eSya.ManageServices.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ManageServices.DL.Repository
{
    public class ClinicalServiceManagementRepository: IClinicalServiceManagementRepository
    {
        private readonly IStringLocalizer<ClinicalServiceManagementRepository> _localizer;
        public ClinicalServiceManagementRepository(IStringLocalizer<ClinicalServiceManagementRepository> localizer)
        {
            _localizer = localizer;
        }
        public async Task<List<DO_SpecialtyCodes>> GetSpecialtiesByBusinessKey(int businessKey)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var do_ms = db.GtEsspbls.Where(x => x.BusinessKey == businessKey)
                        .Join(db.GtEsspcds.Where(x => x.ActiveStatus == true),
                        d => new { d.SpecialtyId },
                        c => new { c.SpecialtyId },
                        (d, c) => new { d, c }
                        )
                        .AsNoTracking()
                        .Select(x => new DO_SpecialtyCodes
                        {
                            SpecialtyID = x.d.SpecialtyId,
                            SpecialtyDesc = x.c.SpecialtyDesc,
                            ActiveStatus = x.c.ActiveStatus
                        }).OrderBy(x => x.SpecialtyDesc).ToListAsync();

                    return await do_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<DO_DoctorClinic>> GetClinicAndConsultationTypebySpecialty(int businessKey, int specialtyId)
        {
            using (var db = new eSyaEnterprise())
            {

                try
                {
                    //var do_cl = db.GtEsopcl
                    //    .Join(db.GtEcapcd.Where(w => w.CodeType == CodeTypeValue.Clinic),
                    //        l => new { l.ClinicId },
                    //        c => new { ClinicId = c.ApplicationCode },
                    //        (l, c) => new { l, c })
                    //    .Join(db.GtEcapcd.Where(w => w.CodeType == CodeTypeValue.ConsultationType),
                    //        lc => new { lc.l.ConsultationId },
                    //        o => new { ConsultationId = o.ApplicationCode },
                    //        (lc, o) => new { lc, o })
                    //     .GroupJoin(db.GtEsdocl.Where(w => w.BusinessKey == businessKey && w.SpecialtyId == specialtyId),
                    //         lco => new { lco.lc.l.BusinessKey, lco.lc.l.ClinicId, lco.lc.l.ConsultationId },
                    //         d => new { d.BusinessKey, d.ClinicId, d.ConsultationId },
                    //         (lco, d) => new { lco, d = d.DefaultIfEmpty().FirstOrDefault() })
                    //     .Where(w => w.lco.lc.l.BusinessKey == businessKey)
                    //   .Select(r => new DO_DoctorClinic
                    //   {

                    //       ClinicId= r.d != null ? r.d.ClinicId:0,
                    //       ClinicDesc= r.lco.lc.c.CodeDesc,
                    //       ConsultationId = r.d != null ? r.d.ConsultationId : 0,
                    //       ConsultationDesc = r.lco.o.CodeDesc,
                    //       ActiveStatus = r.d != null ? true : false

                    //       //BusinessKey = r.d != null ? r.d.BusinessKey : 0,
                    //       //ClinicId = r.lco.lc.l.ClinicId,
                    //       //ClinicDesc = r.lco.lc.c.CodeDesc,
                    //       //ConsultationId = r.lco.lc.l.ConsultationId,
                    //       //ConsultationDesc = r.lco.o.CodeDesc,
                    //       //ActiveStatus = r.d != null ? r.d.ActiveStatus : false
                    //   }).ToListAsync();

                    var do_cl = db.GtEsopcls

                          .Select(r => new DO_DoctorClinic
                          {


                              ActiveStatus = r.ActiveStatus

                              //BusinessKey = r.d != null ? r.d.BusinessKey : 0,
                              //ClinicId = r.lco.lc.l.ClinicId,
                              //ClinicDesc = r.lco.lc.c.CodeDesc,
                              //ConsultationId = r.lco.lc.l.ConsultationId,
                              //ConsultationDesc = r.lco.o.CodeDesc,
                              //ActiveStatus = r.d != null ? r.d.ActiveStatus : false
                          }).ToListAsync();

                    return await do_cl;
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }

        public async Task<List<DO_DoctorMaster>> GetDoctorsbySpecialtyClinicAndConsultation(int businessKey, int specialtyId, int clinicId, int consultationId)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var do_cl = db.GtEsdocls
                        .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.Clinic),
                        l => new { l.ClinicId },
                        c => new { ClinicId = c.ApplicationCode },
                        (l, c) => new { l, c })
                        //.Join(db.GtEcapcd.Where(w => w.CodeType == CodeTypeValue.ConsultationType),
                        //lc => new { lc.l.ConsultationId },
                        //o => new { ConsultationId = o.ApplicationCode },
                        //(lc, o) => new { lc, o })
                        .Join(db.GtEsdocds.Where(w => w.ActiveStatus),
                                                //lco => new { lco.lc.l.DoctorId },
                                                lco => new { lco.l.DoctorId },
                        d => new { d.DoctorId },
                        (lco, d) => new { lco, d })
                        .Join(db.GtEsspcds.Where(w => w.ActiveStatus),
                                                //lcod => new { lcod.lco.lc.l.SpecialtyId },
                                                lcod => new { lcod.lco.l.SpecialtyId },

                        s => new { s.SpecialtyId },
                        (lcod, s) => new { lcod, s })
                                               //.Where(w => w.lcod.lco.lc.l.BusinessKey == businessKey && w.lcod.lco.lc.l.SpecialtyId == specialtyId && w.lcod.lco.lc.l.ClinicId == clinicId && w.lcod.lco.lc.l.ConsultationId == consultationId)
                                               .Where(w => w.lcod.lco.l.BusinessKey == businessKey && w.lcod.lco.l.SpecialtyId == specialtyId && w.lcod.lco.l.ClinicId == clinicId)

                        .AsNoTracking()
                       .Select(r => new DO_DoctorMaster
                       {
                           //DoctorId = r.lcod.lco.lc.l.DoctorId,
                           //DoctorName = r.lcod.d.DoctorName,
                           DoctorId = r.lcod.lco.l.DoctorId,
                           DoctorName = r.lcod.d.DoctorName,
                       }).ToListAsync();

                    return await do_cl;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<DO_DoctordaySchedule>> GetDoctordaySchedulebySearchCriteria(int businessKey, int specialtyId, int clinicId, int consultationId, int doctorId, DateTime scheduleFromDate, DateTime scheduleToDate)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_sc = db.GtEsdos2s
                        .Join(db.GtEsspcds,
                        o => new { o.SpecialtyId },
                        s => new { s.SpecialtyId },
                        (o, s) => new { o, s })
                        .Join(db.GtEsdocds,
                        os => new { os.o.DoctorId },
                        d => new { d.DoctorId },
                        (os, d) => new { os, d })

                        .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.Clinic),
                            l => new { l.os.o.ClinicId },
                            c => new { ClinicId = c.ApplicationCode },
                            (l, c) => new { l, c })
                        .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.ConsultationType),
                            lc => new { lc.l.os.o.ConsultationId },
                            ol => new { ConsultationId = ol.ApplicationCode },
                            (lc, ol) => new { lc, ol })
                        .Where(w => w.lc.l.os.o.BusinessKey == businessKey && w.lc.l.os.o.ClinicId == clinicId && w.lc.l.os.o.ConsultationId == consultationId
                         && w.lc.l.os.o.SpecialtyId == specialtyId && w.lc.l.os.o.DoctorId == doctorId /*&& w.lc.l.os.o.ScheduleDate.Date >= scheduleFromDate.Date */
                        /* && w.lc.l.os.o.ScheduleDate.Date <= scheduleToDate.Date*/)

                        .AsNoTracking()

                        .Select(x => new DO_DoctordaySchedule
                        {

                            BusinessKey = x.lc.l.os.o.BusinessKey,
                            ConsultationId = x.lc.l.os.o.ConsultationId,
                            ConsultationDesc = x.ol.CodeDesc,
                            ClinicId = x.lc.l.os.o.ClinicId,
                            ClinicDesc = x.lc.c.CodeDesc,
                            SpecialtyId = x.lc.l.os.o.SpecialtyId,
                            SpecialtyDesc = x.lc.l.os.s.SpecialtyDesc,
                            DoctorId = x.lc.l.os.o.DoctorId,
                            DoctorName = x.lc.l.d.DoctorName,
                            //ScheduleDate = x.lc.l.os.o.ScheduleDate,
                            SerialNo = x.lc.l.os.o.SerialNo,
                            ScheduleFromTime = x.lc.l.os.o.ScheduleFromTime,
                            ScheduleToTime = x.lc.l.os.o.ScheduleToTime,
                            //NoOfPatients = x.lc.l.os.o.NoOfPatients,
                            //XlsheetReference=x.lc.l.os.o.XlsheetReference,
                            ActiveStatus = x.lc.l.os.o.ActiveStatus
                        })
                        .ToListAsync();

                    return await dc_sc;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<DO_ReturnParameter> InsertIntoDoctordaySchedule(DO_DoctordaySchedule obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var ds_list = db.GtEsdos2s.Where(x => x.BusinessKey == obj.BusinessKey && x.ConsultationId == obj.ConsultationId
                                      && x.ClinicId == obj.ClinicId && x.SpecialtyId == obj.SpecialtyId && x.DoctorId == obj.DoctorId
                                     /* && x.ScheduleDate == obj.ScheduleDate*/ && x.ActiveStatus).ToList();

                        bool isexists = false;
                        foreach (var item in ds_list)
                        {
                            if ((obj.ScheduleFromTime >= item.ScheduleFromTime && obj.ScheduleFromTime < item.ScheduleToTime)
                                   || (obj.ScheduleToTime > item.ScheduleFromTime && obj.ScheduleToTime <= item.ScheduleToTime))
                            {
                                isexists = true;
                            }
                        }
                        if (isexists == true)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0141", Message = string.Format(_localizer[name: "W0141"]) };
                        }
                        var _isexists = await db.GtEsdos2s.Where(x => x.BusinessKey == obj.BusinessKey && x.ConsultationId == obj.ConsultationId
                                     && x.ClinicId == obj.ClinicId && x.SpecialtyId == obj.SpecialtyId && x.DoctorId == obj.DoctorId
                                     /*&& x.ScheduleDate == obj.ScheduleDate*/).FirstOrDefaultAsync();
                        if (_isexists != null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0142", Message = string.Format(_localizer[name: "W0142"]) };
                        }
                        else
                        {
                            int serialNumber = db.GtEsdos2s.Where(x => x.BusinessKey == obj.BusinessKey && x.ConsultationId == obj.ConsultationId && x.ClinicId == obj.ClinicId && x.SpecialtyId == obj.SpecialtyId && x.DoctorId == obj.DoctorId /*&& x.ScheduleDate == obj.ScheduleDate*/).Select(x => x.SerialNo).DefaultIfEmpty().Max() + 1;
                            ////int serialNumber = db.GtEsdos2.Where(x => x.BusinessKey == obj.BusinessKey && x.ConsultationId == obj.ConsultationId && x.ClinicId == obj.ClinicId && x.SpecialtyId == obj.SpecialtyId && x.DoctorId == obj.DoctorId ).Select(x => x.SerialNo).DefaultIfEmpty().Max() + 1;

                            var do_sc = new GtEsdos2
                            {
                                BusinessKey = obj.BusinessKey,
                                ConsultationId = obj.ConsultationId,
                                ClinicId = obj.ClinicId,
                                SpecialtyId = obj.SpecialtyId,
                                DoctorId = obj.DoctorId,
                                //ScheduleDate = obj.ScheduleDate,
                                SerialNo = serialNumber,
                                ScheduleFromTime = obj.ScheduleFromTime,
                                ScheduleToTime = obj.ScheduleToTime,
                                //NoOfPatients = obj.NoOfPatients,
                                //XlsheetReference = "#",
                                //XlsheetReference = obj.XlsheetReference,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormId,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID,
                            };

                            db.GtEsdos2s.Add(do_sc);
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public async Task<DO_ReturnParameter> UpdateDoctordaySchedule(DO_DoctordaySchedule obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdos2 _daySchedule = db.GtEsdos2s.Where(x => x.BusinessKey == obj.BusinessKey && x.ConsultationId == obj.ConsultationId && x.ClinicId == obj.ClinicId && x.SpecialtyId == obj.SpecialtyId && x.DoctorId == obj.DoctorId /*&& x.ScheduleDate == obj.ScheduleDate*/ && x.SerialNo == obj.SerialNo).FirstOrDefault();
                        if (_daySchedule == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0143", Message = string.Format(_localizer[name: "W0143"]) };
                        }
                        else
                        {
                            bool isexists = false;
                            var ds_list = db.GtEsdos2s.Where(x => x.BusinessKey == obj.BusinessKey && x.ConsultationId == obj.ConsultationId
                                      && x.ClinicId == obj.ClinicId && x.SpecialtyId == obj.SpecialtyId && x.DoctorId == obj.DoctorId
                                      /*&& x.ScheduleDate == obj.ScheduleDate*/ && x.ActiveStatus && x.SerialNo != obj.SerialNo).ToList();

                            foreach (var item in ds_list)
                            {
                                if ((obj.ScheduleFromTime >= item.ScheduleFromTime && obj.ScheduleFromTime < item.ScheduleToTime)
                                       || (obj.ScheduleToTime > item.ScheduleFromTime && obj.ScheduleToTime <= item.ScheduleToTime))
                                {
                                    isexists = true;
                                }
                            }
                            if (isexists == true)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0141", Message = string.Format(_localizer[name: "W0141"]) };
                            }

                            _daySchedule.ScheduleFromTime = obj.ScheduleFromTime;
                            _daySchedule.ScheduleToTime = obj.ScheduleToTime;
                            //_daySchedule.NoOfPatients = obj.NoOfPatients;
                            //_daySchedule.XlsheetReference = obj.XlsheetReference;
                            _daySchedule.ActiveStatus = obj.ActiveStatus;
                            _daySchedule.ModifiedBy = obj.UserID;
                            _daySchedule.ModifiedOn = System.DateTime.Now;
                            _daySchedule.ModifiedTerminal = obj.TerminalID;

                        };

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public async Task<DO_ReturnParameter> ActiveOrDeActiveDoctordaySchedule(DO_DoctordaySchedule objdel)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdos2 _dayschedule = db.GtEsdos2s.Where(x => x.BusinessKey == objdel.BusinessKey && x.ConsultationId == objdel.ConsultationId && x.ClinicId == objdel.ClinicId && x.SpecialtyId == objdel.SpecialtyId && x.DoctorId == objdel.DoctorId && x.SerialNo == objdel.SerialNo /*&& x.ScheduleDate== objdel.ScheduleDate*/).FirstOrDefault();
                        if (_dayschedule == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0144", Message = string.Format(_localizer[name: "W0144"]) };
                        }

                        _dayschedule.ActiveStatus = objdel.status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (objdel.status == true)
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };
                        else
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public async Task<DO_ReturnParameter> ImpotedDoctorScheduleList(List<DO_DoctordaySchedule> obj)
        {
            using (var db = new eSyaEnterprise())
            {

                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        foreach (var time in obj)
                        {
                            if (time.ScheduleFromTime >= time.ScheduleToTime)
                            {
                                return new DO_ReturnParameter() { Status = false,StatusCode= "W0145", Message = time.ScheduleFromTime + string.Format(_localizer[name: "W0145"]) + time.ScheduleToTime + string.Format(_localizer[name: "W0146"]) };
                            }

                            var doctor = db.GtEsdocds.Where(x => x.DoctorName.ToUpper().Trim() == time.DoctorName.ToUpper().Trim()).FirstOrDefault();
                            if (doctor == null)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0148", Message = string.Format(_localizer[name: "W0147"]) + time.DoctorName + string.Format(_localizer[name: "W0148"]) };
                            }
                            else
                            {
                                time.DoctorId = doctor.DoctorId;
                            }
                            var clinic = db.GtEcapcds.Where(x => x.CodeDesc.ToUpper().Trim() == time.ClinicDesc.ToUpper().Trim()).FirstOrDefault();
                            if (clinic == null)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0148", Message = string.Format(_localizer[name: "W0147"]) + time.ClinicDesc + string.Format(_localizer[name: "W0148"]) };
                            }
                            else
                            {
                                time.ClinicId = clinic.ApplicationCode;
                            }
                            var consultation = db.GtEcapcds.Where(x => x.CodeDesc.ToUpper().Trim() == time.ConsultationDesc.ToUpper().Trim()).FirstOrDefault();
                            if (consultation == null)
                            {
                                return new DO_ReturnParameter() { Status = false,StatusCode = "W0148", Message = string.Format(_localizer[name: "W0147"]) + time.ConsultationDesc + string.Format(_localizer[name: "W0148"]) };
                            }
                            else
                            {
                                time.ConsultationId = consultation.ApplicationCode;
                            }
                            var specialty = db.GtEsspcds.Where(x => x.SpecialtyDesc.ToUpper().Trim() == time.SpecialtyDesc.ToUpper().Trim()).FirstOrDefault();
                            if (specialty == null)
                            {
                                return new DO_ReturnParameter() { Status = false,StatusCode= "W0148", Message = string.Format(_localizer[name: "W0147"]) + time.SpecialtyDesc + string.Format(_localizer[name: "W0148"]) };
                            }
                            else
                            {
                                time.SpecialtyId = specialty.SpecialtyId;
                            }


                            var ds_list = db.GtEsdos2s.Where(x => x.BusinessKey == time.BusinessKey && x.ConsultationId == time.ConsultationId
                                      && x.ClinicId == time.ClinicId && x.SpecialtyId == time.SpecialtyId && x.DoctorId == time.DoctorId
                                      /*&& x.ScheduleDate == time.ScheduleDate*/ && x.ActiveStatus).ToList();

                            bool isexists = false;
                            foreach (var _isexists in ds_list)
                            {
                                if ((time.ScheduleFromTime >= _isexists.ScheduleFromTime && time.ScheduleFromTime < _isexists.ScheduleToTime)
                                       || (time.ScheduleToTime > _isexists.ScheduleFromTime && time.ScheduleToTime <= _isexists.ScheduleToTime))
                                {
                                    isexists = true;
                                }
                            }
                            if (isexists == true)
                            {
                                return new DO_ReturnParameter() { Status = false,StatusCode= "W0150", Message = string.Format(_localizer[name: "W0149"]) + time.ScheduleDate + string.Format(_localizer[name: "W0150"]) + time.DoctorName };
                            }

                            var scheduled = await db.GtEsdos2s.Where(x => x.BusinessKey == time.BusinessKey && x.ConsultationId == time.ConsultationId
                                     && x.ClinicId == time.ClinicId && x.SpecialtyId == time.SpecialtyId && x.DoctorId == time.DoctorId
                                    /* && x.ScheduleDate == time.ScheduleDate*/ && x.SerialNo == time.SerialNo).FirstOrDefaultAsync();

                            if (scheduled == null)
                            {
                                int serialNumber = db.GtEsdos2s.Where(x => x.BusinessKey == time.BusinessKey && x.ConsultationId == time.ConsultationId && x.ClinicId == time.ClinicId && x.SpecialtyId == time.SpecialtyId && x.DoctorId == time.DoctorId /*&& x.ScheduleDate == time.ScheduleDate*/).Select(x => x.SerialNo).DefaultIfEmpty().Max() + 1;
                                //int serialNumber = db.GtEsdos2.Where(x => x.BusinessKey == time.BusinessKey && x.ConsultationId == time.ConsultationId && x.ClinicId == time.ClinicId && x.SpecialtyId == time.SpecialtyId && x.DoctorId == time.DoctorId).Select(x => x.SerialNo).DefaultIfEmpty().Max() + 1;
                                var do_sc = new GtEsdos2
                                {
                                    BusinessKey = time.BusinessKey,
                                    ConsultationId = time.ConsultationId,
                                    ClinicId = time.ClinicId,
                                    SpecialtyId = time.SpecialtyId,
                                    DoctorId = time.DoctorId,
                                    //ScheduleDate = time.ScheduleDate,
                                    SerialNo = serialNumber,
                                    ScheduleFromTime = time.ScheduleFromTime,
                                    ScheduleToTime = time.ScheduleToTime,
                                    //NoOfPatients = time.NoOfPatients,
                                    //XlsheetReference = "#",
                                    ActiveStatus = time.ActiveStatus,
                                    FormId = time.FormId,
                                    CreatedBy = time.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = time.TerminalID,
                                };

                                db.GtEsdos2s.Add(do_sc);
                                await db.SaveChangesAsync();
                            }
                            else
                            {
                                scheduled.ScheduleFromTime = time.ScheduleFromTime;
                                scheduled.ScheduleToTime = time.ScheduleToTime;
                                //scheduled.NoOfPatients = time.NoOfPatients;
                                //scheduled.XlsheetReference = "#";
                                scheduled.ActiveStatus = time.ActiveStatus;
                                scheduled.ModifiedBy = time.UserID;
                                scheduled.ModifiedOn = System.DateTime.Now;
                                scheduled.ModifiedTerminal = time.TerminalID;
                                await db.SaveChangesAsync();
                            }

                        }
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                    }
                    catch (DbUpdateException ex)
                    {

                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }

                }
            }

        }
    }
}

using eSya.ManageServices.DL.Entities;
using eSya.ManageServices.DO;
using eSya.ManageServices.IF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ManageServices.DL.Repository
{
    public class CommonMethodRepository: ICommonMethodRepository
    {
        public async Task<List<DO_BusinessLocation>> GetBusinessKey()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var bk = db.GtEcbslns
                        .Where(w => w.ActiveStatus)
                        .Select(r => new DO_BusinessLocation
                        {
                            BusinessKey = r.BusinessKey,
                            LocationDescription = r.BusinessName + " - " + r.LocationDescription
                        }).ToListAsync();

                    return await bk;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ApplicationCode>> GetApplicationCodesByCodeType(int codetype)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var bk = db.GtEcapcds
                        .Where(w =>w.CodeType==codetype && w.ActiveStatus)
                        .Select(r => new DO_ApplicationCode
                        {
                            ApplicationCode = r.ApplicationCode,
                            CodeDesc = r.CodeDesc 
                        }).ToListAsync();

                    return await bk;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}

using FiiiPay.Enterprise.Data.Agent;
using FiiiPay.Enterprise.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.BLL
{
    public class BlobBLL 
    {      
        public string UploadImage(FileUploadModel model)
        {
            return new MasterImageAgent().Upload(model.FileName, model.File);
        }

        public byte[] Download(string id)
        {
            return new MasterImageAgent().Download(id);
        }

        

    }
}
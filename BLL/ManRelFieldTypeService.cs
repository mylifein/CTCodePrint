﻿using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ManRelFieldTypeService
    {
        ManRelFieldTypeDao manRelFieldTypeDao = new ManRelFieldTypeDao();

        /// <summary>
        /// Union表查詢
        /// </summary>
        /// <param name="manNo"></param>
        /// <returns></returns>
        public List<MandUnionFieldType> queryMandUnionFieldTypeList(string manNo)
        {
            return manRelFieldTypeDao.queryMandUnionFieldTypeList(manNo);
        }


        public bool deleteManRelFieldType(string uuid)
        {
            return manRelFieldTypeDao.deleteManRelFieldType(uuid);
        }

        public ManRelFieldType saveManRelFieldType(ManRelFieldType manRelFieldType)
        {
            ManRelFieldType reManRelFieldType = null;
            manRelFieldType.Uuid = Auxiliary.Get_UUID();
            manRelFieldType.OpUser = Auxiliary.loginName;
            manRelFieldType.CreateTime = Auxiliary.Get_CurrentTime();
            if (manRelFieldTypeDao.saveManRelFieldType(manRelFieldType))
            {
                reManRelFieldType = manRelFieldTypeDao.queryManRelFieldTypeById(manRelFieldType.Uuid);
            }
            return reManRelFieldType;
        }

        public bool checkAdd(ManRelFieldType manRelFieldType)
        {
            return manRelFieldTypeDao.exists(manRelFieldType);
        }


    }
}

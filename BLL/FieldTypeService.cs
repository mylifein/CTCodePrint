using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class FieldTypeService
    {
        private readonly FieldTypeDao fieldTypeDao = new FieldTypeDao();

        /// <summary>
        /// 保存字段類型信息
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public FieldType saveFieldType(FieldType fieldType)
        {
            FieldType reFieldType = null;
            fieldType.Uuid = Auxiliary.Get_UUID();
            fieldType.OpUser = Auxiliary.loginName;
            fieldType.CreateTime = Auxiliary.Get_CurrentTime();
            if (fieldTypeDao.saveFieldType(fieldType))
            {
                reFieldType = fieldTypeDao.queryFieldTypeById(fieldType.Uuid);
            }
            return reFieldType;
        }

        /// <summary>
        /// 模糊查詢所有字段類型信息
        /// </summary>
        /// <param name="fieldNo"></param>
        /// <returns></returns>
        public List<FieldType> queryFieldTypeList(string fieldNo)
        {
            return fieldTypeDao.queryFieldTypeList(fieldNo);
        }
    }
}

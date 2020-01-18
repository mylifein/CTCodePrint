using DAL;
using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class DepartmentService
    {
        private readonly DepartDao departDao = new DepartDao();

        public Department saveDepartment(Department department)
        {
            Department reDepartment = null;
            department.Uuid = Auxiliary.Get_UUID();
            department.Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            department.Opuser = Auxiliary.loginName;
            if (departDao.saveDepart(department))
            {
                reDepartment = departDao.queryDepartmentById(department.Uuid);
            }
            return reDepartment;
        }


        /// <summary>
        /// TODO 根據departmentId 模糊查詢有部門
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<Department> queryDepartmentList(string departmentId)
        {
            return departDao.queryDepartmentList(departmentId);
        }

    }
}

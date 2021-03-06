﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contoso.Data.Repositories;
using Contoso.Model;

namespace Contoso.Service
{
   public class DepartmentService: IDepartmentService
   {
       private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
           return _departmentRepository.GetAll();
        }

       public IEnumerable<Department> GetAllDepartmentsIncludeCourses()
       {
         //  throw new DivideByZeroException();
           return _departmentRepository.GetAllDepartmentsIncludeCourses();
          // return _departmentRepository.GetAllDepartmentsLazyCourses();
       }

       public IEnumerable<Department> GetDepartmentsPagination(int pageSize = 8, int pageIndex = 0, string name = "")
       {
           return _departmentRepository.GetDepartmentsPagination(pageSize, pageIndex, name);
       }

       public int GetTotalDepartments(string name="")
       {
           if (string.IsNullOrEmpty(name)) return _departmentRepository.GetCount();
           return _departmentRepository.GetCount(d => d.Name.Contains(name));
        }
   }

   public interface IDepartmentService
   {
       IEnumerable<Department> GetAllDepartments();
       IEnumerable<Department> GetAllDepartmentsIncludeCourses();
       IEnumerable<Department> GetDepartmentsPagination(int pageSize = 8, int pageIndex = 0, string name = "");
       int GetTotalDepartments(string name = "");
   }
}

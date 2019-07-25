using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Transactions;
using Contoso.Data.Repositories;
using Contoso.Model;
using Contoso.Model.Common;
using Contoso.Utility;

namespace Contoso.Service
{
    public class StudentService : IStudentService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentService(IPersonRepository personRepository, IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            _personRepository = personRepository;
        }

        public IEnumerable<Student> GetAllStudents(int? page, int pageSize, out int totalCount)
        {
            var students = _studentRepository.GetPagedList(out totalCount, page, pageSize, null, null,
                new SortExpression<Student>(s => s.FirstName, ListSortDirection.Ascending));

            return students;
        }

        public PagedResultSet<Student> GetStudentsByPagination(int? page, int pageSize)
        {
            return _studentRepository.GetPagedData(page ?? 0, pageSize);
        }

        public Student GetStudentById(int id)
        {
            return _studentRepository.GetById(id);
        }

        public IEnumerable<Student> GetStudentByName(string name)
        {
            return _studentRepository.GetMany(s => s.LastName.Contains(name) || s.FirstName.Contains(name)).ToList();
        }

        public Student GetStudentByCode(string employeeCode)
        {
            throw new NotImplementedException();
        }

        public void CreateStudent(Student student)
        {
            using (var transaction = new TransactionScope())
            {
                _personRepository.Add(student);
                _personRepository.SaveChanges();
                transaction.Complete();
            }
        }

        public void UpdateStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }

    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudents(int? page, int pageSize, out int totalCount);
        PagedResultSet<Student> GetStudentsByPagination(int? page, int pageSize);
        Student GetStudentById(int id);
        IEnumerable<Student> GetStudentByName(string name);
        Student GetStudentByCode(string employeeCode);
        void CreateStudent(Student student);
        void UpdateStudent(Student student);
    }
}
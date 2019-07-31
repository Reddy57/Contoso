using System;
using System.Collections.Generic;
using System.Linq;
using Contoso.Data;
using Contoso.Data.Repositories;
using Contoso.Model;
using Contoso.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Contoso.Test.Services
{
    /*
    Arrange: Initializes objects, creates mocks with arguments that are passed to the method under test and adds expectations.
    Act: Invokes the method or property under test with the arranged parameters.
    Assert: Verifies that the action of the method under test behaves as expected.
    */

    // The TestClass attribute denotes a class that contains unit tests
    [TestClass]
    public class StudentServiceTest
    {
        // arrange

        private Mock<IPersonRepository> _mockPersonRepository;
        private Mock<IStudentRepository> _mockStudentRepository;
        private List<Student> _students;
        private IStudentService _studentService;

        // The TestMethod attribute indicates a method is a test method.
        [TestMethod]
        public void Check_StudentsCountFromTheFakeData()
        {
            var totalCount = 0;

            // act
            var students = _studentService.GetAllStudents(1, 10, out totalCount);

            // assert
            Assert.IsInstanceOfType(students, typeof(IEnumerable<Student>));
            Assert.IsNotNull(students);
         //   Assert.AreEqual(8, students.Count());
            CollectionAssert.AllItemsAreInstancesOfType(students.ToList(),typeof(Student));
        }

        /// <summary>
        /// A DataTestMethod attribute represents a suite of tests that execute the same code but have different input arguments.
        /// You can use the DataRow attribute to specify values for those inputs.
        /// </summary>
        //[TestMethod]
        [DataTestMethod]
        [DataRow(1, "Test LastName1")]
        [DataRow(2, "Test LastName2")]
        [DataRow(3, "Test LastName3")]
        public void Check_Student_ById_FromTheFakeData(int id, string expectedLastName)
        {
            var student = _studentService.GetStudentById(id);
            Assert.IsNotNull(student); // Test if student is null or not
            Assert.IsInstanceOfType(student, typeof(Student)); //  Test if type returned is Student
            Assert.AreEqual(expectedLastName, student.LastName);
        }

        [DataTestMethod]
        [DataRow(0 )]
        [DataRow(-1 )]
        public void Check_Student_ById_FromTheFakeDataForExceptions(int id)
        {
            Assert.ThrowsException<InvalidOperationException>(() => _studentService.GetStudentById(id));

        }

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            _mockStudentRepository = new Mock<IStudentRepository>();
            _mockPersonRepository = new Mock<IPersonRepository>();

            IStudentRepository repo = new StudentRepository(new ContosoDbContext());

            _studentService = new StudentService(_mockPersonRepository.Object, _mockStudentRepository.Object);

            _students = new List<Student>
                        {
                            new Student
                            {
                                Id = 1,
                                FirstName = "Test Name 1",
                                LastName = "Test LastName1",
                                City = "DC",
                                Email = "test@test.com"
                            },
                            new Student
                            {
                                Id = 2,
                                FirstName = "Test Name 2",
                                LastName = "Test LastName2",
                                City = "DC",
                                Email = "test2@test.com"
                            },
                            new Student
                            {
                                Id = 3,
                                FirstName = "Test Name 3",
                                LastName = "Test LastName3",
                                City = "DC",
                                Email = "test3@test.com"
                            },
                            new Student
                            {
                                Id = 4,
                                FirstName = "Test Name 4",
                                LastName = "Test LastName4",
                                City = "DC",
                                Email = "test4@test.com"
                            },
                            new Student
                            {
                                Id = 5,
                                FirstName = "Test Name 5",
                                LastName = "Test LastName5",
                                City = "DC",
                                Email = "test5@test.com"
                            },
                            new Student
                            {
                                Id = 6,
                                FirstName = "Test Name 6",
                                LastName = "Test LastName6",
                                City = "DC",
                                Email = "test6@test.com"
                            },
                            new Student
                            {
                                Id = 7,
                                FirstName = "Test Name 7",
                                LastName = "Test LastName7",
                                City = "DC",
                                Email = "test7@test.com"
                            },
                            new Student
                            {
                                Id = 8,
                                FirstName = "Test Name 8",
                                LastName = "Test LastName8",
                                City = "DC",
                                Email = "test8@test.com"
                            }
                        };

            _mockStudentRepository.Setup(s => s.GetAll()).Returns(_students);

            _mockStudentRepository.Setup(s => s.GetById(It.IsAny<int>()))
                                  .Returns((int s) => _students.First(x => x.Id == s));

            _mockStudentRepository.Setup(s => s.GetStudentByLastName(It.IsAny<string>()))
                                  .Returns((string s) => _students.First(x => x.LastName == s));
        }
    }
}
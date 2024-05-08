using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniConnectAPI.Data;
using UniConnectAPI.Models;
using UniConnectAPI.Repository.IRepository;

namespace UniConnectAPI.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _db;
        private int studentIDCounter = 0;
        private static readonly object lockObject = new object();

        public StudentRepository(ApplicationDbContext db)
        {
            this._db = db;
        }
        public async Task CreateAsync(Student student)
        {
            student.StudentID = GenerateStudentID();
            await  _db.Students.AddAsync(student);
            await Save();
        }

        public async Task<Student> GetAsync(Expression<Func<Student,bool>> filter = null)
        {
            IQueryable<Student> query = _db.Students;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<List<Student>> GetAllAsync(Expression<Func<Student,bool>> filter = null)
        {
            IQueryable<Student> query = _db.Students;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(Student student)
        {
             _db.Students.Remove(student);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        private string GenerateStudentID()
        {

            lock (lockObject)
            {
                // Getting the current year as first 2 digits
                string currentYear = DateTime.Now.Year.ToString().Substring(2);

                // Fetch all student IDs from the database
                var studentIds = _db.Students
                .Where(s => s.StudentID.StartsWith(currentYear)) // Assuming student IDs start with the current year
                .Select(s => s.StudentID)
                .ToList();

                // If no student IDs are found, set the counter to 0
                if (studentIds.Count == 0)
                {
                    studentIDCounter = 0;
                }
                else
                {
                    // Get the maximum numeric part of the student IDs
                    var maxNumericID = studentIds
                        .Select(id => int.Parse(id.Substring(2))) // Assuming '24' as prefix
                        .Max();

                    // Set the student ID counter to the maximum numeric ID
                    studentIDCounter = maxNumericID;
                }

                // Increment the counter for the next student ID
                studentIDCounter++;

                // Formatting student ID with current year and six-digit number
                string studentID = $"{currentYear}{studentIDCounter:D6}";
                return studentID;
            }
        }

        public async Task UpdateAsync(Student student)
        {
            _db.Update(student);
            await Save();
        }
    }
}

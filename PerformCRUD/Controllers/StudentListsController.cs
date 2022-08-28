using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PerformCRUD.Data;
using PerformCRUD.Models;

namespace PerformCRUD.Controllers
{
    public class StudentListsController : ApiController
    {
        private PerformCRUDContext db = new PerformCRUDContext();

        // GET: api/StudentLists
        public IQueryable<StudentListDto> GetStudentLists()
        {
            var studentsDto = from b in db.StudentLists
                        select new StudentListDto()
                        {
                            Id = b.Id,
                            Name = b.Name,
                            Age = b.Age,
                            Grade = b.Grade
                        };
            return studentsDto;

            //return db.StudentLists;
        }

        // GET: api/StudentLists/5
        [ResponseType(typeof(StudentListDto))]
        public async Task<IHttpActionResult> GetStudentList(int id)
        {
            //StudentList studentList = await db.StudentLists.FindAsync(id);
            //if (studentList == null)
            //{
            //    return NotFound();
            //}

            var studentList = await db.StudentLists.Include(b => b.Name).Select(b =>
                new StudentListDto()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Age = b.Age,
                    Grade = b.Grade
                }).SingleOrDefaultAsync(b => b.Id == id);

            if (studentList == null)
            {
                return NotFound();
            }

            return Ok(studentList);
        }

        // PUT: api/StudentLists/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStudentList(int id, StudentList studentList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentList.Id)
            {
                return BadRequest();
            }

            db.Entry(studentList).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StudentLists
        [ResponseType(typeof(StudentListDto))]
        public async Task<IHttpActionResult> PostStudentList(StudentList studentList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentLists.Add(studentList);
            await db.SaveChangesAsync();

            db.Entry(studentList).Reference(x => x.Name).Load();

            var dto = new StudentListDto()
            {
                Id = studentList.Id,
                Name = studentList.Name,
                Grade = studentList.Grade,
                Age = studentList.Age
            };

            return CreatedAtRoute("DefaultApi", new { id = studentList.Id }, dto);
        }

        // DELETE: api/StudentLists/5
        [ResponseType(typeof(StudentList))]
        public async Task<IHttpActionResult> DeleteStudentList(int id)
        {
            StudentList studentList = await db.StudentLists.FindAsync(id);
            if (studentList == null)
            {
                return NotFound();
            }

            db.StudentLists.Remove(studentList);
            await db.SaveChangesAsync();

            return Ok(studentList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentListExists(int id)
        {
            return db.StudentLists.Count(e => e.Id == id) > 0;
        }
    }
}
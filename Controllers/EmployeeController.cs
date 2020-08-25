using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using thirtProject.Models;

namespace thirtProject.Controllers
{
   
    public class EmployeeController : ApiController
    {
      
        [BasicAuthentication]
        public HttpResponseMessage get(string gender="All")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (EmployeeDBEntities entitiees = new EmployeeDBEntities())
            {
                switch (username.ToLower())
                {
                   
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, entitiees.Employee.Where(e => e.gender.ToLower() == "male").ToList());
                    case "famale":
                        return Request.CreateResponse(HttpStatusCode.OK, entitiees.Employee.Where(e => e.gender.ToLower() == "famale").ToList());

                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
        }

        [HttpGet]

        public HttpResponseMessage get(int Id)
        {
            using (EmployeeDBEntities entitiees = new EmployeeDBEntities())
            {
                var entity = entitiees.Employee.FirstOrDefault(e => e.Id == Id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id = " + Id.ToString() + "not found");
                }
            }
        }
        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Employee.Add(employee);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employee.Remove(entities.Employee.FirstOrDefault(e => e.Id == id));
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " not found ");
                    }
                    else
                    {
                        entities.Employee.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entites = new EmployeeDBEntities())
                {
                    var entity = entites.Employee.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Empoyee with Id = " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Salary = employee.Salary;
                        entity.Id = employee.Id;
                        entites.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

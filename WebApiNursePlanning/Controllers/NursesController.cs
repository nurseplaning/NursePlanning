using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiNursePlanning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NursesController : ControllerBase
    {

        private readonly INurseRepository repository;

        public NursesController(INurseRepository nurseRepository)
        {
            repository = nurseRepository;
        }
        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nurse>>> GetNurses()
        {
            return Ok(await repository.ListNurses());
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(string id)
        {
            var nurse = await repository.Details(id);

            if (nurse == null)
            {
                return NotFound();
            }

            return Ok(nurse);
        }
    }
}

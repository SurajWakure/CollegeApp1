using AutoMapper;
using Collegeapp1.Data;
using Collegeapp1.Data.Reposetory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace Collegeapp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors(PolicyName = "AllowOnlyGoogleApps")]
    [Authorize(AuthenticationSchemes = "LoginForLocalUsers", Roles ="SuperAdmin,Admin")]
    //[AllowAnonymous] anybody can access

    public class StudentController : ControllerBase
    {
        private readonly CollegeDbContext _dbContext;
        //private readonly ICollegeReposetory<Student> _studentrepo;
        private readonly IStudentReposetory _studentrepo;
        private readonly IMapper _mapper;
        private ApiResponse _apiresponse;

        public StudentController(IStudentReposetory studentrepo, IMapper mapper)
        {
            _studentrepo=studentrepo;
            _mapper=mapper;
            _apiresponse=new ();
        }

        [HttpGet]
        [Route("All", Name = "Getstudents")]
        public async Task<ActionResult<ApiResponse>> Getstudents()
        {

            try
            {
             var students =await _studentrepo.GetAllAsync();

                        _apiresponse.data=_mapper.Map<List<StudentDTO>>(students);
                        _apiresponse.status = true;
                        _apiresponse.Statuscode=HttpStatusCode.OK;

                        return Ok(_apiresponse);
            }
            catch (Exception ex)
            {

                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.Statuscode = HttpStatusCode.InternalServerError;
                _apiresponse.status = false;
                return _apiresponse;

            }


           
           
        }

        [HttpGet("{id:int}", Name = "GetStudntById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiResponse>> GetStudntById(int id)
        {
            try
            {
                if (id <= 0)
                {

                    return BadRequest();
                }


                var studentcreation = await _studentrepo.GetAsync(student => student.Id == id);

                if (studentcreation == null)
                {
                    //_logger.LogError("Student not found with log id");
                    return NotFound();
                }

                _apiresponse.data = _mapper.Map<StudentDTO>(studentcreation);
                _apiresponse.status = true;
                _apiresponse.Statuscode = HttpStatusCode.OK;

                return Ok(_apiresponse);
            }
            catch (Exception ex)
            {

                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.Statuscode = HttpStatusCode.InternalServerError;
                _apiresponse.status = false;
                return _apiresponse;

            }
         
        }

        [HttpGet("{name:alpha}", Name = "GetStudntByName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiResponse>> GetStudntByName(string name)
        {
            try
            {
 if (string.IsNullOrEmpty(name))
            {
                // _logger.LogWarning("Provide the name");
                return BadRequest();
            }


            var student =await _studentrepo.GetAsync(student => student.Name.ToLower().Contains(name));
            if (student == null)
            {
                return NotFound();
            }

            _apiresponse.data = _mapper.Map<StudentDTO>(student);
            _apiresponse.status = true;
            _apiresponse.Statuscode = HttpStatusCode.OK;

            return Ok(_apiresponse);
            }
            catch (Exception ex)
            {

                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.Statuscode = HttpStatusCode.InternalServerError;
                _apiresponse.status = false;
                return _apiresponse;

            }
           
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<ApiResponse>> DeleteStudntById(int id)
        {
            try
            {
             if (id==null)
                        {
                            return BadRequest();
                        }
                        var stud = await _studentrepo.GetAsync(student => student.Id == id);
                        if (stud == null)
                        {
                            return NotFound($" the value is not found at {id}");
                        }

                       await _studentrepo.DeleteAsync(stud);

                        _apiresponse.data = true;
                        _apiresponse.status = true;
                        _apiresponse.Statuscode = HttpStatusCode.OK;

                        // await _dbContext.SaveChangesAsync();
                        return Ok(_apiresponse);
            }
            catch (Exception ex)
            {

                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.Statuscode = HttpStatusCode.InternalServerError;
                _apiresponse.status = false;
                return _apiresponse;

            }

           
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiResponse>> UpdateStudent([FromBody] StudentDTO dto)

        {

            try
            {
              if (dto == null || dto.Id <= 0)
                        {
                            return BadRequest();
                        }
                        var Existingstudennt =await _studentrepo.GetAsync(student => student.Id==dto.Id,true);
                        if (Existingstudennt == null)
                        {
                            return NotFound();
                        }

                        var newrecord = _mapper.Map<Student>(dto);
                        await _studentrepo.UpdateAsync(newrecord);

                        return NoContent();
            }
            catch (Exception ex)
            {

                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.Statuscode = HttpStatusCode.InternalServerError;
                _apiresponse.status = false;
                return _apiresponse;

            }

          
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task< ActionResult<ApiResponse>> UpdatePartialStudent(int id, [FromBody] JsonPatchDocument<StudentDTO> patchdocment)
        {
            try
            {
               if (patchdocment == null || id <= 0)
                        {
                            return BadRequest();
                        }
                        var Existingstudennt =await _studentrepo.GetAsync(student => student.Id == id, true);
                        if (Existingstudennt == null)
                        {
                            return NotFound();
                        }
                        var newStudentDto = _mapper.Map<StudentDTO>(Existingstudennt);

                        patchdocment.ApplyTo(newStudentDto,ModelState);
                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }

                        Existingstudennt = _mapper.Map<Student>(newStudentDto);

                        await _studentrepo.UpdateAsync(Existingstudennt);

                        /*_dbContext.students.Update(Existingstudennt);

                       *//* Existingstudennt.Name = newStudentDto.Name;
                        Existingstudennt.Email = newStudentDto.Email;
                        Existingstudennt.Address = newStudentDto.Address;*//*


                        await _dbContext.SaveChangesAsync();*/
                        return NoContent();
            }
            catch (Exception ex)
            {

                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.Statuscode = HttpStatusCode.InternalServerError;
                _apiresponse.status = false;
                return _apiresponse;

            }


         
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiResponse>> CreateStudent([FromBody] StudentDTO dto)
        {
            try
            {
               if (dto == null)
                        {
                            return BadRequest();
                        }

                        //int newId = _dbContext.students.LastOrDefault().Id + 1;

                        Student std = _mapper.Map<Student>(dto);
                       var studentaftercreatin= await _studentrepo.CreateAsync(std);

                        dto.Id = studentaftercreatin.Id;

                        _apiresponse.data = dto;
                        _apiresponse.status = true;
                        _apiresponse.Statuscode = HttpStatusCode.OK;


                        return CreatedAtRoute("GetStudntById", new { id = dto.Id }, _apiresponse);
                        return Ok(dto);
            }
            catch (Exception ex)
            {

                _apiresponse.Errors.Add(ex.Message);
                _apiresponse.Statuscode = HttpStatusCode.InternalServerError;
                _apiresponse.status = false;
                return _apiresponse;

            }

         
        }
    }
}
using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Customers.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        #region Constants
        private readonly Serilog.ILogger _Logger;
        private readonly ICustomersService _Service;
        #endregion

        #region Costrollers
        public CustomersController(Serilog.ILogger logger, ICustomersService service)
        {
            _Logger = logger;
            _Service = service;
        }
        #endregion

        #region Methods
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] uint id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Problem( statusCode: (int)HttpStatusCode.BadRequest);
                }

                var entity = await _Service.GetByIdAsync(id);

                if (entity?.Id == null)
                {
                    return Problem(statusCode: (int)HttpStatusCode.NoContent);
                }

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.ToString());
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByEmailAsync([FromQuery] string email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Problem(statusCode: (int)HttpStatusCode.BadRequest);
                }

                var entity = await _Service.GetByEmailAsync(email);

                if (entity?.Id == null)
                {
                    return Problem(statusCode: (int)HttpStatusCode.NoContent);
                }

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.ToString());
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> ListAsync([FromQuery] uint page, [FromQuery] uint pageLength)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Problem(statusCode: (int)HttpStatusCode.BadRequest);
                }

                var entity = await _Service.ListAsync(page, pageLength);

                if ((entity?.List?.Count ?? 0) == 0)
                {
                    return NoContent();
                }

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.ToString());
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CustomerEntity entity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Problem(statusCode: (int)HttpStatusCode.BadRequest);
                }

                var newEntity = await _Service.AddAsync(entity);

                if (newEntity?.Id == null)
                {
                    return Problem(statusCode: (int)HttpStatusCode.UnprocessableEntity);
                }

                return Created("", newEntity);
            }
            catch(Exception ex)
            {
                _Logger.Error(ex.ToString());
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }


        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromQuery] uint id, [FromBody] CustomerEntity entity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Problem(statusCode: (int)HttpStatusCode.BadRequest);
                }

                entity.Id = id;
                var newEntity = await _Service.UpdateAsync(entity);

                if (newEntity?.Id == null)
                {
                    return Problem(statusCode: (int)HttpStatusCode.UnprocessableEntity);
                }

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.ToString());
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }


        [AllowAnonymous]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] uint id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Problem(statusCode: (int)HttpStatusCode.BadRequest);
                }

                var deletedId = await _Service.DeleteAsync(id);

                if (deletedId == null)
                {
                    return NoContent();
                }

                return Ok(id);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.ToString());
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion
    }
}

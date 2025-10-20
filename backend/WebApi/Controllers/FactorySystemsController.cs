using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Handlers;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/factory-systems")]
    public class FactorySystemsController : ControllerBase
    {
        private readonly FactorySystemsDbContext _context;
        private readonly IValidator<SystemsDTO> _validator;
        private readonly string _notFoundMessage = "Entity not found";


        public FactorySystemsController(FactorySystemsDbContext context, IValidator<SystemsDTO> validator)
        {
            _context = context;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<List<SystemsDTO>>> GetAllSystems(CancellationToken cancellationToken = default)
        {   
            return Ok(await _context.SystemsDbSet.AsNoTracking().ToListAsync(cancellationToken));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemsDTO>> GetSystem([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            SystemsDTO application = await GetSystemById(id, cancellationToken) ?? throw new ApiException(StatusCodes.Status404NotFound, _notFoundMessage);
                
            return Ok(application);
        }

        [HttpPost]
        public async Task<ActionResult<SystemsDTO>> PostSystem([FromBody] SystemsDTO system, CancellationToken cancellationToken = default)
        {
            ValidationResult result = await _validator.ValidateAsync(system, cancellationToken);

            HandleValidationResult(result);

            system.Id = Guid.NewGuid();

            _context.Entry(system).State = EntityState.Added;
            _context.SystemsDbSet.Add(system);

            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction("GetSystem", new { id = system.Id }, system);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SystemsDTO>> PutSystem([FromRoute] Guid id, [FromBody] SystemsDTO system, CancellationToken cancellationToken = default)
        {
            ValidationResult result = await _validator.ValidateAsync(system, cancellationToken);

            HandleValidationResult(result);

            SystemsDTO existentSystem = await GetSystemById(id, cancellationToken) ?? throw new ApiException(StatusCodes.Status404NotFound, _notFoundMessage);

            system.Id = id;
            
            _context.Entry(existentSystem).CurrentValues.SetValues(system);

            await _context.SaveChangesAsync(cancellationToken);

            return Ok(system);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSystem([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            SystemsDTO existentSystem = await GetSystemById(id, cancellationToken) ?? throw new ApiException(StatusCodes.Status404NotFound, _notFoundMessage);

            _context.SystemsDbSet.Remove(existentSystem);

            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private void HandleValidationResult(ValidationResult result)
        {
            if (!result.IsValid)
            {
                throw new ApiException(StatusCodes.Status400BadRequest, result.Errors.FirstOrDefault().ErrorMessage);
            }
        }

        private async Task<SystemsDTO> GetSystemById(Guid id, CancellationToken cancellationToken) => await _context.SystemsDbSet.FindAsync(id, cancellationToken);
    }
}

using OpenERP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OpenERP.Services;
using OpenERP.ErpDbContext.DataModel;
using Microsoft.AspNetCore.Authorization;


namespace OpenERP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartController : ControllerBase
    {
        private readonly OpenERPContext _context;
        private readonly ILogger<PartController> _logger;

        public PartController(OpenERPContext context, ILogger<PartController> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Part> GetById(string partNum)
        {
            try
            {

                var isPartValid = _context.Parts.Where(p => p.PartNum.Equals(partNum)).Any();
                if(isPartValid) return Ok();
                else return NotFound();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to return Part via GetById - {ex.ToString()}");
                return BadRequest($"Failed to return Part via GetById - {ex.ToString()}");
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Part> CreatePart(Part part)
        {
            try
            {
                if(ModelState.IsValid) 
                {
                    _context.Parts.Add(part);
                    return CreatedAtAction(nameof(CreatePart), new {id  = part.PartNum }, part);
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create Part: {ex.ToString()}");
                return BadRequest($"Failed to create Part: {ex.ToString()}");
            }

        }

    }

} 
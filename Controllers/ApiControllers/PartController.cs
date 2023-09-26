using OpenERP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OpenERP.Services;
using OpenERP.ErpDbContext.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using OpenERP.Infrastructure;

namespace OpenERP.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class PartController : ControllerBase
    {
        private readonly ILogger<PartController> _logger;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly IRepository<Part> _partRepository;

        public PartController(ILogger<PartController> logger, UserManager<User> userManager, IRepository<Part> partRepository)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._partRepository = partRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Part>> GetById(string companyID, string partNum)
        {
            try
            {
                //var username = User.Identity.Name; //how to get username for current logged in user
                //var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                

                var isPartValid =  _partRepository.Exists(p => p.CompanyId.Equals(companyID, StringComparison.InvariantCultureIgnoreCase)
                                                                && p.PartNum.Equals(partNum, StringComparison.InvariantCultureIgnoreCase)); //_context.Parts.Where(p => p.PartNum.Equals(partNum)).Any();
                if(isPartValid) return Ok();
                else return BadRequest($"Failed to return Part {partNum} via GetById");

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
        public async Task<ActionResult<Part>> CreatePart(Part part)
        {
            try
            {
                if(ModelState.IsValid) 
                {
                    //change to use the repository to add new part records
                    _partRepository.Add(part);
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
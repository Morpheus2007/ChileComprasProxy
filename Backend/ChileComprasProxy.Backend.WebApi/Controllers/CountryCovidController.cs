using ChileComprasProxy.Backend.Interfaces.Dto;
using ChileComprasProxy.Backend.Interfaces.Interface;
using ChileComprasProxy.Framework.Utilities.Errors;
using ChileComprasProxy.Framework.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ChileComprasProxy.Backend.WebApi.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CountryCovidController : Controller
    {

        private readonly ICountryProxy _countryProxy;

        public CountryCovidController(ICountryProxy countryProxy)
        {
            _countryProxy = countryProxy;
        }



        /// <summary>
        /// Obtiene listado de casos COVID-19
        /// </summary>
        /// <response code="200">Retorna un listado de casos COVID-19 del día actual</response>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(IList<CountrySummaryDto>))]
        [ProducesResponseType(400, Type = typeof(InvalidRequestParameterErrorMessage))]
        [ProducesResponseType(404, Type = typeof(EntityNotFoundErrorMessage))]
        public async Task<IActionResult> Get()
        {
            try
            {


                var countrySummary = await _countryProxy.GetCountrySummary();


                return Ok(countrySummary);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Error);
            }
            catch (InvalidParamenterException e)
            {
                return BadRequest(e.Error);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ErrorService.GetGenericErrorMessage(e.Message));
            }
        }




        /// <summary>
        /// Obtiene listado histórico diario de casos COVID-19
        /// </summary>
        /// <response code="200">Retorna un listado histórico diario de casos COVID-19 de un país dado</response>
        /// <response code="400">Parametro(s) no valido(s)</response>
        /// <returns></returns>
        [HttpGet("{countryName}")]
        [ProducesResponseType(200, Type = typeof(IList<CountryDto>))]
        [ProducesResponseType(400, Type = typeof(InvalidRequestParameterErrorMessage))]
        [ProducesResponseType(404, Type = typeof(EntityNotFoundErrorMessage))]
        public async Task<IActionResult> Get(string countryName)
        {
            try
            {


                var summaryByCountry = await _countryProxy.GetSummaryByCountry(countryName);


                return Ok(summaryByCountry);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Error);
            }
            catch (InvalidParamenterException e)
            {
                return BadRequest(e.Error);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ErrorService.GetGenericErrorMessage(e.Message));
            }
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PakemonReviewWebAPI.Data.DataTransferObject;
using PakemonReviewWebAPI.Interfaces;
using PakemonReviewWebAPI.Models;

namespace PakemonReviewWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper; 

        public CountryController(ICountryRepository countryRepository, 
            IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository
                .GetCountries());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            var country = _mapper.Map<CountryDto>(_countryRepository.
                GetCountry(countryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(country);
        }

        [HttpGet("/owners/{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryOfAnOwner(int ownerId)
        {
            var country = _mapper.Map<CountryDto>(
                _countryRepository.GetCountryByOwnerId(ownerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(country);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry(
            [FromBody] CountryDto createCountry)
        {
            if(createCountry == null)
            {
                return NotFound();
            }

            var country = _countryRepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == createCountry.Name
                .TrimEnd().ToUpper()).FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(createCountry);

            if(!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong" +
                    " while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Created Successfully");
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int countryId, 
            [FromBody] CountryDto updateCountry)
        {
            if(updateCountry == null)
            {
                return BadRequest(ModelState);
            }

            if(!_countryRepository.CountryExists(updateCountry.Id))
            {
                return NotFound(ModelState);
            }

            if(countryId != updateCountry.Id)
            {
                return BadRequest(ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(updateCountry);

            if(!_countryRepository.UpdateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong" +
                    " while updating country");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated Successfully");
        }

        [HttpDelete("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCountry(int countryId)
        {
            if(!_countryRepository.CountryExists(countryId))
            {
                return BadRequest(ModelState);
            }

            var country = _countryRepository.GetCountry(countryId);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_countryRepository.DeleteCountry(country))
            {
                ModelState.AddModelError("", "Something went wrong" +
                    " while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Deleted Successfully");
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Data;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Controllers
{
    [ApiController]
    [Route("api/brand")]
    public class BrandController : ControllerBase
    {
        private readonly AppDBContext _db;

        public BrandController(AppDBContext db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetAllBrands()
        {
            try
            {

                var allData = await _db.Brands.ToListAsync();

                if (allData == null)
                {
                    return NoContent();
                }

                return Ok(allData);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<Brand>> CreateBrand([FromBody] Brand Name , [FromBody] Brand Description)
        {
            try
            {
                if (Name == null || Description == null)
                {
                    return BadRequest();
                }

                _db.Brands.Add(Name); // Saved In Memory
                _db.Brands.Add(Description); // Saved In Memory 
                await _db.SaveChangesAsync();  // Push to DB Table

                return Ok(Name + " & " + Description + " : Added Successfully");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Brand>> EditeBrand(int id, [FromBody] Brand UpdateBrand)
        {
            try
            {
                if (id != UpdateBrand.Id)
                {
                    return BadRequest();
                }

                var getOldAData = await _db.Brands.FindAsync(id);

                if (getOldAData == null)
                {
                    return NotFound();
                }

                getOldAData.Name = UpdateBrand.Name;
                getOldAData.Description = UpdateBrand.Description;

                _db.Brands.Update(getOldAData); // Saved In Memory 
                await _db.SaveChangesAsync(); // Push to DB Table



                return Ok(getOldAData.Name + " : Updated Successfully");


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var getData = await _db.Brands.FindAsync(id);

                if (getData == null)
                {
                    return NotFound();
                }


                _db.Brands.Remove(getData); // Saved In Memory 
                await _db.SaveChangesAsync(); // Push to DB Table



                return Ok(getData + " : Deleted Successfully");


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

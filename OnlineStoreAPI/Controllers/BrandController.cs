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
        public async Task<ActionResult<Brand>> CreateBrand([FromBody] Brand NewBrand )
        {
            try
            {
                if (NewBrand == null)
                {
                    return BadRequest();
                }

                _db.Brands.Add(NewBrand); // Saved In Memory 
                await _db.SaveChangesAsync();  // Push to DB Table

                return Ok(NewBrand.Name + " : Added Successfully");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Brand>> EditeBrand(int id, [FromBody] Brand UpdateBrand)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var getOldAData = await _db.Brands.FindAsync(id);

                

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


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var getData = await _db.Brands.FindAsync(id);

              


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

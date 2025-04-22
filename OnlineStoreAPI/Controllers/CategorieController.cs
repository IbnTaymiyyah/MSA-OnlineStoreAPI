using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Data;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Controllers
{
    [ApiController]
    [Route("api/Categorie")]
    public class CategorieController : ControllerBase
    {
        private readonly AppDBContext _db;

        public CategorieController(AppDBContext db)
        {
            _db = db;
        }



        // Get All Categories In DB

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetAllCategories()
        {
            try
            {

                var allData = await _db.Categories.ToListAsync();

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
        public async Task<ActionResult<Categorie>> CreateCategorie([FromBody] Categorie Name)
        {
            try
            {
                if (Name == null)
                {
                    return BadRequest();
                }

                _db.Categories.Add(Name); // Saved In Memory 
                await _db.SaveChangesAsync();  // Push to DB Table

                return Ok(Name + " : Added Successfully");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Categorie>> EditeCategorie(int id, [FromBody] Categorie UpdateCategorie)
        {
            try
            {
                if ( id != UpdateCategorie.Id)
                {
                    return BadRequest();
                }

                var getOldAData = await _db.Categories.FindAsync(id);

                if (getOldAData == null)
                {
                    return NotFound();
                }

                getOldAData.Name = UpdateCategorie.Name;

                _db.Categories.Update(getOldAData); // Saved In Memory 
                await _db.SaveChangesAsync(); // Push to DB Table



                return Ok(getOldAData + " : Updated Successfully");


            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCategorie(int id)
        {
            try
            {
                if ( id == null)
                {
                    return BadRequest();
                }

                var getData = await _db.Categories.FindAsync(id);

                if (getData == null)
                {
                    return NotFound();
                }


                _db.Categories.Remove(getData); // Saved In Memory 
                await _db.SaveChangesAsync(); // Push to DB Table



                return Ok(getData + " : Deleted Successfully");


            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}

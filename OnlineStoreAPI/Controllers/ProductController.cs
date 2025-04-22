using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Data;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly AppDBContext _db;  // Dependency Injection From DB Context Class

        public ProductController (AppDBContext db)
        {
            _db = db;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try
            {

                var allData = await _db.Products.ToListAsync();

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
        
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(int id )
        {
            try
            {

                var getProduct = await _db.Products.FindAsync(id);
               

                if (getProduct == null)
                {
                    return NoContent();
                }

                return Ok(getProduct);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product NewProduct )
        {
            try
            {
                if (NewProduct == null)
                {
                    return BadRequest();
                }

                if (NewProduct.Name == null || NewProduct.Description == null
                        || NewProduct.Price <= null || NewProduct.Count <= null)
                {
                    return BadRequest();

                }
                NewProduct.Availability = NewProduct.Count > 0;

                _db.Products.Add(NewProduct); // Saved In Memory
              
                await _db.SaveChangesAsync();  // Push to DB Table

                return Ok(NewProduct.Name + " : Added Successfully");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> EditeProduct(int id, [FromBody] Product UpdateProduct)
        {
            try
            {
                if (id != UpdateProduct.Id)
                {
                    return BadRequest();
                }

                var getOldAData = await _db.Products.FindAsync(id);

                if (getOldAData == null)
                {
                    return NotFound();
                }

                getOldAData.Name = UpdateProduct.Name;
                getOldAData.Description = UpdateProduct.Description;
                getOldAData.Price = UpdateProduct.Price;
                getOldAData.Count = UpdateProduct.Count;
                getOldAData.Availability = UpdateProduct.Count > 0;

                if (getOldAData == null)
                {
                    return NotFound();
                }

                _db.Products.Update(getOldAData); // Saved In Memory 
                await _db.SaveChangesAsync(); // Push to DB Table



                return Ok(getOldAData.Name + " : Updated Successfully");


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var getData = await _db.Products.FindAsync(id);



                _db.Products.Remove(getData); // Saved In Memory 
                await _db.SaveChangesAsync(); // Push to DB Table



                return Ok(" : Deleted Successfully");


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}

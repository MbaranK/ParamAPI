using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ParamAPI.Data;
using ParamAPI.Models;
using ParamAPI.Models.DTOs;
using System.Diagnostics;

namespace ParamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetAll()
        {
            return Ok(ProductStore.productsList);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDTO> GetbyId(int id)
        {
            if (id == 0)
                return BadRequest();

            var product = ProductStore.productsList.FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);

        }

        [HttpGet]
        [Route("GetQuery")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDTO> GetByIdQuery([FromQuery] int id)
        {
            if (id == 0)
                return BadRequest();
            var product = ProductStore.productsList.FirstOrDefault(x => x.Id == id);
            if (product is null)
                return NotFound();

            return Ok(product);
        }



        [HttpGet]
        [Route("List")]

        public ActionResult<ProductDTO> GetbyName([FromQuery] string name)
        {
            if (name is null)
                return BadRequest();
            var productName = ProductStore.productsList.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            if (productName is null)
                return NotFound();

            return Ok(productName);

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDTO> CreateProduct([FromBody] ProductDTO product)
        {
            //Custom Validation


            if (product == null)
                return BadRequest();

            if (product.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            product.Id = ProductStore.productsList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;


            ProductStore.productsList.Add(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product); // 201 geri döndürüyor.

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDTO product)
        {
            if (id != product.Id || product is null)
                return BadRequest();

            var updateProduct = ProductStore.productsList.FirstOrDefault(x => x.Id == id);

            if (updateProduct is null)
                return BadRequest();

            updateProduct.Name = product.Name;
            updateProduct.Price = product.Price;
            updateProduct.Stock = product.Stock;

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProduct(int id)
        {
            if (id == 0)
                return BadRequest();

            var deleteProduct = ProductStore.productsList.FirstOrDefault(x => x.Id == id);

            if (deleteProduct is null)
                return NotFound();

            ProductStore.productsList.Remove(deleteProduct);

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult UpdatewPatch(int id , JsonPatchDocument<ProductDTO> product)
        {
            if (id == 0 || product is null)
                return BadRequest();

            var updateProduct = ProductStore.productsList.FirstOrDefault(x => x.Id == id);
            if (updateProduct is null)
                return BadRequest();
            product.ApplyTo(updateProduct, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();


        }
    }
}

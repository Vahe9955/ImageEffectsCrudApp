using ImageEffectsCrudApp.Data;
using ImageEffectsCrudApp.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImageEffectsCrudApp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ImagesCrudController : ControllerBase
    {
        private ApplicationContext _context;
        public ImagesCrudController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            var images = await _context.Images.ToListAsync();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }

        [HttpPost]
        public async Task<ActionResult<Image>> CreateImage(ImageRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var addedImage = new Image()
            {
                Name = model.Name,
                Px = model.Px,
                Effects = model.Effects,
            };

            await _context.Images.AddAsync(addedImage);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new Image { Id = addedImage.Id }, addedImage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ImageRequestModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var data = await _context.Images.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            data = new Image()
            {
                Name = product.Name,
                Px = product.Px,
                Effects = product.Effects,
            };

            _context.Images.Update(data);

            await _context.SaveChangesAsync();


            return NoContent();
        }

        [HttpPut("UpdateMultiple")]
        public async Task<IActionResult> UpdateMultipleProducts(MultipleImagesUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            foreach (var product in request.Images)
            {
                var data = await _context.Images.FindAsync(product.Id);
                if (data == null)
                {
                    return NotFound($"Image with ID {product.Id} not found.");
                }

                data.Name = product.Name;
                data.Px = product.Px;
                data.Effects = product.Effects;

                _context.Images.Update(data);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Images.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Images.Remove(product);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
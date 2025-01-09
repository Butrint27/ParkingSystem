using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Core.DbContext;
using ParkingSystem.Core.Dtos.PaymentMethod;
using ParkingSystem.Core.Entities;

namespace ParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PaymentMethodController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreatePaymentMethod([FromBody] PaymentMethodDto dto)
        {
            var newPaymentMethod = _mapper.Map<PaymentMethod>(dto);

            await _context.PaymentMethods.AddAsync(newPaymentMethod);
            await _context.SaveChangesAsync();

            return Ok("Payment Method Created Successfully");
        }

        // Read All
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<PaymentMethodDto>>> GetAllPaymentMethods()
        {
            var paymentMethods = await _context.PaymentMethods
                .Include(pm => pm.Payments)
                .ToListAsync();

            var convertedPaymentMethods = _mapper.Map<IEnumerable<PaymentMethodDto>>(paymentMethods);
            return Ok(convertedPaymentMethods);
        }

        // Read by ID
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PaymentMethodDto>> GetPaymentMethodById([FromRoute] int id)
        {
            var paymentMethod = await _context.PaymentMethods
                .Include(pm => pm.Payments)
                .FirstOrDefaultAsync(pm => pm.id == id);

            if (paymentMethod is null)
            {
                return NotFound("Payment Method Not Found");
            }

            var convertedPaymentMethod = _mapper.Map<PaymentMethodDto>(paymentMethod);
            return Ok(convertedPaymentMethod);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePaymentMethod([FromRoute] int id, [FromBody] PaymentMethodDto dto)
        {
            var paymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(pm => pm.id == id);
            if (paymentMethod is null)
            {
                return NotFound("Payment Method Not Found");
            }

            paymentMethod.type = dto.Type;
            paymentMethod.details = dto.Details;

            await _context.SaveChangesAsync();
            return Ok("Payment Method Updated Successfully");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePaymentMethod([FromRoute] int id)
        {
            var paymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(pm => pm.id == id);
            if (paymentMethod is null)
            {
                return NotFound("Payment Method Not Found");
            }

            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync();
            return Ok("Payment Method Deleted");
        }
    }
}

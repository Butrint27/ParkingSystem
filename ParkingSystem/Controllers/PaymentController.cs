using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Core.DbContext;
using ParkingSystem.Core.Entities;

namespace ParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PaymentController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentDto dto)
        {
            var newPayment = _mapper.Map<Payment>(dto);
            newPayment.date = DateTime.UtcNow;

            await _context.Payments.AddAsync(newPayment);
            await _context.SaveChangesAsync();

            return Ok("Payment Created Successfully");
        }

        // Read All
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAllPayments()
        {
            var payments = await _context.Payments
                .Include(p => p.paymentMethod)
                .ToListAsync();

            var convertedPayments = _mapper.Map<IEnumerable<PaymentDto>>(payments);
            return Ok(convertedPayments);
        }

        // Read by ID
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPaymentById([FromRoute] int id)
        {
            var payment = await _context.Payments
                .Include(p => p.paymentMethod)
                .FirstOrDefaultAsync(p => p.id == id);

            if (payment is null)
            {
                return NotFound("Payment Not Found");
            }

            var convertedPayment = _mapper.Map<PaymentDto>(payment);
            return Ok(convertedPayment);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePayment([FromRoute] int id, [FromBody] PaymentDto dto)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.id == id);
            if (payment is null)
            {
                return NotFound("Payment Not Found");
            }

            payment.amount = dto.Amount;
            payment.status = dto.Status; 

            await _context.SaveChangesAsync();
            return Ok("Payment Updated Successfully");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePayment([FromRoute] int id)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.id == id);
            if (payment is null)
            {
                return NotFound("Payment Not Found");
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return Ok("Payment Deleted");
        }
    }
}

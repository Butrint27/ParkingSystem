using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Core.DbContext;
using ParkingSystem.Core.Dtos.Invoice;
using ParkingSystem.Core.Entities;

namespace ParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDto dto)
        {
            Invoice newInvoice = _mapper.Map<Invoice>(dto);

            await _context.Invoices.AddAsync(newInvoice);
            await _context.SaveChangesAsync();

            return Ok("Invoice Created Successfully");
        }

        // Read All
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetAllInvoices()
        {
            var invoices = await _context.Invoices.Include(i => i.Payment).ToListAsync();
            var convertedInvoices = _mapper.Map<IEnumerable<InvoiceDto>>(invoices);

            return Ok(convertedInvoices);
        }

        // Read by ID
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<InvoiceDto>> GetInvoiceById([FromRoute] int id)
        {
            var invoice = await _context.Invoices.Include(i => i.Payment).FirstOrDefaultAsync(i => i.Id == id);

            if (invoice is null)
            {
                return NotFound("Invoice Not Found");
            }

            var convertedInvoice = _mapper.Map<InvoiceDto>(invoice);
            return Ok(convertedInvoice);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateInvoice([FromRoute] int id, [FromBody] InvoiceDto dto)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == id);
            if (invoice is null)
            {
                return NotFound("Invoice Not Found");
            }

            invoice.InvoiceNumber = dto.InvoiceNumber;
            invoice.DateGenerated = dto.DateGenerated;
            invoice.TotalAmount = dto.TotalAmount;
            invoice.PaymentId = dto.PaymentId;

            await _context.SaveChangesAsync();
            return Ok("Invoice Updated Successfully");
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteInvoice([FromRoute] int id)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == id);
            if (invoice is null)
            {
                return NotFound("Invoice Not Found");
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return Ok("Invoice Deleted");
        }
    }
}

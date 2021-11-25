using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ensek.Models;
using Ensek.Services;
using Ensek.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CsvHelper;
using Ensek.DTOs;

namespace Ensek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeterReadingController : ControllerBase
    {
        private readonly ILogger<MeterReadingController> _logger;
        private readonly MeterReadingContext _context;
        private readonly IMeterReadingFileService _meterReadingFileService;
        private readonly IMeterReadingPersister _meterReadingPersister;

        public MeterReadingController(ILogger<MeterReadingController> logger, MeterReadingContext context, IMeterReadingFileService meterReadingFileService, IMeterReadingPersister meterReadingPersister)
        {
            _logger = logger;
            _context = context;
            _meterReadingFileService = meterReadingFileService;
            _meterReadingPersister = meterReadingPersister;
        }

        [HttpPost]
        public async Task<CreateMeterReadingsResponse> UploadReadings([FromForm] IFormFile file)
        {
            var meterReadings = await _meterReadingFileService.ReadFromFile(file);
            var created = await _meterReadingPersister.Persist(meterReadings.Where(r => !r.Errors.Any()));
            var response = new CreateMeterReadingsResponse();
            response.TotalRead = meterReadings.Count();
            response.Created = created;
            var failed = meterReadings.Where(r => r.Errors.Any()).ToList();
            response.Failed = failed.Count;
            response.Errors = failed;
            return response;
        }

        [HttpGet]
        public async Task<IEnumerable<Account>> Get()
        {
            return await _context.Accounts.ToListAsync();
        }
    }
}

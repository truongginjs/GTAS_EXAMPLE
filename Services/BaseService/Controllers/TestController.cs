using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BaseService.DTOs;
using BaseService.Infrastructure.Repositories;
using BaseService.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository _repo;
        private readonly IMapper _mapper;

        public TestController(ITestRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestResponseDTO>>> GetsAsync()
        {
            var data = await _repo.GetsAsync();
            var result = _mapper.Map<IEnumerable<TestResponseDTO>>(data);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TestResponseDTO>> GetAsync(Guid id)
        {
            var data = await _repo.GetAsync(id);
            if (data == null) return NotFound();
            var result = _mapper.Map<TestResponseDTO>(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TestResponseDTO>> CreateAsync([FromBody] TestRequestDTO test)
        {
            var input = _mapper.Map<Test>(test);
            var data = await _repo.CreateAsync(input);
            if (data == null) return BadRequest();
            var result = _mapper.Map<TestResponseDTO>(data);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TestResponseDTO>> UpdateAsync(Guid id, [FromBody] TestRequestDTO test)
        {
            var input = _mapper.Map<Test>(test);
            var data = await _repo.UpdateAsync(id, input);
            if (data == null) return BadRequest();
            var result = _mapper.Map<TestResponseDTO>(data);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TestResponseDTO>> UpdateAsync(Guid id)
        {
            var data = await _repo.DeleteAsync(id);
            if (data == null) return BadRequest();
            var result = _mapper.Map<TestResponseDTO>(data);
            return Ok(result);
        }
    }
}
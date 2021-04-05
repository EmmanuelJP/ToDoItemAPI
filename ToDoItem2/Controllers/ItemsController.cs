using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoItem2.Model.Entities;
using TodoItem2.Services.Repositories;
using ToDoItem2.Dtos;

namespace ToDoItem2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ItemDto> _validator;

        public ItemsController(ILogger<ItemsController> logger, IItemRepository itemRepository, IMapper mapper, IValidator<ItemDto> validator)
        {
            _logger = logger;
            _itemRepository = itemRepository;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _itemRepository.GetAll();
            var itemDtos = _mapper.Map<IEnumerable<ItemDto>>(items);
            return Ok(itemDtos);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _itemRepository.GetById(id);
            if (item is null)
                return NotFound();

            var itemDto = _mapper.Map<ItemDto>(item);
            return Ok(itemDto);
        }
        [HttpPost]
        public IActionResult Create(ItemDto itemDto)
        {
            var validationResult = _validator.Validate(itemDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); 

            var item = _mapper.Map<Item>(itemDto);
            var itemDtoResult = _itemRepository.Create(item);
            return Created("",itemDtoResult);
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id,[FromBody] ItemDto itemDto)
        {
            var item = _itemRepository.GetById(id);
            if (item is null)
                return NotFound();

            _mapper.Map(itemDto, item);

             _itemRepository.Update(item);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _itemRepository.GetById(id);
            if (item is null)
                return NotFound();
            var itemResult = _itemRepository.Delete(id);
            var itemDto = _mapper.Map<ItemDto>(itemResult);
            return Ok(itemDto);
        }
    }
}

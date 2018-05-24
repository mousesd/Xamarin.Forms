using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormsApp31.WebApi.Models;
using FormsApp31.WebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormsApp31.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        #region == Fields & Constructors ==
        private readonly ITodoRepository toDoRepository;

        public TodoController(ITodoRepository toDoRepository)
        {
            this.toDoRepository = toDoRepository;
        }
        #endregion

        #region == Web API methods ==
        [HttpGet]
        public IActionResult GetAllToDoList()
        {
            return Ok(toDoRepository.All);
        }

        [HttpPost]
        public IActionResult CreateToDo([FromBody] TodoItem item)
        {
            if (item == null || !ModelState.IsValid)
                return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());

            try
            {
                if (toDoRepository.DoesItemExist(item.Id))
                    return StatusCode(StatusCodes.Status409Conflict, ErrorCode.TodoItemIdInUse);

                toDoRepository.Insert(item);
                return Ok(item);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotCreateItem.ToString());
            }
        }

        [HttpPut]
        public IActionResult UpdateToDo([FromBody] TodoItem item)
        {
            if (item == null || !ModelState.IsValid)
                return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());

            try
            {
                if (toDoRepository.Find(item.Id) == null)
                    return NotFound(ErrorCode.RecordNotFound.ToString());

                toDoRepository.Update(item);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotUpdateItem.ToString());
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(string id)
        {
            try
            {
                var item = toDoRepository.Find(id);
                if (item == null)
                    return NotFound(ErrorCode.RecordNotFound.ToString());

                toDoRepository.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotDeleteItem.ToString());
            }
        }
        #endregion
    }

    public enum ErrorCode
    {
        TodoItemNameAndNotesRequired,
        TodoItemIdInUse,
        RecordNotFound,
        CouldNotCreateItem,
        CouldNotUpdateItem,
        CouldNotDeleteItem
    }
}

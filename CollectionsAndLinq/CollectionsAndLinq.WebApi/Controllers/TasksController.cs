﻿using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly IDataProcessingService _service;

        public TasksController(IDataProcessingService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<TaskDto>> GetTasks()
        {
            return View();
        }

        [HttpGet]
        public ActionResult<TaskDto> GetTask(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult<BL.Entities.Task> AddTask([FromBody] BL.Entities.Task task)
        {
            return View();
        }

        [HttpPut]
        public ActionResult<BL.Entities.Task> EditTask([FromBody] BL.Entities.Task task)
        {
            return View();
        }

        [HttpDelete]
        public ActionResult<TaskDto> DeleteTask(int id)
        {
            return View();
        }
    }
}
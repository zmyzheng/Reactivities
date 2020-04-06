using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    // [Route("api/[controller]")]
    // [ApiController]
    public class ActivitiesController : BaseController
    {
        // private readonly IMediator _mediator;

        // public ActivitiesController(IMediator mediator)
        // {
        //     this._mediator = mediator;
        // }

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> List(CancellationToken ct) 
        {
            return await Mediator.Send(new List.Query(), ct);
        }

        [HttpGet("{id}")]
        // [Authorize]  添加了policy就不用这样一个一个加了，因为大部分API都需要authorize，我们只需要给不需要authorize的API加注解就行
        public async Task<ActionResult<Activity>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command { Id = id});
        }
    }
}
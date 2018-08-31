using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tryingWithAngularChat.Hubs;
using tryingWithAngularChat.Models;
using tryingWithAngularChat.Services;

namespace tryingWithAngularChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IChatService iservice;


        public ChatController(IChatService c)
        {
            iservice = c;
        }

        // creating a workspace
        [HttpPost]
        [Route("workspaces")]
        public IActionResult CreateWorkspace([FromBody] string workspace) // frombody workspace object or string name
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            iservice.CreateWorkspace(workspace);
            return new ObjectResult(workspace);
        }

        // getting all the workspaces
        [HttpGet]
        [Route("workspaces")]
        public IActionResult GetAllWorkspace()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ListofWorkspace = iservice.GetAllWorkspaces();
            return new ObjectResult(ListofWorkspace);
        }
        // getting all the workspaces
        [HttpGet]
        [Route("workspaces/{id:length(24)}")]
        public IActionResult GetWorkspaceById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Workspace = iservice.GetWorkspaceById(id);
            if (Workspace == null)
            {
                return NotFound();
            }
            return new ObjectResult(Workspace);
        }

        // deleting a workspace by id 
        [HttpDelete]
        [Route("workspaces/{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var workspaceToDelete = iservice.GetWorkspaceById(id);
            if (workspaceToDelete == null)
            {
                return NotFound();
            }

            iservice.DeleteWorkspace(workspaceToDelete.workspaceId);
            return new OkResult();
        }
        // creating a Channel
        [HttpPut]
        [Route("workspaces/{id:length(24)}")]
        public IActionResult CreateChannelInWorkSpace([FromBody] Channel channel, string id) // frombody workspace object or string name
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            iservice.CreateChannel(channel,id);
            return new ObjectResult(channel);
        }
        // Adding a user to a channel
        [HttpPut]
        [Route("workspaces/channel/{channelId}")]
        public IActionResult AddUserToChannel([FromBody] User user, string ChannelId) // frombody workspace object or string name
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            iservice.AddUserToChannel(user, ChannelId);
            return new ObjectResult(user);
        }

        // getting all the users
        [HttpGet]
        [Route("user/{workspaceName}")]
        public IActionResult GetAllUsersInWorkspace(string workspaceName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ListofWorkspace = iservice.GetAllUsersInWorkspace(workspaceName);
            return new ObjectResult(ListofWorkspace);
        }

        // Adding a user to a workspace
        [HttpPut]
        [Route("workspaces/user/{workspaceName}")]
        public IActionResult AddUserToWorkspace([FromBody] User user, string workspaceName) // frombody workspace object or string name
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userAdded = iservice.AddUserToWorkspace(workspaceName, user);
            return new ObjectResult(userAdded);
        }


    }
}
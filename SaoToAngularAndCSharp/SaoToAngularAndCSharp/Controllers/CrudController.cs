using Microsoft.AspNetCore.Mvc;
using SaoToAngularAndCSharp.DAL.Models;
using SaoToAngularAndCSharp.DAL.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaoToAngularAndCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly IRepostory myRep;
        public CrudController(IRepostory myRep)
        {
            this.myRep = myRep;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser(UserModel newUser)
        {
            var id = await this.myRep.MakeNewUser(newUser.Username, newUser.Password, newUser.UserDisplay);
            return Ok(id);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> Getall()
        {
            var all = await this.myRep.getAllUsers();
            return Ok(all);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> GetUserByLogin(UserModel newUser)
        {
            var user = await this.myRep.GetUserByLogin(newUser.Username, newUser.Password);
            if (user == null) return Ok(false);
            return Ok(user);
        }

        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage(Message msg)
        {
            await this.myRep.SendMessage(msg.sendById, msg.sentToId, msg.message);
            return Ok();
        }

    }
}

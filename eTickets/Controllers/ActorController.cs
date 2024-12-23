using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Models;
using eTickets.Repositores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorController : Controller
    {
  
        private readonly IUnitOfWork unitOfWork;

        public ActorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await unitOfWork.Actors.GetAll();
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureUrl,Bio")]Actor actor)
        {
            if(!ModelState.IsValid) 
            {
                return View(actor);
            }
            unitOfWork.Actors.Add(actor);
            return RedirectToAction(nameof(Index));

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var actordetails = await unitOfWork.Actors.GetById(id);
            if(actordetails == null)
            {
                return NotFound();
            }
            return View(actordetails);  
        }

        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await unitOfWork.Actors.GetById(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureUrl,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await unitOfWork.Actors.Update(id, actor);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await unitOfWork.Actors.GetById(id);
            if (actor == null)
            {
                return NotFound(nameof(actor));
            }
            return View(actor);
        }

        [HttpPost , ActionName("Delete")] 
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actordetails = await unitOfWork.Actors.GetById(id);
            if (actordetails == null)
            {
                return NotFound();
            }
            unitOfWork.Actors.Delete(id);
            return RedirectToAction(nameof(Index));

        }
    }
}

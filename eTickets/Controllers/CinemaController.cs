using eTickets.Data.Static;
using eTickets.Models;
using eTickets.Repositores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemaController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CinemaController(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var cinemas = await unitOfWork.Cinema.GetAll();
            return View(cinemas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Discription")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await unitOfWork.Cinema.Add(cinema);
            return RedirectToAction(nameof(Index));
        }


        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await unitOfWork.Cinema.GetById(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await unitOfWork.Cinema.GetById(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Discription")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await unitOfWork.Cinema.Update(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await unitOfWork.Cinema.GetById(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var cinemaDetails = await unitOfWork.Cinema.GetById(id);
            if (cinemaDetails == null) return View("NotFound");

            await unitOfWork.Cinema.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

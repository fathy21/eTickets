using eTickets.Models;
using eTickets.Repositores;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ProducerController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProducerController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var Producers =  await unitOfWork.Producers.GetAll(); 
            return View(Producers);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var producerDetails = await unitOfWork.Producers.GetById(Id);   
            if(producerDetails == null)
            {
                return NotFound();
            }

            return View(producerDetails);   
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureUrl,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);

            await unitOfWork.Producers.Add(producer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await unitOfWork.Producers.GetById(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureUrl,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await unitOfWork.Producers.Update(id, producer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await unitOfWork.Producers.GetById(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await unitOfWork.Producers.GetById(id);
            if (producerDetails == null) return View("NotFound");

            await unitOfWork.Producers.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostImagenesAzure.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PostImagenesAzure.Controllers
{
    public class ImagenesController : Controller
    {
        private ServiceStorageBlobs service;

        public ImagenesController(ServiceStorageBlobs service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<string> imagenes = await this.service.GetBlobsAsync("pokemon");
            return View(imagenes);
        }

        public IActionResult UploadImagen()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImagen(String nombreImagen,IFormFile imagenBBDD)
        {
            string extension = imagenBBDD.FileName.Split(".")[1];
            string fileName = nombreImagen.Trim() + "." + extension;
            using (Stream stream = imagenBBDD.OpenReadStream())
            {
                await this.service.UploadBlobAsync("pokemon", fileName, stream);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string nombre)
        {
            await this.service.DeleteBlobAsync("pokemon", nombre);
            return RedirectToAction("Index");
        }
    }
}

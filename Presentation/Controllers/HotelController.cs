using Application.Common.Interfaces;
using Application.Common.Utility;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Controllers
{
  [Authorize(Roles = UserRoles.Role_Admin)]
  public class HotelController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HotelController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
      _unitOfWork = unitOfWork;
      _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
      var hotels = _unitOfWork.Hotel.GetAll();
      return View(hotels);
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Create(Hotel hotel)
    {
      if (hotel.Name == hotel.Description)
      {
        ModelState.AddModelError("name", "Tên khách sạn nhập vào không được trùng với mô tả");
      }
      if (ModelState.IsValid)
      {

        if (hotel.Image != null)
        {
          string fileName = Guid.NewGuid().ToString()+Path.GetExtension(hotel.Image.FileName);
          string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"img\hotel");

          using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
          {
            hotel.Image.CopyTo(fileStream);
          }

          hotel.ImageUrl = @"\img\hotel\" + fileName;

          //using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
          //hotel.Image.CopyTo(fileStream);
        }
        else
        {
          hotel.ImageUrl = "https://pix6.agoda.net/geo/city/16440/1_16440_02.jpg";
        }

        _unitOfWork.Hotel.Add(hotel);
        //_unitOfWork.Hotel.Save();
        _unitOfWork.Save();
        return RedirectToAction("Index", "Hotel");
      }
      return View();
    }

    public IActionResult Update(int hotelId)
    {
      Hotel? hotel = _unitOfWork.Hotel.Get(h => h.Id == hotelId);

      if (hotel == null)
      {
        return RedirectToAction("Error", "Home");
      }
      return View(hotel);
    }

    [HttpPost]
    public IActionResult Update(Hotel hotel)
    {
      if (ModelState.IsValid && hotel.Id > 0)
      {
        if (hotel.Image != null)
        {
          string fileName = Guid.NewGuid().ToString() + Path.GetExtension(hotel.Image.FileName);
          string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"img\hotel");

          if (!string.IsNullOrEmpty(hotel.ImageUrl))
          {
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, hotel.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
              System.IO.File.Delete(oldImagePath);
            }
          }


          using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
          {
            hotel.Image.CopyTo(fileStream);
          }

          hotel.ImageUrl = @"\img\hotel\" + fileName;
        }

        else
        {
          hotel.ImageUrl = "https://pix6.agoda.net/geo/city/16440/1_16440_02.jpg";
        }

        _unitOfWork.Hotel.Update(hotel);
        //_unitOfWork.Hotel.Save();
        _unitOfWork.Save();
        return RedirectToAction("Index", "Hotel");
      }
      return View();
    }

    public IActionResult Delete(int hotelId)
    {
      Hotel? hotel = _unitOfWork.Hotel.Get(h => h.Id == hotelId);
      if (hotel == null)
      {
        return RedirectToAction("Error", "Home");
      }

      return View(hotel);
    }

    [HttpPost]
    public IActionResult Delete(Hotel hotel)
    {
      var hotelFromDatabase = _unitOfWork.Hotel.Get(h => h.Id == hotel.Id);
      if (hotelFromDatabase != null)
      {
        if (!string.IsNullOrEmpty(hotelFromDatabase.ImageUrl))
        {
          var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, hotelFromDatabase.ImageUrl.TrimStart('\\'));

          if (System.IO.File.Exists(oldImagePath))
          {
            System.IO.File.Delete(oldImagePath);
          }
        }
        _unitOfWork.Hotel.Remove(hotelFromDatabase);
        _unitOfWork.Save();
        TempData["success"] = "Đã xóa hoàn tất";
        return RedirectToAction("Index", "Hotel");
      }
      TempData["error"] = "Không xóa được thông tin khách sạn";
      return View();
    }
  }
}

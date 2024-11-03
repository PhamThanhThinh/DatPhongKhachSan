using Application.Common.Interfaces;
using Application.Common.Utility;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Models;

namespace Presentation.Controllers
{
  [Authorize(Roles = UserRoles.Role_Admin)]
  public class AmenityController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public AmenityController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    
    public IActionResult Index()
    {
      var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Hotel");
      return View(amenities);
    }

    public IActionResult Create()
    {
      AmenityViewModel amenityViewModel = new()
      {
        HotelList = _unitOfWork.Hotel.GetAll().Select(
        h => new SelectListItem
        {
          Text = h.Name,
          Value = h.Id.ToString()
        }
        )
      };
      return View(amenityViewModel);
    }

    [HttpPost]
    public IActionResult Create(AmenityViewModel amenityViewModel)
    {
      // Nếu ModelState hợp lệ
      if (ModelState.IsValid)
      {
        _unitOfWork.Amenity.Add(amenityViewModel.Amenity);
        _unitOfWork.Save();
        TempData["success"] = "Đã thêm thành công";
        return RedirectToAction("Index", "Amenity");
      }

      amenityViewModel.HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
      {
        Text = h.Name,
        Value = h.Id.ToString()
      });

      // Trả lại view với dữ liệu hiện tại nếu có lỗi
      return View(amenityViewModel);
    }

    public IActionResult Update(int amenityId)
    {
      // Kiểm tra nếu danh sách Hotels null hoặc trống
      var hotels = _unitOfWork.Hotel.GetAll();
      if (hotels == null || !hotels.Any())
      {
        // Trường hợp không tìm thấy danh sách hotels
        TempData["error"] = "Không có dữ liệu khách sạn để hiển thị";
        return RedirectToAction("Error", "Home");
      }

      var amenity = _unitOfWork.Amenity.Get(h => h.Id == amenityId);

      // Khởi tạo ViewModel
      AmenityViewModel amenityViewModel = new()
      {
        HotelList = hotels.Select(h => new SelectListItem
        {
          Text = h.Name,
          Value = h.Id.ToString()
        }).ToList(), // Đảm bảo kết quả là một danh sách
        Amenity = amenity
      };

      return View(amenityViewModel);
    }

    [HttpPost]
    public IActionResult Update(AmenityViewModel amenityViewModel)
    {
      // Kiểm tra ModelState
      if (ModelState.IsValid)
      {
        // Kiểm tra nếu HotelNumber không null
        if (amenityViewModel.Amenity != null)
        {
          _unitOfWork.Amenity.Update(amenityViewModel.Amenity);
          _unitOfWork.Save();  // Lưu thay đổi vào cơ sở dữ liệu
          TempData["success"] = "Đã cập nhật thông tin dịch vụ thành công";
          return RedirectToAction("Index", "Amenity");
        }
        else
        {
          TempData["error"] = "Dữ liệu dịch vụ thêm vào không hợp lệ";
        }
      }

      // Nếu có lỗi trong ModelState hoặc trong quá trình xử lý, lấy lại danh sách hotels
      amenityViewModel.HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
      {
        Text = h.Name,
        Value = h.Id.ToString()
      }).ToList();

      // Trả lại view với dữ liệu hiện tại nếu có lỗi
      return View(amenityViewModel);
    }

    public IActionResult Delete(int amenityId)
    {
      AmenityViewModel amenityViewModel = new()
      {
        HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
        {
          Text = h.Name,
          Value = h.Id.ToString()
        }),
        Amenity = _unitOfWork.Amenity.Get(h => h.Id == amenityId)
      };

      if (amenityViewModel.Amenity == null)
      {
        return RedirectToAction("Error", "Home");
      }

      return View(amenityViewModel);
    }

    [HttpPost]
    public IActionResult Delete(AmenityViewModel amenityViewModel)
    {
      Amenity? itemFromDatabase = _unitOfWork.Amenity
        .Get(h => h.Id == amenityViewModel.Amenity.Id);
      if (itemFromDatabase != null)
      {
        _unitOfWork.Amenity.Remove(itemFromDatabase);
        _unitOfWork.Save();
        TempData["success"] = "Đã xóa hoàn tất";
        return RedirectToAction("Index", "Amenity");
      }
      TempData["error"] = "Không xóa được thông tin dịch vụ của khách sạn";
      return View();
    }
  }
}

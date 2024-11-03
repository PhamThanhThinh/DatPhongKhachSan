using Application.Common.Interfaces;
using Application.Common.Utility;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;

namespace Presentation.Controllers
{
  [Authorize(Roles = UserRoles.Role_Admin)]
  public class HotelNumberController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public HotelNumberController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
      //var hotelNumbers = _db.HotelNumbers.Include(h => h.Hotel).ToList();
      var hotelNumbers = _unitOfWork.HotelNumber.GetAll(includeProperties: "Hotel");
      return View(hotelNumbers);
    }

    public IActionResult Create()
    {
      //List<SelectListItem> list = new List<SelectListItem>();
      HotelNumberViewModel hotelNumberViewModel = new()
      {
        HotelList = _unitOfWork.Hotel.GetAll().Select(
        h => new SelectListItem
        {
          Text = h.Name,
          Value = h.Id.ToString()
        }
        )
      };
      return View(hotelNumberViewModel);
    }

    [HttpPost]
    public IActionResult Create(HotelNumberViewModel hotelNumberViewModel)
    {
      // Kiểm tra xem hotelNumber và HotelNumber bên trong có giá trị null không
      if (hotelNumberViewModel == null || hotelNumberViewModel.HotelNumber == null)
      {
        TempData["error"] = "Thông tin số phòng không hợp lệ.";
        return View(hotelNumberViewModel);
      }

      // Kiểm tra số phòng có bị trùng không
      bool isNumberUnique = _unitOfWork.HotelNumber.Any(h => h.Hotel_Number == hotelNumberViewModel.HotelNumber.Hotel_Number);

      // Nếu ModelState hợp lệ và số phòng không bị trùng
      if (ModelState.IsValid && !isNumberUnique)
      {
        _unitOfWork.HotelNumber.Add(hotelNumberViewModel.HotelNumber);
        _unitOfWork.Save();
        return RedirectToAction("Index", "HotelNumber");
      }

      // Nếu số phòng bị trùng
      if (isNumberUnique)
      {
        TempData["error"] = "Số phòng thêm vào bị trùng";
      }

      hotelNumberViewModel.HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
      {
        Text = h.Name,
        Value = h.Id.ToString()
      });

      // Trả lại view với dữ liệu hiện tại nếu có lỗi
      return View(hotelNumberViewModel);
    }


    //public IActionResult Update(int hotelNumberId)
    //{
    //  HotelNumberViewModel hotelNumberViewModel = new()
    //  {
    //    HotelList = _db.Hotels.ToList().Select(h => new SelectListItem
    //    {
    //      Text = h.Name,
    //      Value = h.Id.ToString()
    //    }),
    //    HotelNumber = _db.HotelNumbers.FirstOrDefault(h => h.Hotel_Number == hotelNumberId)
    //  };
    //  if (hotelNumberViewModel.HotelNumber == null)
    //  {
    //    return RedirectToAction("Error", "Home");
    //  }

    //  return View(hotelNumberViewModel);
    //}

    public IActionResult Update(int hotelNumberId)
    {
      // Kiểm tra nếu danh sách Hotels null hoặc trống
      var hotels = _unitOfWork.Hotel.GetAll();
      if (hotels == null || !hotels.Any())
      {
        // Trường hợp không tìm thấy danh sách hotels
        TempData["error"] = "Không có dữ liệu khách sạn để hiển thị";
        return RedirectToAction("Error", "Home");
      }

      // Tìm HotelNumber theo ID
      var hotelNumber = _unitOfWork.HotelNumber.Get(h => h.Hotel_Number == hotelNumberId);
      if (hotelNumber == null)
      {
        // Nếu không tìm thấy HotelNumber (số phòng khách sạn, đồng thời cũng là khóa chính trong bảng HotelNumber), chuyển hướng đến trang lỗi
        TempData["error"] = "Không tìm thấy số phòng khách sạn";
        return RedirectToAction("Error", "Home");
      }

      // Khởi tạo ViewModel
      HotelNumberViewModel hotelNumberViewModel = new()
      {
        HotelList = hotels.Select(h => new SelectListItem
        {
          Text = h.Name,
          Value = h.Id.ToString()
        }).ToList(), // Đảm bảo kết quả là một danh sách
        HotelNumber = hotelNumber
      };

      return View(hotelNumberViewModel);
    }


    //[HttpPost]
    //public IActionResult Update(HotelNumberViewModel hotelNumberViewModel)
    //{
    //  // Nếu ModelState hợp lệ
    //  if (ModelState.IsValid)
    //  {
    //    _db.HotelNumbers.Update(hotelNumberViewModel.HotelNumber);
    //    _db.SaveChanges();
    //    TempData["success"] = "Số phòng đã được update thành công";
    //    return RedirectToAction("Index", "HotelNumber");
    //  }

    //  hotelNumberViewModel.HotelList = _db.Hotels.ToList().Select(h => new SelectListItem
    //  {
    //    Text = h.Name,
    //    Value = h.Id.ToString()
    //  });

    //  // Trả lại view với dữ liệu hiện tại nếu có lỗi
    //  return View(hotelNumberViewModel);
    //}

    [HttpPost]
    public IActionResult Update(HotelNumberViewModel hotelNumberViewModel)
    {
      // Kiểm tra ModelState
      if (ModelState.IsValid)
      {
        // Kiểm tra nếu HotelNumber không null
        if (hotelNumberViewModel.HotelNumber != null)
        {
          try
          {
            _unitOfWork.HotelNumber.Update(hotelNumberViewModel.HotelNumber);
            _unitOfWork.Save();  // Lưu thay đổi vào cơ sở dữ liệu
            TempData["success"] = "Số phòng đã được cập nhật thành công";
            return RedirectToAction("Index", "HotelNumber");
          }
          catch (Exception ex)
          {
            // Log lỗi (nếu có hệ thống log) và thông báo lỗi cho người dùng
            TempData["error"] = "Có lỗi xảy ra khi cập nhật số phòng: " + ex.Message;
          }
        }
        else
        {
          TempData["error"] = "Dữ liệu phòng khách sạn không hợp lệ";
        }
      }

      // Nếu có lỗi trong ModelState hoặc trong quá trình xử lý, lấy lại danh sách hotels
      hotelNumberViewModel.HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
      {
        Text = h.Name,
        Value = h.Id.ToString()
      }).ToList();

      // Trả lại view với dữ liệu hiện tại nếu có lỗi
      return View(hotelNumberViewModel);
    }


    public IActionResult Delete(int hotelNumberId)
    {
      HotelNumberViewModel hotelNumberViewModel = new()
      {
        HotelList = _unitOfWork.Hotel.GetAll().Select(h => new SelectListItem
        {
          Text = h.Name,
          Value = h.Id.ToString()
        }),
        HotelNumber = _unitOfWork.HotelNumber.Get(h => h.Hotel_Number == hotelNumberId)
      };

      if (hotelNumberViewModel.HotelNumber == null)
      {
        return RedirectToAction("Error", "Home");
      }

      return View(hotelNumberViewModel);
    }

    //[HttpDelete]
    [HttpPost]
    public IActionResult Delete(HotelNumberViewModel hotelNumberViewModel)
    {

      //var hotelFromDatabase = _db.Hotels.FirstOrDefault(h => h.Id == hotel.Id);
      
      HotelNumber? itemFromDatabase = _unitOfWork.HotelNumber
        .Get(h => h.Hotel_Number == hotelNumberViewModel.HotelNumber.Hotel_Number);
      if (itemFromDatabase != null)
      {
        _unitOfWork.HotelNumber.Remove(itemFromDatabase);
        _unitOfWork.Save();
        TempData["success"] = "Đã xóa hoàn tất";
        return RedirectToAction("Index", "HotelNumber");
      }
      TempData["error"] = "Không xóa được số phòng của khách sạn";
      return View();
    }
  }
}

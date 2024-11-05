using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Diagnostics;

namespace Presentation.Controllers
{
  public class HomeController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
      HomeViewModel homeViewModel = new()
      {
        HotelList = _unitOfWork.Hotel.GetAll(includeProperties: "HotelAmenity"),
        Nights = 1,
        CheckInDate = DateOnly.FromDateTime(DateTime.Now),
      };
      return View(homeViewModel);
    }

    [HttpPost]
    public IActionResult Index(HomeViewModel homeViewModel)
    {
      homeViewModel.HotelList = _unitOfWork.Hotel.GetAll(includeProperties: "HotelAmenity");
      foreach (var hotel in homeViewModel.HotelList)
      {
        // dùng dữ liệu giả, ta giả định khách sạn
        // với id chẵn thì hết phòng
        // với id lẻ thì còn phòng
        if (hotel.Id % 2 == 0)
        {
          hotel.IsAvailable = false;
        }
      }

      return View(homeViewModel);
    }

    //public IActionResult GetHotelByDate(int soDem, DateOnly ngayNhanPhong)
    //{
    //  var hotelList = _unitOfWork.Hotel.GetAll(includeProperties: "HotelAmenity").ToList();
    //  foreach (var hotel in hotelList)
    //  {
    //    // dùng dữ liệu giả, ta giả định khách sạn
    //    // với id chẵn thì hết phòng
    //    // với id lẻ thì còn phòng
    //    if (hotel.Id % 2 == 0)
    //    {
    //      hotel.IsAvailable = false;
    //    }
    //    HomeViewModel homeViewModel = new()
    //    {
    //      CheckInDate = ngayNhanPhong,
    //      HotelList = hotelList,
    //      Nights = soDem
    //    };
    //    return View(homeViewModel);
    //  }
    //  return View(hotelList);
    //}
    public IActionResult GetHotelByDate(int nights, DateOnly ngayNhanPhong)
    {
      // Lấy danh sách khách sạn
      var hotelList = _unitOfWork.Hotel.GetAll(includeProperties: "HotelAmenity").ToList();

      // Thiết lập trạng thái phòng của từng khách sạn
      foreach (var hotel in hotelList)
      {
        // Giả định khách sạn với id chẵn thì hết phòng
        if (hotel.Id % 2 == 0)
        {
          hotel.IsAvailable = false;
        }
      }

      // Khởi tạo ViewModel sau khi đã thiết lập danh sách khách sạn
      HomeViewModel homeViewModel = new()
      {
        CheckInDate = ngayNhanPhong,
        HotelList = hotelList,
        Nights = nights
      };

      // Trả về View với ViewModel hoàn chỉnh
      return View(homeViewModel);

      // Trả về một partial view
      //return PartialView("_HotelList", homeViewModel);
    }


    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      return View();
    }


  }
}

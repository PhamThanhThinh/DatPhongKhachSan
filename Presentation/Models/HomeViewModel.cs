using Domain.Entities;

namespace Presentation.Models
{
  public class HomeViewModel
  {
    // danh sach khach san
    public IEnumerable<Hotel>? HotelList { get; set; }
    // ngay nhan phong
    public DateOnly CheckInDate { get; set; }
    // ngay tra phong
    public DateOnly CheckOutDate { get; set; }

    // so dem nghi lai phong khach san
    public int Nights { get; set; }

  }
}

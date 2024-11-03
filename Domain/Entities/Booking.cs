using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  public class Booking
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }

    [Required]
    public int HotelId { get; set; }
    [ForeignKey("HotelId")]
    public Hotel Hotel { get; set; }

    [Required]
    public string Name {  get; set; }
    [Required]
    public string Email { get; set; }
    public string? Phone { get; set; }

    // tong chi phi
    [Required]
    public double TotalCost { get; set; }
    // so dem
    public int Nights { get; set; }
    // trang thai: người ta có lùi phòng (giống kiểu đặt rồi sau đó lui phòng)
    public string? Status { get; set; }

    // ngay dat phong
    [Required]
    public DateTime BookingDate { get; set; }
    // ngay nhan phong
    [Required]
    public DateOnly CheckInDate { get; set; }
    // ngay tra phong
    [Required]
    public DateOnly CheckOutDate { get; set; }

    // trang thai da thanh toan thanh cong chua
    public bool IsPaymentSuccessful { get; set; } = false;

    // ngay thanh toan
    public DateTime PaymentDate { get; set; }

    // phuong thuc thanh toan: stripe hoặc paypal
    // mã định danh của phiên giao dịch
    public string? StripeSessionId { get; set; }
    // lưu thông tin thanh toán
    public string? StripePaymentIntentId {  get; set; }

    // ngay nhan phong thuc te
    public DateTime ActualCheckInDate { get; set; }
    // ngay tra phong thuc te
    public DateTime ActualCheckOutDate { get; set; }

    public int HotelNumber { get; set; }

  }
}

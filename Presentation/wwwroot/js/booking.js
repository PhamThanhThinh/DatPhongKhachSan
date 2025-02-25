var dataTable;

$(document).ready(function () {
  const urlPrams = new URLSearchParams(window.location.search);
  const status = urlPrams.get('status');
  loadDataTable(status);
});

function loadDataTable(status) {
  dataTable = $('#tableBookings').DataTable({
    //cách cơ bản
    /*let table = new DataTable('#tableBookings');*/
    // dùng ajax để không cần load lại trang
    "ajax": {
      url: 'booking/getall?status='+status
    },
    "columns": [
      { data: 'id', "width": "5%" },
      { data: 'name', "width": "15%" },
      { data: 'email', "width": "15%" },
      { data: 'phone', "width": "10%" },
      { data: 'status', "width": "15%" },
      { data: 'checkInDate', "width": "15%" },
      { data: 'nights', "width": "5%" },
      { data: 'totalCost', "width": "10%" },
      {
        data: 'id', "width": "15%" ,
        "render": function (data) {
          return `<div class="w-75 btn-group">
            <a href="/booking/bookingDetail?bookingId=${data}" 
            class="btn btn-outline-warning mx-2 my-2" >
              <i class="bi bi-pencil-square"> Chi Tiết</i>
            </a>
          </div>`
        }
      }
    ]
  });
}

//ctrl K F


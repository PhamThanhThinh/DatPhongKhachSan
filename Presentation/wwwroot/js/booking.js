var dataTable;

$(document).ready(function () {
  loadDataTable();
});

function loadDataTable() {
  dataTable = $('#tableBookings').DataTable({
    //cách cơ bản
    /*let table = new DataTable('#tableBookings');*/
    // dùng ajax để không cần load lại trang
    "ajax": {
      url: 'booking/getall'
    },
    "columns": [
      { data: 'id', "width": "5%" },
      { data: 'name', "width": "5%" },
      { data: 'email', "width": "5%" },
      { data: 'phone', "width": "5%" },
      { data: 'status', "width": "5%" },
      { data: 'checkInDate', "width": "5%" },
      { data: 'nights', "width": "5%" },
      { data: 'totalCost', "width": "5%" }
    ]
  });
}
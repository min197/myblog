﻿var contact = {
    init: function () {
        contact.registerEvents(); // Tạo một sự kiện
    },
    registerEvents: function () {
        $('.btn-block').off('click').on('click', function (e) {  //Tạo kịch bản gọi sự kiện, chỉ đến nút ấn, on, off 
            e.preventDefault();    // Tạo một sự kiện mặc định
            var btn = $(this)
            var id = $(this).data("id");  // tạo biến id lưu lại giá trị của id

            $.ajax({
                url: "/Admin/Contact/ChangeStatus",  // gọi đến hàm trong controller
                data: { id: id }, // truyền vào tham số
                dataType: "json", // chọn kiểu cho ajax
                type: "POST",
                success: function (response) {
                    alert("Trạng thái của contact đã được thay đổi.");
                    if (response.status == true) {
                        btn.text('Blocked');
                    }
                    else {
                        btn.text('Actived');
                    }
                },
                error: function (ex) {
                    alert("Không thể thay đổi trạng thái.");
                }

            })
        });
    }
}
contact.init();
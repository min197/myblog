var user = {
    init: function () {
        user.registerEvents(); // Tạo một sự kiện
    },
    registerEvents: function () {
        $('.btn-block').off('click').on('click', function (e) {  //Tạo kịch bản gọi sự kiện, chỉ đến nút ấn, on, off 
            e.preventDefault();    // Tạo một sự kiện mặc định
            var btn = $(this)
            var id = $(this).data("id");  // tạo biến id lưu lại giá trị của id

            $.ajax({
                url: "/Admin/Tag/ChangeStatus",  // gọi đến hàm trong controller
                data: { id: id }, // truyền vào tham số
                dataType: "json", // chọn kiểu cho ajax
                type: "POST",
                success: function (response) {
                    alert("Status tag have been changed!");
                    if (response.status == true) {
                        btn.text('Blocked');
                    }
                    else {
                        btn.text('Actived');
                    }
                },
                error: function (ex) {
                    alert("Cannot change status");
                }

            })
        });
    }
}
user.init();
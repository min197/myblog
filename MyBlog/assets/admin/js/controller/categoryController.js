var cat = {
    init: function () {
        cat.registerEvents(); // Tạo một sự kiện
    },
    registerEvents: function () {
        $('.btn-block').off('click').on('click', function (e) {  //Tạo kịch bản gọi sự kiện, chỉ đến nút ấn, on, off 
            e.preventDefault();    // Tạo một sự kiện mặc định
            var btn = $(this)
            var id = $(this).data("id");  // tạo biến id lưu lại giá trị của id

            $.ajax({
                url: "/Admin/Category/ChangeStatus",  // gọi đến hàm trong controller
                data: { id: id }, // truyền vào tham số
                dataType: "json", // chọn kiểu cho ajax
                type: "POST",
                success: function (response) {
                    alert("Trạng thái của danh mục đã thay đổi!");
                    if (response.status == true) {
                        btn.text('Blocked');
                    }
                    else {
                        btn.text('Actived');
                    }
                },
                error: function (ex) {
                    alert("Không thể thay đổi trạng thái");
                }

            })
        });
    }
}
cat.init();

var cat2 = {
    init: function () {
        cat2.registerEvents(); // Tạo một sự kiện
    },
    registerEvents: function () {
        $('.btn-success').off('click').on('click', function (e) {  //Tạo kịch bản gọi sự kiện, chỉ đến nút ấn, on, off 
            e.preventDefault();    // Tạo một sự kiện mặc định
            var btn = $(this)
            var id = $(this).data("id");  // tạo biến id lưu lại giá trị của id

            $.ajax({
                url: "/Admin/Category/ChangeShowOnHome",  // gọi đến hàm trong controller
                data: { id: id }, // truyền vào tham số
                dataType: "json", // chọn kiểu cho ajax
                type: "POST",
                success: function (response) {
                    alert("Trạng thái hiển thị đã thay đổi");
                    if (response.status == true) {
                        btn.text('Hiển thị');
                    }
                    else {
                        btn.text('Ẩn');
                    }
                },
                error: function (ex) {
                    alert("Không thể thay đổi trạng thái");
                }

            })
        });
    }
}
cat2.init();
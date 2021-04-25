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
                    alert("Status category have been changed!");
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
cat.init();

var cat2 = {
    init: function () {
        cat2.registerEvents(); // Tạo một sự kiện
    },
    registerEvents: function () {
        $('.btn-default').off('click').on('click', function (e) {  //Tạo kịch bản gọi sự kiện, chỉ đến nút ấn, on, off 
            e.preventDefault();    // Tạo một sự kiện mặc định
            var btn = $(this)
            var id = $(this).data("id");  // tạo biến id lưu lại giá trị của id

            $.ajax({
                url: "/Admin/Category/ChangeShowOnHome",  // gọi đến hàm trong controller
                data: { id: id }, // truyền vào tham số
                dataType: "json", // chọn kiểu cho ajax
                type: "POST",
                success: function (response) {
                    alert("ShowOnHome have been changed!");
                    if (response.status == true) {
                        btn.text('Blocked');
                    }
                    else {
                        btn.text('Actived');
                    }
                },
                error: function (ex) {
                    alert("Cannot change ShowOnHome");
                }

            })
        });
    }
}
cat2.init();
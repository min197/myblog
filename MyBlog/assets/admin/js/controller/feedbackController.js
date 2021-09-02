var idFb;
var fb = {
    idFeedback: "",
    init: function () {
        fb.registerEvent();
    },

    registerEvent: function () {

        $('#formFeedbackData').validate({
            rules: {
                txtContent: {
                    required: true,
                    minlength: 10
                }
            },
            messages: {
                txtContent: {
                    required: "Bắt buộc nhập nội dung tin nhắn*",
                    minlength: "Nội dung tin nhắn trả lời phải lớn hơn 10 ký tự"
                }
            }
        });

        $('.btn-block').off('click').on('click', function (e) {  //Tạo kịch bản gọi sự kiện, chỉ đến nút ấn, on, off 
            e.preventDefault();    // Tạo một sự kiện mặc định
            var btn = $(this)
            var id = $(this).data("id");  // tạo biến id lưu lại giá trị của id
            $("#modalRepFeedback").modal('show');
            fb.resetForm();
            idFb = id;
           
        });

        //$('#formFeedbackData').keypress(function () {
        //    if ($('#formFeedbackData').valid()) {
        //        $('#btnSendReply').show();
        //    }
        //    else {
        //        $('#btnSendReply').hide();
        //    }
        //});

        $('#btnSendReply').off('click').on('click', function () {
            if ($('#formFeedbackData').valid()) {
                fb.sendMessage(idFb);
            }
        });
    },

    resetForm: function () {
        $("#txtContent").val('');
    },

    sendMessage: function(idFb){
        var id = parseInt(idFb);
        var message = $('#txtContent').val();

        $.ajax({
            url: "/Admin/Feedback/RepFeedback",  // gọi đến hàm trong controller
            data: {
                id: id,
                message : message
            }, // truyền vào tham số
            dataType: "json", // chọn kiểu cho ajax
            type: "POST",
            success: function (response) {
                if (response.status == true) {
                    window.alert("Tin nhắn đã gửi đi thành công!");
                }
                else {
                    window.alert("Tin nhắn gửi đi không thành công!");
                }
            },
            error: function (ex) {
                alert("Không thể gửi tin nhắn");
            }
        });
    }

}
fb.init();
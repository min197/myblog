var feedback = {
    init: function () {
        feedback.registerEvent();
    },

    registerEvent: function () {

        //tạo điều kiện dữ liệu đầu vào của form feedback
        $('#formFeedback').validate({
            rules: {
                txtName: {
                    required: true,
                    minlength: 2
                },
                txtEmail: {
                    required: true,
                    email: true,
                    minlength: 10
                },
                txtMessage: {
                    required: true,
                    minlength: 10,
                    maxlength: 999
                },
                txtPhone: {
                    required: true,
                   // match: "/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/",
                    minlength: 9,
                    maxlength: 13
                }
            },
            messages: {
                // Trả về các tin nhắn nếu chuỗi đầu vào không thỏa mãn điều kiện
                txtName: {
                    required: "Bắt buộc nhập tên*",
                    minlength: "Tên phải lớn hơn 2 ký tự"
                },
                txtEmail: {
                    required: "Bắt buộc nhập email*",
                    email: "Email phải đúng yêu cầu",
                    minlength: "Email tối thiểu 10 ký tự"
                },
                txtMessage: {
                    required: "Bắt buộc nhập nội dung tin nhắn",
                    minlength: "Nội dung bình luận phải lớn hơn 10 ký tự",
                    maxlength: "Nội dung tin nhắn phải nhỏ hơn 1000 ký tự"
                },
                txtPhone: {
                    required: "Bắt buộc nhập số điện thoại*",
                    //match: "Số điện thoại phải hợp lệ",
                    minlength: "Số điện thoại phải lớn hơn 9 số",
                    maxlength: "Số điện thoại phải nhỏ hơn 13 số"
                }
            }
        });

        $('#btnSendFeedback').off('click').on('click', function () {
            if ($('#formFeedback').valid()) {
                feedback.sendFeedback();
            } else {
                window.alert("Bạn chưa nhập đúng thông tin yêu cầu");
            }
        });

        //hàm kiểm tra sự kiện khi gõ
        $('#formFeedback').keypress(function () {
            if ($('#formFeedback').valid()) {
                $('#btnSendFeedback').show();
            } else {
                $('#btnSendFeedback').hide();
            }
        });

        $('#txtPhone').keypress(function () {
            if ($('#formFeedback').valid()) {
                $('#btnSendFeedback').show();
            } else {
                $('#btnSendFeedback').hide();
            }
        });

        $('#txtEmail').keypress(function () {
            if ($('#formFeedback').valid()) {
                $('#btnSendFeedback').show();
            } else {
                $('#btnSendFeedback').hide();
            }
        });
        $('#txtMessage').keypress(function () {
            if ($('#formFeedback').valid()) {
                $('#btnSendFeedback').show();
            } else {
                $('#btnSendFeedback').hide();
            }
        });

    },
    //reset dữ liệu các thành phần theo id truyền vào
    resetForm: function () {
        $('#txtName').val('');
        $('#txtEmail').val('');
        $('#txtPhone').val('');
        $('#txtMessage').val('');
    },

    // hàm gửi feed back, gọi ajax, gửi về server chuỗi string với dữ liệu đã nhập
    sendFeedback: function () {
        var name = $('#txtName').val();
        var email = $('#txtEmail').val();
        var phone = $('#txtPhone').val();
        var message = $('#txtMessage').val();

        $.ajax({
            url: '/Contact/AddFeedback',
            type: 'POST',
            data: {
                name: name,
                email: email,
                phone: phone,
                message: message

            },
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    window.alert("Thư của bạn đã được gửi thành công, cảm ơn bạn đã đóng góp cho sự phát triển của chúng tôi!");
                    $('#btnSendFeedback').hide();
                    feedback.resetForm();
                }
                else {
                    window.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}
feedback.init();
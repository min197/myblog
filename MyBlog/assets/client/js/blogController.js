var cmtID;
var blogController = {

    // Hàm init (hàm sẽ khởi chạy khi biến được gọi)
    init: function () {
        blogController.registerEvent();
    },

    
    // Gán các sự kiện cho form
    registerEvent: function () {
        $('#formCommentData').validate({
            // Kiểm tra chuỗi nhập tại form popup (JQuery validation)
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
                txtContent: {
                    required: true,
                    minlength: 10
                },
                txtWebsite: {
                    url: true,
                    minlength: 10
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
                txtContent: {
                    required: "Bắt buộc nhập nội dung bình luận*",
                    minlength: "Nội dung bình luận phải lớn hơn 10 ký tự"
                },
                txtWebsite: {
                    url: "Địa chỉ trang web phải hợp lệ",
                    minlength: "Địa chỉ Website phải lớn hơn 10 ký tự"
                }
            }

        });

        // Kiểm tra chuỗi nhập form reply comment = tương tự comment
        $('#formReplyCommentData').validate({
            rules: {
                txtRepName: {
                    required: true,
                    minlength: 2
                },
                txtRepEmail: {
                    required: true,
                    email: true,
                    minlength: 10
                },
                txtRepContent: {
                    required: true,
                    minlength: 10
                },
                txtRepWebsite: {
                    url: true,
                    minlength: 10
                }
            },
            messages: {
                txtRepName: {
                    required: "Bắt buộc nhập tên*",
                    minlength: "Tên phải lớn hơn 2 ký tự"
                },
                txtRepEmail: {
                    required: "Bắt buộc nhập email*",
                    email: "Email phải đúng yêu cầu",
                    minlength: "Email tối thiểu 10 ký tự"
                },
                txtRepContent: {
                    required: "Bắt buộc nhập nội dung bình luận*",
                    minlength: "Nội dung bình luận phải lớn hơn 10 ký tự"
                },
                txtRepWebsite: {
                    url: "Địa chỉ trang web phải hợp lệ",
                    minlength: "Địa chỉ Website phải lớn hơn 10 ký tự"
                }
            }

        });

        // Tạo sự kiện khi click vào đối tượng với ID = #ID/ off - tắt trạng thái click trên các đối tượng trước tiên

        $('#btnAddComment').off('click').on('click', function () {
            $('#modalAddComment').modal('show');  // Gọi đến đối tượng với Id, gọi đến modal và hiển thị modal
            blogController.resetForm();   // Gọi đến hàm resetForm();
        });

        $('#btnSendComment').off('click').on('click', function () {
            if ($('#formCommentData').valid()) {  // Kiểm tra dữ liệu nhập vào form
                blogController.saveData();  // Lưu dữ liệu nếu như thỏa mãn
            }
        });

        // Tương tự như trên ^

        $('[id=btnAddReplyComment]').off('click').on('click', function () {
            $('#modalAddReplyComment').modal('show');
            blogController.resetRepForm();

            cmtID = $(this).attr("data-id");  // Lấy giá trị của phần tử hiện tại với data-id
            //console.log("Success got comment: " + cmtID);
        });

        //$('.btnAddReplyComment').off('click').on('click', function () {
        //    $('#modalAddReplyComment').modal('show');
        //    blogController.resetRepForm();

        //});


        $('#btnSendReplyComment').off('click').on('click', function () {
            if ($('#formReplyCommentData').valid()) {
                blogController.sendReply(cmtID);
            }
        });


    },

    // Tạo hàm resetForm trong đối tượng
    resetForm: function () {
        // Reset các trường với Id truyền vào
        $('#txtName').val('');
        $('#txtEmail').val('');
        $('#txtWebsite').val('');
        $('#txtContent').val('');
        $('#ckStatus').prop('checked', true);
    },

    // Tương tự như trên ^
    resetRepForm: function () {
        $('#txtRepName').val('');
        $('#txtRepEmail').val('');
        $('#txtRepWebsite').val('');
        $('#txtRepContent').val('');
        $('#ckRepStatus').prop('checked', true);
    },

    // Lưu thông tin đối tượng và gửi về Controller qua ajax
    saveData: function () {
        // Gán các biến với giá trị là giá trị của các thành phần HTML với Id truyền vào
        var postId = parseInt($('#PostId').val());
        var username = $('#txtName').val();
        var email = $('#txtEmail').val();
        var website = $('#txtWebsite').val();
        var content = $('#txtContent').val();
        var status = $('#ckStatus').prop('checked');

        // Tạo biến kiểu class lưu các giá trị nhận được
        var Comment = {
            PostID: postId,
            UserName: username,
            Email: email,
            Website: website,
            Content: content,
            AcceptContact: status
        }
        // Gọi đến ajax
        $.ajax({
            url: '/Blog/AddComment',  // Gọi đến Controller và Action
            data: {
                strComment: JSON.stringify(Comment) // Dữ liệu truyền vào được chuyển đổi sang văn bản AJAX
            },
            type: 'POST',    // kiểu gọi hàm (POST)
            dataType: 'json',   // kiểu dữ liệu JSON
            // Hàm success - xử lý khi thành công gửi về, nhận và hiển thị thông báo, đóng form
            success: function (response) { 
                if (response.status == true) {
                    bootbox.alert("Bình luận của bạn đã gửi thành công, vui lòng chờ kiểm duyệt, cảm ơn bạn đã đóng góp!", function () {
                        $('#modalAddComment').modal('hide');
                    });
                }
                else {
                    bootbox.alert(response.messages);
                }
            },
            // Hàm error - xử lý khi dữ liệu gửi về thất bại, thông báo lỗi qua console browser
            error: function (err) {
                console.log(err);
            }

        });
    },

    // Hàm gửi câu trả lời bình luận = tương tự hàm gửi bình luận, khác nhau về ParentId truyền vào
    sendReply: function (prID) {
        //console.log("Reply send to comment: " + prID);
        var postId = parseInt($('#RepPostId').val());
        var parentId = parseInt(prID);
        var username = $('#txtRepName').val();
        var email = $('#txtRepEmail').val();
        var website = $('#txtRepWebsite').val();
        var content = $('#txtRepContent').val();
        var status = $('#ckRepStatus').prop('checked');
        var Comment = {
            PostID: postId,
            ParentID: parentId,
            UserName: username,
            Email: email,
            Website: website,
            Content: content,
            AcceptContact: status
        }
        $.ajax({
            url: '/Blog/AddReplyComment',
            data: {
                strComment: JSON.stringify(Comment)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Câu trả lời của bạn đã gửi thành công, vui lòng chờ kiểm duyệt, cảm ơn bạn đã đóng góp!", function () {
                        $('#modalAddReplyComment').modal('hide');
                    });
                }
                else {
                    bootbox.alert(response.messages);
                }
            },
            error: function (err) {
                console.log(err);
            }

        });
    }
}
// Gọi đến hàm khởi tạo
blogController.init(); 
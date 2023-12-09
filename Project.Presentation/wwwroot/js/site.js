
window.onload = function () {
    // Sayfa yüklendiğinde getHeroPosts fonksiyonunu çağır
    getHeroPosts();
    getGridBigPost('Yazılım')
    getGridPostOne("Teknoloji")
    getGridPostOne("Spor")
    getGridPostOne("Otomotiv")
    getGridPostOne("Siyaset")
    getGridPostOne("Dünya")
    getGridPostOne("Donanım")
    $('.reply-btn').click(function () {
        // Tıklanan "Yanıtla" düğmesine sahip olan yanıtın formunu göster/gizle
        $(this).closest('.comment').find('.reply-form').toggle();
    });
};

function getHeroPosts() {
    $.ajax({
        url: "/Post/GetHeroPosts",
        type: "GET",
        success: function (response) {
            $("#heroPosts").html(response);
        }
    })
}

function getGridBigPost(genreName) {
    $.ajax({
        url: "/Post/GetGridBigPost",
        type: "GET",
        data: { 'genreName': genreName },
        success: function (response) {
            $("#gridBigPost").html(response);
        }
    })
}

function getGridPostOne(genreName) {
    $.ajax({
        url: "/Post/GetGridPost",
        type: "GET",
        data: { 'genreName': genreName },
        success: function (response) {
            if ($("#gridPostOne").children().length === 0) {
                $("#gridPostOne").html(response);
            }
            else if ($("#gridPostTwo").children().length === 0) {
                $("#gridPostTwo").html(response);
            }
            else if ($("#gridPostThree").children().length === 0) {
                $("#gridPostThree").html(response);
            }
            else if ($("#gridPostFour").children().length === 0) {
                $("#gridPostFour").html(response);
            }
            else if ($("#gridPostFive").children().length === 0) {
                $("#gridPostFive").html(response);
            }
            else if ($("#gridPostSix").children().length === 0) {
                $("#gridPostSix").html(response);
            }
        }
    })
}

function LikePost(x) {
    x.classList.toggle("fa-thumbs-down");
    x.classList.toggle("red-color");
    x.classList.toggle("fa-thumbs-up");
    x.classList.toggle("green-color");
}

function CreateComment(postId) {
    let commentData = {
        content: $('#content').val(),
        postId: postId
    }
    $.ajax({
        url: "/Comment/Create",
        type: "POST",
        data: commentData,
        success: function (response) {
            $('#comments').append(response);
            $('#content').val("")
        }
    })
}

function CreateReply(commentId) {
    let commentData = {
        content: $('#replyContent').val(),
        commentId: commentId
    }
    $.ajax({
        url: "/Reply/Create",
        type: "POST",
        data: commentData,
        success: function (response) {
            $('#replies').append(response);
            $('#replyContent').val("")
        }
    })
}
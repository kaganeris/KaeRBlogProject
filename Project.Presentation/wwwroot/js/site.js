
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
    FirstSection("Yazılım")
    SecondSection("Dünya")
    ThirdSection("Spor")
    GetTrendPosts()
    GetDetailTrendPosts()
    GetPopulerPosts()
    GetLatestPosts()
    GetLatestFooterPosts()
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

function FirstSection(genreName) {
    $.ajax({
        url: "/Post/FirstSectionPosts",
        type: "GET",
        data: { 'genreName': genreName },
        success: function (response) {
            $("#firstSection").html(response);
        },
        error: function (err) {
            console.log(err)
            console.log(genreName + " FirstSection")
        }
    })
}

function SecondSection(genreName) {
    $.ajax({
        url: "/Post/SecondSectionPosts",
        type: "GET",
        data: { 'genreName': genreName },
        success: function (response) {
            $("#secondSection").html(response);
        },
        error: function (err) {
            console.log(err)
            console.log(genreName + " SecondSection")
        }
    })
}

function ThirdSection(genreName) {
    $.ajax({
        url: "/Post/ThirdSectionPosts",
        type: "GET",
        data: { 'genreName': genreName },
        success: function (response) {
            $("#thirdSection").html(response);
        },
        error: function (err) {
            console.log(err)
            console.log(genreName + " ThirdSection")
        }
    })
}

function GetTrendPosts() {
    $.ajax({
        url: "/Post/GetTrendPosts",
        type: "GET",
        success: function (response) {
            $("#trend").html(response);
        },
        error: function (err) {
            console.log(err)
        }
    })
}

function GetDetailTrendPosts() {
    $.ajax({
        url: "/Post/GetDetailTrendPosts",
        type: "GET",
        success: function (response) {
            $("#pills-trending").html(response);
        },
        error: function (err) {
            console.log(err)
        }
    })
}

function GetLatestPosts() {
    $.ajax({
        url: "/Post/GetLatestPosts",
        type: "GET",
        success: function (response) {
            $("#pills-latest").html(response);
        },
        error: function (err) {
            console.log(err)
        }
    })
}
function GetLatestFooterPosts() {
    $.ajax({
        url: "/Post/GetLatestFooterPosts",
        type: "GET",
        success: function (response) {
            $("#latestPostFooter").html(response);
            console.log("lastes footer post çalıştı",response)
        },
        error: function (err) {
            console.log(err)
        }
    })
}

function GetPopulerPosts() {
    $.ajax({
        url: "/Post/GetPopulerPosts",
        type: "GET",
        success: function (response) {
            $("#pills-popular").html(response);
        },
        error: function (err) {
            console.log(err)
        }
    })
}
// Global olarak tutulan bir değişken üzerinden pageNumber değerini saklayın
let currentPageNumber = 1;

// Next butonuna tıklanınca çağrılacak fonksiyon
function nextPage() {
    currentPageNumber++;
    CategoryPosts(categoryName, currentPageNumber);
}

// Previous butonuna tıklanınca çağrılacak fonksiyon
function previousPage() {
    if (currentPageNumber > 1) {
        currentPageNumber--;
        CategoryPosts(categoryName, currentPageNumber);
    }
}

function CategoryPosts(categoryName, pageNumber) {
    let catPostData = {
        categoryName: categoryName,
        pageNumber: pageNumber
    }
    $.ajax({
        url: "/Post/GetCategoryPosts",
        type: "POST",
        data: catPostData,
        success: function (response) {
            // Tüm sayfa düğmelerinden 'active' sınıfını kaldır
            $('.custom-pagination a').removeClass('active');

            // Seçilen sayfa düğmesine 'active' sınıfını ekle
            $(`#cat${pageNumber}`).addClass("active");
            currentPageNumber = pageNumber;
            $('html, body').animate({ scrollTop: 0 }, 'fast');
            $("#categoryPosts").html("");
            $("#categoryPosts").html(response);
        },
        error: function (err) {
            console.log(err)
        }
    })
}


function LikePost(x, postId) {
    let likeData = {
        postId: postId
    }
    $.ajax({
        url: "/Like/Create",
        type: "POST",
        data: likeData,
        success: function (response) {
            if (response === "Ok") {
                x.classList.remove("fa-thumbs-down", "red-color");
                x.classList.add("fa-thumbs-up", "green-color");
                x.onclick = function () { UnlikePost(x, postId); };
            }
        }
    })
}

function UnlikePost(x, postId) {
    let likeData = {
        postId: postId
    }
    $.ajax({
        url: "/Like/Delete",
        type: "POST",
        data: likeData,
        success: function (response) {
            if (response === "Ok") {
                x.classList.remove("fa-thumbs-up", "green-color");
                x.classList.add("fa-thumbs-down", "red-color");
                x.onclick = function () { LikePost(x, postId); };
            }
        }
    })
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
        content: $(`#replyContent-${commentId}`).val(),
        commentId: commentId
    }
    $.ajax({
        url: "/Reply/Create",
        type: "POST",
        data: commentData,
        success: function (response) {
            $(`#replies-${commentId}`).append(response);
            $(`#replyContent-${commentId}`).val("")
        }
    })
}
document.getElementById("openPopup").addEventListener("click", function () {
    document.getElementById("overlay").style.display = "block";
    document.getElementById("popup").style.display = "block";
});

// Popup kapatma fonksiyonu
document.getElementById("closePopup").addEventListener("click", function () {
    document.getElementById("overlay").style.display = "none";
    document.getElementById("popup").style.display = "none";
});


function UpdatePostPopup(id) {
    $.ajax({
        url: "/Post/Update",
        type: "GET",
        data: { 'id': id },
        success: function (response) {
            $("#popup").html('<span class="close" id="closePopup">&times;</span>' + response);

            $(document).on("click", "#closePopup", function () {
                $("#overlay").hide(); // Örtüyü gizle
                $("#popup").hide(); // Popup'ı gizle
            });
        }
    });
}

function UpdatePost(postId) {
    let postData = {
        content: $('#summernote').val(),
        title: $('#title').val(),
        genreId: $('#genreId').val(),
        imagePath: $('#imagePath').val(),
        id: postId
    }
    $.ajax({
        url: "/Post/Update",
        type: "POST",
        data: postData,
        success: function (response) {
            if (response === "Ok") {
                window.location.reload();
            }
        }
    })
}

function DeletePost(id) {
    $.ajax({
        url: "/Post/Delete",
        type: "GET",
        data: { 'id': id },
        success: function (response) {
            if (response === "Ok") {
                setTimeout(function () {
                    window.location.href = "/"; // Replace with your homepage URL
                }, 2000); // 2 seconds delay

                // Show success notification
                showSuccessNotification();
            }
        }
    });
}




function showSuccessNotification() {
    // Create a popup or notification element
    var notification = document.createElement("div");
    notification.className = "success-notification";
    notification.textContent = "Başarıyla silindi";

    // Add styles to the notification
    notification.style.position = "fixed";
    notification.style.top = "20px";
    notification.style.left = "50%";
    notification.style.transform = "translateX(-50%)";
    notification.style.padding = "10px 20px";
    notification.style.background = "#4CAF50";
    notification.style.color = "#FFF";
    notification.style.borderRadius = "5px";
    notification.style.zIndex = "9999";
    notification.style.width = "100%";
    notification.style.textAlign = "center";

    // Append the notification to the body
    document.body.appendChild(notification);

    // Remove the notification after 2 seconds
    setTimeout(function () {
        document.body.removeChild(notification);
    }, 2000);
}

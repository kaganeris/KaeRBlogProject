window.onload = function () {
    UserTable(1)
    getPostCount()
    getUserCount()
    getAdminInfos()
}


let currentPageNumber = 1;


function nextPage() {
    currentPageNumber++;
    UserTable(currentPageNumber);
}

function previousPage() {
    if (currentPageNumber > 1) {
        currentPageNumber--;
        UserTable(currentPageNumber);
    }
}

function UserTable(pageNumber) {
    let tableData = {
        pageNumber: pageNumber
    }
    $.ajax({
        url: "/Admin/Home/GetAllUsers",
        type: "POST",
        data: tableData,
        success: function (response) {
            // Tüm sayfa düğmelerinden 'active' sınıfını kaldır
            $('.custom-pagination a').removeClass('active');

            // Seçilen sayfa düğmesine 'active' sınıfını ekle
            $(`#cat-${pageNumber}`).addClass("active");
            currentPageNumber = pageNumber;
            $("#index-table").html("");
            $("#index-table").html(response);
            console.log("index-table çalıştı", response)
        },
        error: function (err) {
            console.log("index-table post hata", err)
        }
    })
}

function getPostCount() {
    $.ajax({
        url: "/Admin/Home/GetPostCount",
        type: "GET",
        success: function (response) {
            $("#postCount").html(response);
        }
    })
}

function getUserCount() {
    $.ajax({
        url: "/Admin/Home/GetUsersCount",
        type: "GET",
        success: function (response) {
            $("#userCount").html(response);
        }
    })
}

function getAdminInfos() {
    $.ajax({
        url: "/Admin/Home/GetAdminInfos",
        type: "GET",
        success: function (response) {
            $("#profileImage").attr("src", response.imagePath);
            $("#profileImage2").attr("src", response.imagePath);
            $("#profileName").text(response.fullName);
            $("#profileEmail").text(response.email);
        }
    });
}
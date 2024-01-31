

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
            console.log("cat post çalıştı", response)
            $("#categoryPosts").html("");
            $("#categoryPosts").html(response);
        },
        error: function (err) {
            console.log(err)
        }
    })
}
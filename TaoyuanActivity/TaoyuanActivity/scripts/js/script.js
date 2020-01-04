var bookDataFromLocalStorage = [];

$(document).ready(function () {
    loadBookData();

});

//var group = [
//    { text: "年輕人(0~35歲)", value: "young" },
//    { text: "中年人(36~60歲)", value: "mid" },
//    { text: "老年人(61歲以上)", value: "old" }
//];

//var action = [
//    { text: "影視/廣播類", value: "video" },
//    { text: "舞蹈類", value: "dance" },
//    { text: "戲劇類", value: "drama" }
//];

// create DropDownList from input HTML element
//$("#target_group").kendoDropDownList({
//    dataTextField: "text",
//    dataValueField: "value",
//    dataSource: group,
//    index: 0
//});

//$(".other_act_dropdownlist").kendoDropDownList({
//    dataTextField: "text",
//    dataValueField: "value",
//    dataSource: action,
//    index: 0
//});

// $("#TOP1_recommend_act_grid").kendoGrid({
//     columns: [
//         {field: "Seq"},
//         {field: "ActionName"}
//     ],
//     dataource: [
//         {Seq:"1",ActionName:"AAA"},
//         {Seq:"2",ActionName:"BBB"},
//         {Seq:"3",ActionName:"CCC"},
//     ]
// });

//$("#TOP1_recommend_act_grid").kendoGrid({
//    columns: [
//        {field: "",width: "10%"},
//        {field: "Seq",width: "20%"},
//        {field: "ActionName"}
//    ],
//    pageable: {
//        input: true,
//        numeric: false
//    },
//    dataSource: [
//        {Checkbox:"",Seq:"1",ActionName:"AAA"},
//        {Checkbox:"",Seq:"2",ActionName:"BBB"},
//        {Checkbox:"",Seq:"3",ActionName:"CCC"},
//    ]
//});

//$("#TOP2_recommend_act_grid").kendoGrid({
//    columns: [
//        {field: "",width: "10%"},
//        {field: "Seq",width: "20%"},
//        {field: "ActionName"}
//    ],
//    pageable: {
//        input: true,
//        numeric: false
//    },
//    dataSource: [
//        {Checkbox:"",Seq:"1",ActionName:"DDD"},
//        {Checkbox:"",Seq:"2",ActionName:"EEE"},
//        {Checkbox:"",Seq:"3",ActionName:"FFF"},
//    ]
//});

//$("#TOP3_recommend_act_grid").kendoGrid({
//    columns: [
//        {field: "",width: "10%"},
//        {field: "Seq",width: "20%"},
//        {field: "ActionName"}
//    ],
//    pageable: {
//        input: true,
//        numeric: false
//    },
//    dataSource: [
//        {Checkbox:"",Seq:"1",ActionName:"QQQ"},
//        {Checkbox:"",Seq:"2",ActionName:"WWW"},
//        {Checkbox:"",Seq:"3",ActionName:"EEE"},
//    ]
//});

//$("#other_act_grid").kendoGrid({
//    columns: [
//        {field: "",width: "7%"},
//        {field: "Seq",width: "15%"},
//        {field: "ActionName"}
//    ],
//    height:450,
//    pageable: {
//        input: true,
//        numeric: false
//    },
//    dataSource: [
//        {Checkbox:"",Seq:"1",ActionName:""},
//        {Checkbox:"",Seq:"2",ActionName:""},
//        {Checkbox:"",Seq:"3",ActionName:""},
//    ]
//});


$("#book-bought-datepicker").kendoDatePicker({
    format: "yyyy-MM-dd",
    value: new Date(),
    dateInput: true
});

$("#act_search").kendoGrid({
    dataSource: {
        data: bookDataFromLocalStorage,
        schema: {
            model: {
                fields: {
                    BookId: { type: "int" },
                    BookName: { type: "string" },
                    BookCategory: { type: "string" },
                    BookAuthor: { type: "string" },
                    BookBoughtDate: { type: "string" }
                }
            }
        },
        pageSize: 20,
    },
    toolbar: kendo.template("<div  class='book-grid-toolbar'><input class='book-grid-search' placeholder='我想要找......' type='text'></input></div>"),
    height: 450,
    sortable: true,
    pageable: {
        input: true,
        numeric: false
    },
    columns: [
        { field: "BookId", title: "書籍編號", width: "10%" },
        { field: "BookName", title: "書籍名稱", width: "50%" },
        { field: "BookCategory", title: "書籍種類", width: "10%" },
        { field: "BookAuthor", title: "作者", width: "15%" },
        { field: "BookBoughtDate", title: "購買日期", width: "15%" },
        { command: { text: "刪除", click: deleteBook }, title: " ", width: "120px" }
    ]
});

$("#act_model").kendoGrid({
    dataSource: {
        data: bookDataFromLocalStorage,
        schema: {
            model: {
                fields: {
                    BookId: { type: "int" },
                    BookName: { type: "string" },
                    BookCategory: { type: "string" },
                    BookAuthor: { type: "string" },
                    BookBoughtDate: { type: "string" }
                }
            }
        },
        pageSize: 20,
    },
    // toolbar: kendo.template("<div  class='book-grid-toolbar'><input class='book-grid-search' placeholder='我想要找......' type='text'></input></div>"),
    height: 550,
    sortable: true,
    pageable: {
        input: true,
        numeric: false
    },
    columns: [
        { field: "BookId", title: "書籍編號", width: "10%" },
        { field: "BookName", title: "書籍名稱", width: "50%" },
        { field: "BookCategory", title: "書籍種類", width: "10%" },
        { field: "BookAuthor", title: "作者", width: "15%" },
        { field: "BookBoughtDate", title: "購買日期", width: "15%" },
        { command: { text: "刪除", click: deleteBook }, title: " ", width: "120px" }
    ]
});

//$("#selected_act").kendoGrid({
//    dataSource: {
//        data: bookDataFromLocalStorage,
//        schema: {
//            model: {
//                fields: {
//                    BookId: { type: "int" },
//                    BookName: { type: "string" },
//                    BookCategory: { type: "string" },
//                    BookAuthor: { type: "string" },
//                    BookBoughtDate: { type: "string" }
//                }
//            }
//        },
//        pageSize: 20,
//    },
//    height: 300,
//    sortable: true,
//    pageable: {
//        input: true,
//        numeric: false
//    },
//    columns: [
//        { field: "BookId", title: "書籍編號", width: "10%" },
//        { field: "BookName", title: "書籍名稱", width: "50%" },
//        { field: "BookCategory", title: "書籍種類", width: "10%" },
//        { field: "BookAuthor", title: "作者", width: "15%" },
//        { field: "BookBoughtDate", title: "購買日期", width: "15%" },
//        { command: { text: "刪除", click: deleteBook }, title: " ", width: "120px" }
//    ]
//});


//搜尋                                        
$(".book-grid-search").on("input", function (e) {
    // debugger;
    //var val = $(".book-grid-search").val();  此方法也可
    $("#book_grid").data("kendoGrid").dataSource.filter({
        filters: [
            {
                field: "BookName",
                operator: "contains",
                value: e.target.value
            }
        ]
    });
});

// 新增
$(".btn-add-book").click(function () {
    var update = {
        "BookId": bookDataFromLocalStorage[bookDataFromLocalStorage.length - 1].BookId + 1,  //取id的方式有bug
        "BookCategory": $("#book-category").data("kendoDropDownList").text(),
        "BookName": $("#book-name").val(),
        "BookAuthor": $("#book-author").val(),
        "BookBoughtDate": $("#book-bought-datepicker").val()
    }

    $("#book_grid").data("kendoGrid").dataSource.add(update);
    localStorage.setItem("bookData", JSON.stringify($("#book_grid").data("kendoGrid").dataSource.data()))

    //$("#book_grid").data("kendoGrid").dataSource.add(BookData);
    //bookDataFromLocalStorage.push(BookData);
    //localStorage.setItem("bookData", JSON.stringify(bookDataFromLocalStorage));

    // $.merge(bookDataFromLocalStorage,update)
    //localStorageSetItem("bookData",$.merge(bookDataFromLocalStorage,update));
    // localStorage.setItem("bookData",JSON.stringify($.merge(bookDataFromLocalStorage,update)));
    alert("新增成功");
    //$("#book_grid").data("kendoGrid").dataSource.read();  //instant update 讀取數據項
    //$("#book_grid").data("kendoGrid").refresh();  //使用當前數據項呈現所有表行
    $("#book-category").data("kendoDropDownList").value(data[0].value);
    $("#book-image").attr("src", "image/" + data[0].value + ".jpg")
    $("#book-name").val("");
    $("#book-author").val("");
    var currentDatetime = new Date();
    $("#book-bought-datepicker").data("kendoDatePicker").value(currentDatetime);
})

// 刪除
function deleteBook(e) {
    var tr = this.dataItem($(e.target).closest("tr"));
    // alert(JSON.stringify(tr));
    var r = confirm("確定刪除?");                  // localstorage.setitem出現多次，建議用function代入參數
    if (r == true) {
        $("#book_grid").data("kendoGrid").dataSource.data().remove(tr);
        localStorageSetItem("bookData", $("#book_grid").data("kendoGrid").dataSource.data());
        // localStorage.setItem("bookData",JSON.stringify($("#book_grid").data("kendoGrid").dataSource.data()));
    }
    //loadBookData();            
}

function localStorageSetItem(a, b) {
    localStorage.setItem(a, JSON.stringify(b));
}



function loadBookData() {                   // localstorage去取值 js(bookdata)轉json
    bookDataFromLocalStorage = JSON.parse(localStorage.getItem("bookData"));
    if (bookDataFromLocalStorage == null) {
        bookDataFromLocalStorage = bookData;  //提供一個類似structure給他(空殼)      
        localStorageSetItem("bookData", bookDataFromLocalStorage);
        // localStorage.setItem("bookData",JSON.stringify(bookDataFromLocalStorage));
    }                                         // localstorage要存值 json轉js                         
}

    // localStorage.clear() 刪除測試資料

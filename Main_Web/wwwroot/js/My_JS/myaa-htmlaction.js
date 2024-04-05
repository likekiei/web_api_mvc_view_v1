
// 執行有標記的元素(加載後執行的)(css標記"_LoadMark_ByReady")
function Execute_LoadMark_ByReady() {
    var elms = $("._LoadMark_ByReady");

    $.each(elms, function (index, element) {
        $(element).click();
    });
}

// 執行有標記的元素(加載後執行的)(指定容器)
// 【container：容器】【cssName：要執行的css名稱】
function Execute_LoadMark(container, cssName) {
    var elms = $(container).find("." + cssName);

    $.each(elms, function (index, element) {
        $(element).click();
    });
}

// 顯示HtmlActionLink的View
// 【e：觸發點】【response：結果值】
function ShowView_HtmlActionLink(e, response) {
    if (response !== undefined) { // 有回傳
        if (response._isSuccess !== undefined) { // 回傳Message
            //if (response._isSuccess == true) { // 成功
            //    myaa_ComAlert(response._message, null, null, null);
            //} else { // 失敗
            //    myaa_ComAlert(response._message, null, null, null);
            //}
        } else { // 回傳PartialView
            $(e).after(response);
            $(e).remove();
        }
    } else { // 無回傳
        //myaa_ComFail();
    }
}
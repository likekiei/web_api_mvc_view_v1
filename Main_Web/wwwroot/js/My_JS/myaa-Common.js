//關閉表單Enter觸發Submit
//Enter不觸發表單Submit
function myaa_Enter_DontSubmitForm(formId) {
    $('#' + formId).bind('keypress keydown keyup', function (e) {
        if (e.keyCode == 13) { e.preventDefault(); }
    });
}

//設定特定Cookie名稱的值
//【name：Cookie名稱】【value：Cookie值】
function myaa_Set_CookieValue(name, value) {
    //var id = $(e).attr("id");
    //document.cookie = "SG_WarehouseManageSystem_Web_SideBarItemId=" + id + "; path=/";
    document.cookie = name + "=" + value + "; path=/";

}

//取得特定Cookie名稱的值
//【name：Cookie名稱】
function myaa_Get_CookieValue(name) {
    // Cookie Array
    var cookieAry = document.cookie.split(';');

    // 取得指定Cookie名稱的值
    var value = "";
    $.each(cookieAry, function (index, item) {
        var cookieKV = item.split('=');
        if (cookieKV[0].trim() === name) {
            value = cookieKV[1];
            return false;
        }
    });

    return value;
}

//再次展開左側Bar當前選取的摺疊區塊
//【sideBarId：左側BarId】
function myaa_AgainShow_SideBarCollapseBlock(sideBarId) {
    //sideBar區塊
    var sideBarElm = $("#" + sideBarId);
    //active標籤
    var activeElm = sideBarElm.find(".active");
    //當前標籤最接近的上層collapse區塊
    var collapseElm = activeElm.closest(".collapse");

    //[T：還沒展開][]
    if (collapseElm.hasClass("show") == false) {
        //collapse區塊Id
        var collapseElm_Id = collapseElm.attr("id");
        console.log("collapseElm_Id:" + collapseElm_Id);
        //click[觸發該摺疊區塊的標籤]
        sideBarElm.find("[data-bs-target='#" + collapseElm_Id + "']").click();
    }
}

//變更表單Url。  (直接收Url)
function myaa_Form_ChangeAction_ByUrl(form, url) {
    //var form = $("#" + formId);
    $(form).attr("action", url); //變更Form submit時的連結
}

//變更表單Url，並提交。  (直接收Url)
function myaa_Form_ChangeAction_Submit_ByUrl(formId, url) {
    var form = $("#" + formId);
    form.attr("action", url); //變更Form submit時的連結
    form.submit();
}

//變更表單Url，並提交。  (收controller、action)
function myaa_Form_ChangeAction_Submit_ByAction(formId, controller, action) {
    var form = $("#" + formId);
    var url = "/" + controller + "/" + action;
    form.attr("action", url); //變更Form submit時的連結
    form.submit();
}

//【開發備註】顯示隱藏
//【e：觸發按鈕】【memoElements：備註元件清單】
function myaa_DevelopMemo_Toggle(e, memoElements) {
    switch ($(e).attr("DisplayStatus")) {
        case "hide": //當前為隱藏狀態，改為顯示
            $(memoElements).show();
            $(e).attr("DisplayStatus", "show");
            break;
        case "show": //當前為顯示狀態，改為隱藏
            $(memoElements).hide();
            $(e).attr("DisplayStatus", "hide");
            break;
    }
}

//重新綁定前端驗證
function myaa_RefreshUnobtrusive() {
    $('form').removeData('validator');
    $('form').removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse('form');

    //設定要忽略的驗證(預設會忽略隱藏標籤)(這邊設定為特定的隱藏標籤也要驗證)
    $('form').each(function (index, element) {
        //$(element).validate().settings.ignore = ":hidden:not([isValid='True'])";
        //$(element).validate().settings.ignore = ":hidden:not([isValid='True']), [isValid='False']";
        $(element).validate().settings.ignore = ":hidden:not([isValid='True']), [isValid='False'], .ck";
    });
}

//重新綁定前端驗證
function myaa_RefreshUnobtrusive_ById(formId) {
    $("#" + formId).removeData('validator');
    $("#" + formId).removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse("#" + formId);

    //設定要忽略的驗證(預設會忽略隱藏標籤)(這邊設定為特定的隱藏標籤也要驗證)
    $("#" + formId).each(function (index, element) {
        //$(element).validate().settings.ignore = ":hidden:not([isValid='True'])";
        $(element).validate().settings.ignore = ":hidden:not([isValid='True']), [isValid='False']";
    });
}

//下載檔案
function myaa_DoDownloadFile(data) {
    if (!data.isSuccess) {
        myaa_ComAlert(data.message, null, null, null);
    } else {
        //下載檔案
        window.location = "/Z_Common/DoDownloadFile?fileName=" + data.fileName;
    }
}

//設定返回頂部按鈕
function myaa_ScrollToTopButton_Setting(btnId) {
    var targetBtn = $("#" + btnId);

    //顯示按鈕的條件
    $(window).scroll(function () {
        if ($(this).scrollTop() > 50) {
            targetBtn.fadeIn();
        } else {
            targetBtn.fadeOut();
        }
    });
    //綁定按鈕事件
    targetBtn.click(function () {
        $('body,html').animate({
            scrollTop: 0
        }, 400);
        return false;
    });
}

//檢查字串是否為[undefined || null || 空字串]
function myaa_IsNullOrEmpty(str) {
    return (str == undefined || str == null || str == "" || str === "");
}

//檢查Element是否為[undefined || null || 空字串]
function myaa_IsEmpty_Elm(e) {
    var html = $(e).html();
    return (html == undefined || html == null || $.trim(html) == "");
}

//數字格式化-千分位
Number.prototype.ThousandFormat = function () {
    var parts = this.toString().split('.');
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    return parts.join('.');
};

//讀取中動畫-show
function myaa_LoadingOn() {
    //$("#loading").show();
    $("#loading").css("display", "flex");
}

//讀取中動畫-hide
function myaa_LoadingOff() {
    $("#loading").hide();
}

//【Toastr】Init彈出訊息框
function myaa_ToastrBasic_Init() {
    toastr.options = {
        // 參數設定[註1]
        "closeButton": true, // 顯示關閉按鈕
        "debug": false, // 除錯
        "newestOnTop": true,  // 最新一筆顯示在最上面
        "progressBar": false, // 顯示隱藏時間進度條
        "positionClass": "toast-top-center", // 位置的類別
        "preventDuplicates": false, // 隱藏重覆訊息
        "onclick": null, // 當點選提示訊息時，則執行此函式
        "showDuration": "300", // 顯示時間(單位: 毫秒)
        "hideDuration": "1000", // 隱藏時間(單位: 毫秒)
        "timeOut": "3000", // 當超過此設定時間時，則隱藏提示訊息(單位: 毫秒)
        "extendedTimeOut": "1500", // 當使用者觸碰到提示訊息時，離開後超過此設定時間則隱藏提示訊息(單位: 毫秒)
        "showEasing": "swing", // 顯示動畫時間曲線
        "hideEasing": "linear", // 隱藏動畫時間曲線
        "showMethod": "fadeIn", // 顯示動畫效果
        "hideMethod": "fadeOut" // 隱藏動畫效果
    }
}

//【sweetalert2】導頁後的錯誤訊息
function myaa_RedirectComAlter(message) {
    setTimeout(function () { //延遲0.25秒後執行
        Swal.fire(message);
    }, 250);
}

//【sweetalert2】通用Fail訊息框
function myaa_ComFail() {
    Swal.fire("【異常】處理失敗，請重新嘗試 or 聯繫相關人員");
    //toastr.warning("處理失敗，請重新嘗試 or 聯繫相關人員", "", {
    //    timeOut: 0,
    //    extendedTimeOut: 0
    //});
}

////【sweetalert2】通用Alert訊息框。
//function myaa_ComAlert(_title, _text, _html) {
//    //覺得哪裡怪怪的，以後有空在調整吧
//    _text = myaa_IsNullOrEmpty(_text) ? "" : _text;
//    _html = myaa_IsNullOrEmpty(_html) ? "" : _html;

//    var title = "<div style='display:block'>" +
//        "<div style='word-break: break-word'>" + _title + "</div>" +
//        "<div style='word-break: break-all; text-align: left'>" + _text + "</div>" +
//        "<div style='word-break: break-word'>" + _html + "</div>" +
//        "</div>";

//    var swal = Swal.fire({
//        //title: "<div style='word-break: break-word'>" + _title + "</div>",
//        title: title,
//        //text: _text,
//        //html: _html,
//    });
//}

//【sweetalert2】通用Alert訊息框。
function myaa_ComAlert(_title, _text, _html, _url) {
    // 避免null的值
    _text = myaa_IsNullOrEmpty(_text) ? "" : _text;
    _html = myaa_IsNullOrEmpty(_html) ? "" : _html;

    // Log的超連結元素
    var logUrlElm = myaa_IsNullOrEmpty(_url) ? "" : "<a href='" + _url + "' style='font-size: 20px' target='_blank'>更多訊息(開新視窗)</a>";

    var title = "<div style='display:block'>" +
        "<div style='word-break: break-word'>" + _title + "</div>" +
        "<div style='word-break: break-all; text-align: center'>" + _text + "</div>" +
        "<div style='word-break: break-word'>" + _html + "</div>" +
        "<div style='word-break: break-word'>" + logUrlElm + "</div>" +
        "</div>";

    var swal = Swal.fire({
        //title: "<div style='word-break: break-word'>" + _title + "</div>",
        title: title,
        //text: _text,
        //html: _html,
    });
}

//【sweetalert2】【導頁】通用Alert訊息框
function myaa_ComAlert_Redirect(_title, _text, _html, _url) {
    var swal = Swal.fire({
        title: "<div style='word-break: break-word'>" + _title + "</div>",
        text: _text,
        html: _html,
    }).then(function (result) {
        if (_url !== undefined &&_url != null) { //有給Url才導頁
            window.location.replace(_url);
        }
    });

    return swal;
}

//【sweetalert2】【await】通用送出確認
//formId要submit的表單Id
async function myaa_ComConfirm_Submit(formId) {
    var isConfirm = await myaa_ComConfirm_Swal_await("是否執行此操作?", null, null);

    if (isConfirm) {
        $("#" + formId).submit();
    }
}

//【sweetalert2】【await】通用Alert訊息框
async function myaa_ComAlert_await(_title, _text, _html) {
    var swal = Swal.fire({
        title: "<div style='word-break: break-word'>" + _title + "</div>",
        text: _text,
        html: _html,
    });

    return swal;
}

//【sweetalert2】【下載檔案】通用Confirm訊息框
function myaa_ComConfirm_Download(_title, _text, _html, _url) {
    var swal = Swal.fire({
        title: "<div style='word-break: break-word'>" + _title + "</div>",
        text: _text,
        html: _html,
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonText: "確認",
        cancelButtonText: "返回"
    }).then(function (result) {
        if (result.value) { //點選確認
            if (_url != null) { //有給Url才導頁
                window.location.replace(_url);
            } else {
                myaa_ComFail()
            }
        }
        else { //點選返回
        }
    });
}

//【sweetalert2】【await】通用Confirm訊息框  [return true,false]
async function myaa_ComConfirm_Swal_await(_title, _text, _html) {
    var swal = Swal.fire({
        title: "<div style='word-break: break-word'>" + _title + "</div>",
        text: _text,
        html: _html,
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonText: "確認",
        cancelButtonText: "返回"
    }).then(function (result) {
        if (result.value) { //點選確認
            return true;
        }
        else { //點選返回
            return false;
        }
    });

    return swal;
}


//function myaa_ComConfirm_Swal_Submit 的搭配 Function(e) {
//    var checkElement = $(e).closest("td").find(".isDeleteComfirm"); //用來判斷是否Submit
//    if (checkElement.val().toLocaleLowerCase() === "true") {
//        checkElement.val(false);
//        return true;
//    } else {
//        myaa_ComConfirm_Swal_Submit("【刪除確認】", "此操作將永久刪除該資料", null, e, checkElement);
//        return false;
//    }
//}

//【請搭配上面Function處理】
//【sweetalert2】【是否確認執行】通用Confirm訊息框  [btnElement：要執行的按鈕][checkElement：存放是否確認的<input>]
function myaa_ComConfirm_Swal_Submit(_title, _text, _html, btnElement, checkElement) {
    var swal = Swal.fire({
        title: "<div style='word-break: break-word'>" + _title + "</div>",
        text: _text,
        html: _html,
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonText: "確認",
        cancelButtonText: "返回"
    }).then(function (result) {
        if (result.value) { //點選確認
            //LoadingOn();
            //按鈕再次觸發自己，才能設定為true。
            //$(checkElement).val(true); //是否確認設為true
            //按鈕去觸發隱藏按鈕，才能設定為false。  [默認使用該方法]
            if (checkElement !== undefined && checkElement != null) { //有傳入才處理
                $(checkElement).val(false); //是否確認設為false，設成false，下次在點擊的時候才會再次Confirm。
            }
            $(btnElement).click(); //執行按鈕
            //return true;
        }
        else { //點選返回
            //return false;
        }
    });

    return swal;
}

//【href】禁用開關
//【e：元件】【isToggle：開關(true禁用)】
function myaa_href_Disable_Toggle(e, isToggle) {
    var elm = $(e);
    if (isToggle == true) {
        elm.addClass("disabled");
        elm.addClass("bg-disable-btn");
        //elm.attr("disabled", true);
        //elm.addClass("_mOff");
    } else {
        elm.attr("disabled", false);
        elm.removeClass("bg-disable-btn");
        //elm.attr("disabled", false);
        //elm.removeClass("_mOff");
    }
}

//【button】禁用開關
//【e：元件】【isToggle：開關(true禁用)】
function myaa_button_Disable_Toggle(e, isToggle) {
    var elm = $(e);
    if (isToggle == true) {
        elm.attr("disabled", true);
        elm.addClass("bg-disable-btn");
        elm.addClass("_mOff");
    } else {
        elm.attr("disabled", false);
        elm.removeClass("bg-disable-btn");
        elm.removeClass("_mOff");
    }
}

//【textbox】禁用開關
//【e：元件】【isToggle：開關(true禁用)】
function myaa_textbox_Disable_Toggle(e, isToggle) {
    var elm = $(e);
    if (isToggle == true) {
        elm.attr("readonly", true);
        elm.addClass("bg-disable");
    } else {
        elm.attr("readonly", false);
        elm.removeClass("bg-disable");
    }
}

//【select】禁用開關
//【e：元件】【isToggle：開關(true禁用)】
function myaa_select_Disable_Toggle(e, isToggle) {
    var elm = $(e);
    if (isToggle == true) {
        elm.attr("readonly", true);
        elm.css("pointer-events", "none");
        elm.addClass("bg-disable");
    } else {
        elm.attr("readonly", false);
        elm.css("pointer-events", "");
        elm.removeClass("bg-disable");
    }
}

//【Select2】禁用開關
//【e：元件】【isToggle：開關(true禁用)】
function myaa_select2_Disable_Toggle(e, isToggle) {
    var elm = $(e);
    if (isToggle == true) {
        //鎖定下拉清單 & 變更背景顏色
        elm.parent().find(".select2-container").css("pointer-events", "none");
        elm.parent().find(".select2-container").find(".select2-selection").addClass("bg-disable-select");
    } else {
        //解鎖下拉清單 & 移除背景顏色
        elm.parent().find(".select2-container").css("pointer-events", "");
        elm.parent().find(".select2-container").find(".select2-selection").removeClass("bg-disable-select");
    }
}

////【Select2】設成禁用  //不改成開關了，因為有太多地方用到，以後新專案記得調整
//function myaa_select2_Disable(e) {
//    var elm = $(e);
//    //鎖定下拉清單 & 變更背景顏色
//    elm.parent().find(".select2-container").css("pointer-events", "none");
//    elm.parent().find(".select2-container").find(".select2-selection").addClass("bg-disable-select");
//}

//【Bootstrap】收合變更Icon  //請注意，整個class會換掉
function myaa_CollapseChangeIcon(targetId, targetIconId, showIconCss, hideIconCss) {
    var target = $("#" + targetId); //要收合的目標
    var targetIcon = $("#" + targetIconId); //要變更圖示的Icon目標

    //顯示事件
    target.on("show.bs.collapse", function () {
        targetIcon.attr("class", hideIconCss);
    });

    //隱藏事件
    target.on("hide.bs.collapse", function () {
        targetIcon.attr("class", showIconCss);
    });
}

// 【通用設定值】元件A.val()的值，寫入元件B.val()
// 【eSource：來源】【eTarget：目標】
function myaa_Com_SetInput_ValAToValB(eSource, eTarget) {
    $(eTarget).val($(eSource).val());
}

// 【通用設定值】元件A.val()的值，寫入元件B.text()
// 【eSource：來源】【eTarget：目標】
function myaa_Com_SetInput_ValAToTextB(eSource, eTarget) {
    $(eTarget).text($(eSource).val());
}

// 【通用設定值】元件A.val()的值，寫入元件B.val()，可處理內容替換
// 【eSource：來源】【eTarget：目標】【replaceTXTs：要替換的字串值["被替換值", "替換值"]】
function myaa_Com_SetInput_ValAToValB_ReplaceToEmpty(eSource, eTarget, replaceTXTs) {
    // 新值(取來源的值)
    var newTXT = $(eSource).val();
    // 走訪替換內容陣列，替換新值
    for (var i = 0; i < replaceTXTs.length; i++) {
        newTXT = newTXT.replaceAll(replaceTXTs[i][0], replaceTXTs[i][1]);
    }
    // 填入目標
    $(eTarget).val(newTXT);
}

// 【checkbox】CheckboxA變更CheckboxB的checked狀態
// 【eSource：來源】【eTarget：目標】【isDiff：[T相異變更][F相同變更]】
function myaa_ChangeCheckbox_AToB(eSource, eTarget, isDiff) {
    // [T：相異變更][F：相同變更]
    if (isDiff) {
        $(eTarget).prop("checked", !$(eSource).prop('checked'));
    } else {
        $(eTarget).prop("checked", $(eSource).prop('checked'));
    }
}

// 【checkbox】Checkbox變更input的value(Checked值)
// 【eSource：來源】【eTarget：目標】【isDiff：[T相異變更][F相同變更]】
function myaa_Checkbox_SetInput_CheckedVal(eSource, eTarget, isDiff) {
    // [T：相異變更][F：相同變更]
    if (isDiff) {
        $(eTarget).val(!$(eSource).prop('checked'));
    } else {
        $(eTarget).val($(eSource).prop('checked'));
    }
}

// 【checkbox】區間選取(Shift選取) (radiobutton好像也能用，哪天要是有問題，再改一改拆開吧)
// 【eDiv：來源區塊】
function myaa_Init_Checkbox_Shiftsel(eDiv) {
    //參考套件：shift-select-multiple-checkboxes

    var chkboxShiftLastChecked = []; //各屬性值的最後一次被點擊的checkbox

    //加載事件
    $(eDiv).find('[data-chkbox-shiftsel]').click(function (e) {
        //屬性
        var chkboxType = $(this).data('chkbox-shiftsel');
        if (chkboxType === '') {
            chkboxType = 'default';
        }

        //相同屬性值的checkbox
        var $chkboxes = $('[data-chkbox-shiftsel="' + chkboxType + '"]');

        //放入最後一次被點擊的checkbox，並結束
        if (!chkboxShiftLastChecked[chkboxType]) {
            chkboxShiftLastChecked[chkboxType] = this;
            return;
        }

        //有配合Shift按鍵
        if (e.shiftKey) {
            var start = $chkboxes.index(this); //當前點選的checkbox索引值
            var end = $chkboxes.index(chkboxShiftLastChecked[chkboxType]); //同屬性上一次點選的checkbox索引值

            //依索引開始到結束，包含的checkbox，都押上上一次點選的checkbox的選取狀態
            //$chkboxes.slice(Math.min(start, end), Math.max(start, end) + 1).prop('checked', chkboxShiftLastChecked[chkboxType].checked);

            $chkboxes.slice(Math.min(start, end), Math.max(start, end) + 1).each(function (index, value) {
                var _elm = $(value);
                var chkNowIndex = $chkboxes.index(_elm); //當前Checkbox的索引值
                var checkedVal = chkboxShiftLastChecked[chkboxType].checked; //上一次點選的checkbox的選取狀態(要變更的選取值)

                //不是開頭跟結束的checkbox才處理
                if (chkNowIndex != start && chkNowIndex != end) {
                    //要變更項目的選取值，與要變更的值不相同，才處理
                    if (_elm.prop('checked') != checkedVal) {
                        _elm.click();
                    }
                }
            });
        }

        chkboxShiftLastChecked[chkboxType] = this;
    });
}

// 【embedly】嵌入式Element處理，依embedly套件重新建置嵌入區塊
// 【elmId：區塊Id】
function myaa_EmbeddedBind_Byembedly(elmId) {
    document.getElementById(elmId).querySelectorAll('oembed[url]').forEach(element => {
        // Create the <a href="..." class="embedly-card"></a> element that Embedly uses
        // to discover the media.
        const anchor = document.createElement('a');

        anchor.setAttribute('href', element.getAttribute('url'));
        anchor.className = 'embedly-card';

        element.appendChild(anchor);
    });
}

// 【TextBox】在選取位置插入文字
// 【e：TextBox元素】【txt：要插入的文字】
function myaa_TextBox_InsertAtCaret(e, txt) {
    // 取元素的HTMLInputElement
    var htmlElm = $(e)[0];
    // 取當前選取中的起訖位置
    var strPos = htmlElm.selectionStart;
    var endPos = htmlElm.selectionEnd;
    // 在起訖範圍插入資料
    htmlElm.value = htmlElm.value.substring(0, strPos) + txt + htmlElm.value.substring(endPos, htmlElm.value.length);
    // 重新關注該元素
    $(e).focus();
    // 設定新的起訖位置
    htmlElm.selectionStart = strPos + txt.length;
    htmlElm.selectionEnd = endPos + txt.length;
}
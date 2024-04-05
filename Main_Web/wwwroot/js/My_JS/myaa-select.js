
// 建構Select2(基礎)
// 【e：元件】
function myaa_InitSelect2_Base(e) {
    var element = $(e);

    // 建構
    element.select2({
        placeholder: "請選擇...",
        allowClear: true,
        //dropdownParent: form,
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 0,
    });
}

// 建構Select2(基礎Ajax)
// 【e：元件】【initObj：建構用的值】
function myaa_InitSelect2_BaseAjax(e, initObj) {
    var element = $(e);

    // initObj 欄位
    // var initObj = {
    //     ControllerName: "",
    //     ActionName: "",
    //     IsEnable: true,
    //     PageSize: $PageSize,
    //     IdSelected: 1,
    //     CompanyId: 2,
    // }

    // 建構
    element.select2({
        placeholder: "請選擇...",
        allowClear: true,
        //dropdownParent: form,
        ajax: {
            url: "/" + initObj.ControllerName + "/" + initObj.ActionName,
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    Keyword: params.term,
                    PageNumber: params.page || 1,
                    PageSize: initObj.PageSize,
                    IsEnable: initObj.IsEnable,
                    IdSelected: initObj.IdSelected,
                    CompanyId: initObj.CompanyId,
                    //ignoreNo: deptNo
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;
                return {
                    results: data.data,
                    pagination: {
                        more: (params.page * initObj.PageSize) < data.totalCount
                    }
                };
            },
            cache: true
        },
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 0,
    });
}
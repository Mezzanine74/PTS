$(function () {
    // This is fixing DDLs z-index problem in popup control
    DevExpress.ui.dxOverlay.baseZIndex(50000);
})

//local variables
var _index = '';

var validationPaymentValue = false, validationFinanceNo = false, validationDollar = false, validationEuro = false;
var regExpNumber = /^\d+(\.\d{1,2})?$/;
var regExpNumber4 = /^\d+(\.\d{1,4})?$/;

function validatePaymentValue(rowindex) {

    var val = eval("TextBoxPayment" + rowindex).GetValue();

    var _self = {};
    _self.valid = regExpNumber.test(val)
    _self.message = "";
    _self.empty = true;
    if (val) { _self.empty = false; };

    if (!_self.valid) {
        _self.message = "- Payment value format wrong!" + '<br/>';
    }
    return _self;
};

function validateFinanceNo(rowindex) {

    var val = eval("TextBoxFinance" + rowindex).GetValue();
    var _self = {};
    _self.valid = false;
    _self.message = "";
    _self.empty = true;
    if (val) { _self.empty = false; };

    $.ajax({
        async: false,
        url: "/JSON/ValidateFinanceNo",
        data:{
                no: val,
                year: 2017
                },
        dataType: "json",
        success: function (data) {
            _self.valid = data;

            if (!_self.valid) {
                _self.message = '- ' + val + " already exists!" + '<br/>';
            };

            //return _self;
        }
    });

    return _self;
};

function validateDollar(rowindex) {
    var val = eval("txtRubbleDollar_" + rowindex).GetValue();

    var _self = {};
    _self.valid = regExpNumber4.test(val)
    _self.message = "";
    _self.empty = true;
    if (val) { _self.empty = false; };

    if (!_self.valid) {
        _self.message = "- Dollar rate format wrong!" + '<br/>';
    }
    return _self;

};

function validateEuro(rowindex) {
    var val = eval("txtRubbleEuro_" + rowindex).GetValue();

    var _self = {};
    _self.valid = regExpNumber4.test(val)
    _self.message = "";
    _self.empty = true;
    if (val) { _self.empty = false; };

    if (!_self.valid) {
        _self.message = "- Euro rate format wrong!" + '<br/>';
    }
    return _self;

};

function validateAll(rowindex) {

    var _self = {};

    var vPayment    = validatePaymentValue(rowindex);
    var vFinanceNo  = validateFinanceNo(rowindex);
    var vDollar     = validateDollar(rowindex);
    var vEuro       = validateEuro(rowindex);

    _self.valid = (vPayment.valid && vFinanceNo.valid && vDollar.valid && vEuro.valid);

    _self.empty = (vPayment.empty || vFinanceNo.empty || vDollar.empty || vEuro.empty);

    _self.message = vPayment.message + vFinanceNo.message + vDollar.message + vEuro.message

    return _self;

};

function paymentGotFocus(s, e, rowindex) {
    //if (!(validateAll(rowindex).empty == false && validateAll(rowindex).valid == false)) {
    //    showPopup(rowindex);
    //    console.log('aq');
    //}
};

function financeGotFocus(s, e, rowindex) {
    //if (!(validateAll(rowindex).empty == false && validateAll(rowindex).valid == false)) {
    //    showPopup(rowindex);
    //    console.log('aq');
    //}
};

function dollarGotFocus(s, e, rowindex) {
    //if (!(validateAll(rowindex).empty == false && validateAll(rowindex).valid == false)) {
    //    showPopup(rowindex);
    //    console.log('aq');
    //}
};

function euroGotFocus(s, e, rowindex) {
    //if (!(validateAll(rowindex).empty == false && validateAll(rowindex).valid == false)) {
    //    showPopup(rowindex);
    //    console.log('aq');
    //}
};

// out of document.ready()

function btnMoveClick(s, e, rowindex) {

    loadingPanelmove.Show();
    var value = eval("withVAT" + rowindex).GetValue();
    eval("TextBoxPayment" + rowindex).SetValue(value);
    loadingPanelmove.Hide();
    eval("TextBoxFinance" + rowindex).Focus();

    //showPopup(rowindex);
};

function btnPayRowClick(s, e, rowindex) {
    showPopup(rowindex);
}

function paymentTextChanged(s, e, rowindex) {

    //showPopup(rowindex);
}

function financeTextChanged(s, e, rowindex) {
    
    //showPopup(rowindex);

};

function showPopup(rowindex) {

    loadingPanelmove.ShowInElementByID('btnPayRow' + rowindex);

    setTimeout(function () {
        var validation = validateAll(rowindex);

        popupPayConfirm.SetPopupElementID('btnPayRow' + rowindex);
        popupPayConfirm.Show();
        loadingPanelmove.Hide();

        if (validation.valid) {
            lblMessage.SetClientVisible(false);
            ButtonPay.SetClientVisible(true);
            ButtonPay.Focus();
        } else {
            lblMessage.SetClientVisible(true);
            lblMessage.SetValue(validation.message);
            ButtonPay.SetClientVisible(false);
            // read for enter key
            document.onkeypress = function (e) {
                if (e.which == 13) {
                    popupPayConfirm.Hide(); s.Focus();
                    document.onkeypress = null;
                };
            };
        };

        popupPayConfirm.SetPopupElementID("");
    }, 1);

};

$("#inputProjects").dxSelectBox({
    dataSource: '/PtsPO/JSON/DDLProjectsPOCreate/' + $("#UserName").val(),
    searchEnabled: true,
    searchExpr: ['ProjectName'],
    valueExpr: 'ProjectID',
    displayExpr: 'ProjectName',
    onValueChanged: function (e) {
        GridViewPayLog.PerformCallback();
    },
    onInitialized: function (e) {
        e.component.option("value", 210);
    }
});

projectSelect = $("#inputProjects").dxSelectBox('instance');

function onGridViewPayLogBeginCallback(s, e) {
    console.log(DateSelect.GetText());
    e.customArgs["date"] = DateSelect.GetText();
    e.customArgs["prjid"] = projectSelect.option('value');
}

function onDateSelectChanged(s, e) {
    GridViewPayLog.PerformCallback();
}

function onDateSelectInit(s, e) {
    GridViewPayLog.PerformCallback();
}

//function buttonPayClicked(s, e) {
//    // handles PAY

//    var index = _index;

//    $.getJSON(
//        '/PurchaseOrder/Pay',
//        {
//            financeNo: eval("TextBoxFinance" + index).GetValue(),
//            paymentDate: '2017/01/01', 
//            amount: eval("TextBoxPayment" + index).GetValue(),
//            currency: 'Rub',
//            rubbleDollar: eval("txtRubbleDollar_" + index).GetValue(),
//            rubbleEuro: eval("txtRubbleEuro_" + index).GetValue()
//        }
//        ).success(function (data) {
//            console.log(data);
//            if (data === true) {
//                // payment inserted
//            } else {
//                // payment failed
//            };
//        })
//}


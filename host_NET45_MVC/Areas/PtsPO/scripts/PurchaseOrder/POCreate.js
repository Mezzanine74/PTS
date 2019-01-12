$(function () {
    // This is fixing DDLs z-index problem in popup control
    DevExpress.ui.dxOverlay.baseZIndex(50000);

    $("#inputProjects").dxSelectBox({
        dataSource: '/PtsPO/JSON/DDLProjectsPOCreate/' + $("#UserName").val(),
        searchEnabled: true,
        searchExpr: ['ProjectName'],
        valueExpr: 'ProjectID',
        displayExpr: 'ProjectName',
        onValueChanged: function (e) {

            var _selecteditem = $.grep(e.component.option('items'), function (o) { return o.ProjectID == e.value; })[0];

            if (_selecteditem) {
                if (_selecteditem.RequestedRequired === true) {
                    FormPoCreate.GetItemByName('WhoRequested').SetVisible(true)} else {
                    FormPoCreate.GetItemByName('WhoRequested').SetVisible(false)
                };
            }

            $("#Project_ID").val(e.value);
            $("#ProjectIdName").val(e.component.option('displayValue')); // postback icin gerekli
            whoRequestedSelectBox.option('value', null);

            costCodeSelectBox.option('dataSource', '/PtsPO/JSON/DDLCostCodePOCreate/?prjid=' + $("#Project_ID").val() + '&username=' + $("#UserName").val());
            costCodeSelectBox.option('value', null);
            whoRequestedSelectBox.option('dataSource', '/PtsPO/JSON/DDLRequestedByPOCreate/' + $("#Project_ID").val());
        },
        onInitialized: function (e) {
            // postback icin gerekli
            e.component.option('items', [{ ProjectID: $("#Project_ID").val(), ProjectName: $("#ProjectIdName").val() }]);
            e.component.option('value', $("#Project_ID").val());

            if ($.grep(e.component.option('items'), function (o) { return o.ProjectID == $("#Project_ID").val(); })[0].RequestedRequired === true) {
                FormPoCreate.GetItemByName('WhoRequested').SetVisible(true)
            } else {
                FormPoCreate.GetItemByName('WhoRequested').SetVisible(false)
            };

        }
    });

    $("#inputWhoRequested").dxSelectBox({
        dataSource: '/PtsPO/JSON/DDLRequestedByPOCreate/' + $("#Project_ID").val(),
        searchEnabled: true,
        searchExpr: ['NameSurname'],
        valueExpr: 'username',
        displayExpr: 'NameSurname',
        onValueChanged: function (e) {
            $("#WhoRequested").val(e.value);
            console.log(e.value);
        }
    }).dxValidator({
        validationRules: [
            {
                type: 'required',
                message: 'WhoRequested required'
            }
        ]
    });
    
    whoRequestedSelectBox = $("#inputWhoRequested").dxSelectBox('instance');

    $("#inputCostCode").dxSelectBox({
        dataSource: '/PtsPO/JSON/DDLCostCodePOCreate/?prjid=' + $("#Project_ID").val() + '&username=' + $("#UserName").val(),
        searchEnabled: true,
        searchExpr: ['CostCode_Description'],
        valueExpr: 'CostCode',
        displayExpr: 'CostCode_Description',
        onValueChanged: function (e) {
            $("#CostCode").val(e.value);
            $("#CostCodeDescription").val(e.component.option('displayValue')); // postback icin gerekli
            console.log(e.value);
        },
        onInitialized: function (e) {
            // postback icin gerekli
            e.component.option('items', [{ CostCode: $("#CostCode").val(), CostCode_Description: $("#CostCodeDescription").val() }]);
            e.component.option('value', $("#CostCode").val())
        }
    });

    costCodeSelectBox = $("#inputCostCode").dxSelectBox('instance');

    $("#inputSuppliers").dxAutocomplete({
        dataSource: '/PtsPO/JSON/DDLSupplierPOCreate',
        searchEnabled: true,
        searchExpr: ['SupplierName'],
        valueExpr: 'SupplierName',
        searchTimeout: 250,
        placeholder: 'Type supplier name or INN',
        onItemClick: function (e) {
            $("#SupplierID").val(e.itemData.SupplierID);
            $("#SupplierIDname").val(e.itemData.SupplierName);
            console.log(e.itemData.SupplierID);
            console.log(e);
        },
        onValueChanged: function (e) {
            if (e.value === '' || e.value === null) {
                // for empty string
                $("#SupplierID").val(e.value);
                $("#SupplierIDname").val(e.value);
            }
        },
        onInitialized: function (e) {
            var supplierIDname = $("#SupplierIDname").val();
            console.log(supplierIDname);
            e.component.option('value', supplierIDname);
        }
    });

})

$(function () {
    // This is fixing DDLs z-index problem in popup control
    DevExpress.ui.dxOverlay.baseZIndex(50000);

    $("#inputProjects").dxSelectBox({
        dataSource: '/PtsPO/JSON/DDLProjectsPOCreate/' + $("#UserName").val(),
        searchEnabled: true,
        searchExpr: ['ProjectName'],
        valueExpr: 'ProjectID',
        displayExpr: 'ProjectName',
        //onValueChanged: function (e) {

        //    var _selecteditem = $.grep(e.component.option('items'), function (o) { return o.ProjectID == e.value; })[0];

        //    if (_selecteditem) {
        //        if (_selecteditem.RequestedRequired === true) {
        //            FormPoCreate.GetItemByName('WhoRequested').SetVisible(true)} else {
        //            FormPoCreate.GetItemByName('WhoRequested').SetVisible(false)
        //        };
        //    }

        //    $("#Project_ID").val(e.value);
        //    $("#ProjectIdName").val(e.component.option('displayValue')); // postback icin gerekli
        //    whoRequestedSelectBox.option('value', null);

        //    costCodeSelectBox.option('dataSource', '/PtsPO/JSON/DDLCostCodePOCreate/?prjid=' + $("#Project_ID").val() + '&username=' + $("#UserName").val());
        //    costCodeSelectBox.option('value', null);
        //    whoRequestedSelectBox.option('dataSource', '/PtsPO/JSON/DDLRequestedByPOCreate/' + $("#Project_ID").val());
        //},
        //onInitialized: function (e) {
        //    // postback icin gerekli
        //    e.component.option('items', [{ ProjectID: $("#Project_ID").val(), ProjectName: $("#ProjectIdName").val() }]);
        //    e.component.option('value', $("#Project_ID").val());

        //    if ($.grep(e.component.option('items'), function (o) { return o.ProjectID == $("#Project_ID").val(); })[0].RequestedRequired === true) {
        //        FormPoCreate.GetItemByName('WhoRequested').SetVisible(true)
        //    } else {
        //        FormPoCreate.GetItemByName('WhoRequested').SetVisible(false)
        //    };

        //}
    });

})

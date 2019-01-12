
    $(document).ready(function () {

        $('[data-blockpo]').click(
                function () {
                    $("#ModalRaisePo").modal('show');
                }
        );

        // FOR Contracts

        $("div[id*='panelInItem']").on('dblclick', function () {
            var _this = $(this);
            processMe(_this)
        })

        $('.showMoreLess').on('click', function () {
            var _this = $(this);
            processMe(_this.parent())
        })

        var processMe = function (_p) {

            console.log(_p);

            var _commercialPart = _p.find("div[id*='PanelCommercialTermsContractItem']");
            var _commonPart = _p.find("table[id*='tblCommonNewGN']");
            var _showMoreLess = _p.find('.showMoreLess').find("a");

            if (_commercialPart.hasClass('hide')) {
                _commercialPart.removeClass('hide');
                _showMoreLess.text('Показать меньше < < <');
            } else {
                _commercialPart.addClass('hide');
                _showMoreLess.text('> > > Показать больше');
            }

            if (_commonPart.hasClass('hide')) {
                _commonPart.removeClass('hide');
                _showMoreLess.text('Показать меньше < < <');
            } else {
                _commonPart.addClass('hide');
                _showMoreLess.text('> > > Показать больше');
            }
        }

        //  ............. END FOR Contracts

        // FOR Addendums

        $("div[id*='panelInItemAddendum']").on('dblclick', function () {
            var _this = $(this);
            processMeAddendum(_this)
        })

        $('.showMoreLessAdd').on('click', function () {
            var _this = $(this);
            processMeAddendum(_this.parent())
        })

        var processMeAddendum = function (_p) {

            console.log(_p);

            var _commercialPart = _p.find("div[id*='PanelCommercialTermsAddendumItem']");
            var _commonPart = _p.find("table[id*='tblAddCommonNewGN']");
            var _showMoreLess = _p.find('.showMoreLessAdd').find("a");

            if (_commercialPart.hasClass('hide')) {
                _commercialPart.removeClass('hide');
                _showMoreLess.text('Показать меньше < < <');
            } else {
                _commercialPart.addClass('hide');
                _showMoreLess.text('> > > Показать больше');
            }

            if (_commonPart.hasClass('hide')) {
                _commonPart.removeClass('hide');
                _showMoreLess.text('Показать меньше < < <');
            } else {
                _commonPart.addClass('hide');
                _showMoreLess.text('> > > Показать больше');
            }
        }

        //  ............. END FOR Addendums

    });
       

        $(function () {

            $("input[name*='TxtBoxTagsContract']").tagsInput({
                'height': 'auto',
                'width': 'auto',
                'autocomplete_url': '',
                'autocomplete': {

                    'source': function (request, response) {
                        $.ajax({
                            url: '<%= ResolveUrl("~/Pages/PTS/GenericHandler/TaggingSuppliers.ashx")%>',
                            data: { query: request.term },
                            success: function (data) {
                                var transformed = $.map(data, function (el) {
                                    return {
                                        label: el.SupplierType,
                                        id: el.SupplierTypeId
                                    };
                                });
                                response(transformed);
                            },
                            error: function () {
                                //alert('error');
                            }
                        });
                    },
                    'delay': 0
                },

                'onAddTag': function (tag) {

                    var _id = $(this).parent().next().val();
                    var _item = {};
                    _item.SupplierType = tag;

                    $.ajax({
                        type: "POST",
                        url: '<%= ResolveUrl("~/Pages/PTS/PageMethods/Table_SupplierType.aspx/Insert")%>',
                        data: '{_item: ' + JSON.stringify(_item) + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            //

                            var _item2 = {};
                            _item2.TagSourceIndex = _id;
                            _item2.SourceType = 'c';
                            _item2.tag = tag;

                            $.ajax({
                                type: "POST",
                                url: '<%= ResolveUrl("~/Pages/PTS/PageMethods/Table_SupplierType_Tags.aspx/Insert")%>',
                                data: '{_item2: ' + JSON.stringify(_item2) + '}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    //

                                }
                            });

                        }
                    });

                },

                'onRemoveTag': function (tag) {

                    var _id = $(this).parent().next().val();
                    var _item2 = {};
                    _item2.TagSourceIndex = _id;
                    _item2.SourceType = 'c';
                    _item2.tag = tag;

                    $.ajax({
                        type: "POST",
                        url: '<%= ResolveUrl("~/Pages/PTS/PageMethods/Table_SupplierType_Tags.aspx/Delete")%>',
                        data: '{_item2: ' + JSON.stringify(_item2) + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            //

                        }
                    });

                }

            });

        })


$("input[name*='TxtBoxTagsAddendum']").tagsInput({
    'height': 'auto',
    'width': 'auto',
    'autocomplete_url': '',
    'autocomplete': {

        'source': function (request, response) {
            $.ajax({
                url: '<%= ResolveUrl("~/Pages/PTS/GenericHandler/TaggingSuppliers.ashx")%>',
                data: { query: request.term },
                success: function (data) {
                    var transformed = $.map(data, function (el) {
                        return {
                            label: el.SupplierType,
                            id: el.SupplierTypeId
                        };
                    });
                    response(transformed);
                },
                error: function () {
                    //alert('error');
                }
            });
        },
        'delay': 0
    },

    'onAddTag': function (tag) {

        var _id = $(this).parent().next().val();
        var _item = {};
        _item.SupplierType = tag;

        $.ajax({
            type: "POST",
            url: '<%= ResolveUrl("~/Pages/PTS/PageMethods/Table_SupplierType.aspx/Insert")%>',
            data: '{_item: ' + JSON.stringify(_item) + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                //

                var _item2 = {};
                _item2.TagSourceIndex = _id;
                _item2.SourceType = 'a';
                _item2.tag = tag;

                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("~/Pages/PTS/PageMethods/Table_SupplierType_Tags.aspx/Insert")%>',
                    data: '{_item2: ' + JSON.stringify(_item2) + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        //

                    }
                });

            }
        });

    },

    'onRemoveTag': function (tag) {

        var _id = $(this).parent().next().val();
        var _item2 = {};
        _item2.TagSourceIndex = _id;
        _item2.SourceType = 'a';
        _item2.tag = tag;

        $.ajax({
            type: "POST",
            url: '<%= ResolveUrl("~/Pages/PTS/PageMethods/Table_SupplierType_Tags.aspx/Delete")%>',
            data: '{_item2: ' + JSON.stringify(_item2) + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                //

            }
        });

    }

});

// Search Items goes here
$(function () {

    $("#MainContent_btnSearchContract").on('click', function () {
        $("#divContractSearch").modal('show');

        var tagBoxProjectInstance = $("#inputProjects").dxTagBox("instance");
        var tagBoxSupplierInstance = $("#inputSuppliers").dxTagBox("instance");

        $("input[id*='hiddenSearchAdvanceMode']").val(1);

        selectedPrjId = $("select[id*='DropDownListPrjID']").val();
        selectedProjectName = $("select[id*='DropDownListPrjID'] option:selected").text();

        selectedSupplierId = $("select[id*='DropDownListSupplier']").val();
        selectedSupplierName = $("select[id*='DropDownListSupplier'] option:selected").text();

        console.log(selectedPrjId !== "-1");

        if (selectedPrjId !== "-1") {
            tagBoxProjectInstance.option("value", [{ ProjectID: selectedPrjId, ProjectName: selectedProjectName }])
        }

        console.log(selectedSupplierId !== '');
        if (selectedSupplierId !== '') {
            tagBoxSupplierInstance.option("value", [{ SupplierID: selectedSupplierId, SupplierName: selectedSupplierName }])
        }

    });

    $('#divContractSearch').on('hidden.bs.modal', function () {
        // do something…
        console.log('modal closed');
        $("input[id*='hiddenSearchAdvanceMode']").val(0);
    })

    $("#inputProjects").dxTagBox({
        dataSource: '/PtsContractual/ContractsAddendums/getProjectsSearch',
        searchEnabled: true,
        searchExpr: ['ProjectName'],
        valueExpr: 'ProjectID',
        displayExpr: 'ProjectName',
        onValueChanged: function (e) {
            // e.value is an array

            var _result = "";

            $.each(e.value, function (index, value) {
                if (_result === "") {
                    if (typeof value === "object") { _result = value.ProjectID; } else { _result = value; }
                } else {
                    if (typeof value === "object") { _result = _result + ", " + value.ProjectID; } else { _result = _result + ", " + value; }
                }
            })

            $("input[id*='hiddenSearchInputProjects']").val(_result);
        },
        onInitialized: function (e) {
            $("input[id*='hiddenSearchInputProjects']").val('');
            $("input[id*='hiddenSearchAdvanceMode']").val(0);
        }
    });

    $("#inputSuppliers").dxTagBox({
        dataSource: '/PtsContractual/ContractsAddendums/getSuppliersSearch',
        searchEnabled: true,
        searchExpr: ['SupplierName'],
        valueExpr: 'SupplierID',
        displayExpr: 'SupplierName',
        onValueChanged: function (e) {
            // e.value is an array
            var _result = "";

            $.each(e.value, function (index, value) {
                if (_result === "") {
                    if (typeof value === "object") { _result = "N'" + value.SupplierID + "'"; } else { _result = "N'" + value + "'"; }
                } else {
                    if (typeof value === "object") { _result = _result + "," + "N'" + value.SupplierID + "'"; } else { _result = _result + "," + "N'" + value + "'" }
                }
            })
            
            $("input[id*='hiddenSearchInputSuppliers']").val(_result);
        },
        onInitialized: function (e) {
            $("input[id*='hiddenSearchInputSuppliers']").val('');
            $("input[id*='hiddenSearchAdvanceMode']").val(0);
        }
    });

    $('.add_daterangepicker').on('cancel.daterangepicker', function (ev, picker) {
        picker.element.val("");
    });

    $("#btnReset").on("click",function(){
        $("#MainContent_textBoxSearchInputContractNo").val("");
        $("#MainContent_textBoxSearchInputContractDate").val("");
        $("#MainContent_textBoxSearchInputContractDescription").val("");
        $("#MainContent_cbxSearchInputContractTypeSer").prop("checked", true);
        $("#MainContent_cbxSearchInputContractTypeSub").prop("checked", true);
        $("#MainContent_cbxSearchInputContractTypeSup").prop("checked", true);
        $("#MainContent_ddlBoxSearchInputSignBysupplier").val("");
        $("#MainContent_ddlSearchInputSignByMercury").val("");
        $("#MainContent_cbxSearchInputCurrencyRuble").prop("checked", true);
        $("#MainContent_cbxSearchInputCurrencyDollar").prop("checked", true);
        $("#MainContent_cbxSearchInputCurrencyEuro").prop("checked", true);
        $("#MainContent_ddlBoxSearchInputAdvance").val("");
        $("#MainContent_ddlSearchInputRetention").val("");
        $("#MainContent_ddlBoxSearchInputDraft").val("");
        $("#MainContent_ddlSearchInputFinal").val("");
        $("#MainContent_TextBoxEntryInterval").val("");
        $("#MainContent_TextBoxApprovalInterval").val("");
        $("#inputSuppliers").dxTagBox("reset");
        $("#inputProjects").dxTagBox("reset");
    })

});
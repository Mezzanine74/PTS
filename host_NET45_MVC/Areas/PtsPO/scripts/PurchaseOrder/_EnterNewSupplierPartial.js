
function doOnSuccess() {
    if ($('#Success').val() == '1') {
        FormSupplierEnter.SetVisible(false);
        setTimeout(function () {
            popupAddSupplier.Hide();
            // reset entry controls
            $("h1[id*=h1success]").hide();
            SupplierIDentry.SetValue('');
            SupplierNameentry.SetValue('');
        }, 1000)
        var autocompleteSupplierInstance = $("#inputSuppliers").dxAutocomplete("instance");
        autocompleteSupplierInstance.option("value", $("input[id*=SupplierNamereturn]").val() + " - " + $("input[id*=SupplierIDreturn]").val());
        $("#SupplierID").val($("input[id*=SupplierIDreturn]").val())
    }
}

function PopupControlShown(s, e) {
    FormSupplierEnter.SetVisible(true);
}
$(document).ready(function () {
    // bind your jQuery events here initially
    PTS_BindEvents();
});

function PTS_BindEvents() {
    $(document).ready(function () {
        // bind your jQuery events here initially
        //datepicker plugin
        //link
        $('.add_datepicker').datepicker({
            autoclose: true,
            todayHighlight: true,
            format: 'dd/mm/yyyy'
        })
        //show datepicker when clicking on the icon
        .next().on(ace.click_event, function () {
            $(this).prev().focus();
        });

        $('.add_daterangepicker').daterangepicker({
            format: 'DD/MM/YYYY',
            showDropdowns: true
        });

        $('[data-rel=tooltip]').tooltip();

        var width = $("#navbar").width();
        $("#main-container").width(width);

        // Add timepicker
        $('.add_timepicker').timepicker({
            minuteStep: 1,
            showSeconds: true,
            showMeridian: false,
            disableFocus: true
        });

    });

}




			



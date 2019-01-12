$(function () {

    $('#SpanSiteMap').click(function () {

        $.ajax(
        {
            url: '/PageMethods/SiteMapHTML',
            dataType: "html",
            success: function (data) {
                html = '                <div id="ModalSiteMapTest" class="modal">'
                html += '                    <div class="modal-dialog modal-dialog-center">'
                html += '                        <div class="modal-content modal_inlineBlock">'
                html += '                            <div class="modal-header">'
                html += '                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>'
                html += '                                <h4 class="modal-title">Site Map</h4>'
                html += '                            </div>'
                html += '                            <div class="modal-body" style="width:850px;">'
                html += ''
                html += data
                html += ''
                html += '                            </div>'
                html += '                        </div>'
                html += '                    </div>'
                html += '                </div>'
                $('body').append(html);
                $("#ModalSiteMapTest").modal();
                $("#ModalSiteMapTest").modal('show');
            }
        });

        $('#ModalSiteMapTest').on('hidden.bs.modal', function (e) {
            $(this).remove();
        });

    });

})

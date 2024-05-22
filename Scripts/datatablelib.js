(function ($) {
    "use strict";
    $(document).ready(function () {
        //init dataTables
        $('#gvs').dataTable(
            {
                "lengthMenu": [5, 10, 15, 20, 25],
                lengthChange: true,
                info: true,
                pageLength: 5
            });

        $('#gvsUsers').dataTable(
            {
                "lengthMenu": [5, 10, 15, 20, 25],
                lengthChange: true,
                info: true,
                pageLength: 5
            });
    });
}(jQuery));
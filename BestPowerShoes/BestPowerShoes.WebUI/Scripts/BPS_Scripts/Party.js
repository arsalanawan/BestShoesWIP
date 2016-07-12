$(document).ready(function () {

    $(".chosen-select").chosen({ width: "80%" });
    //  Make and fill the datavase by serach of Customer

    function fillDataTable(members) {
        $("#cusTbl").DataTable({
            "aaData": members,
            //"aoColumnDefs": [{ "bSortable": false, "aTargets": [3] }],
            scrollX: false,
            scrollCollapse: false,
            paging: true,
            "bSort": true,
            "bSortCellsTop": false,
            "bAutoWidth": false,
            "pageLength": 10,
            "bPaginate": true,
            "bLengthChange": true,
            "bFilter": true,
            "bInfo": true, //Hide Data Table Bottom Total Entries Message
            destroy: true,
            "fnRowCallback": function (nRow, aData) {
                $('td:eq(4)', nRow).html('<a href="'+aData.PartyId+'" class="editPlan glyphicon glyphicon-edit" data-toggle="modal" data-target="#editPlanModal"></a><span class="glyphicon glyphicon-remove"></span>');

                //$('td:eq(0)', nRow).addClass('member-' + aData.status);
                //$('td:eq(1)', nRow).html('<a href="Member/' + aData.memberId + '">' + aData.firstName + '</a>');
                //if (aData.status === "Active") {
                //    $('td:eq(5)', nRow).html('<span class="text-success">' + aData.status + '</span>');
                //}
                //if (aData.status === "Inactive") {
                //    $('td:eq(5)', nRow).html('<span class="text-danger">' + aData.status + '</span>');
                //}
            },
            "aoColumns": [
              { data: 'PartyName', defaultContent: 'N/A' },
              { data: 'CategoryObj.CategoryName', defaultContent: 'N/A' },
              { data: 'Mobile', defaultContent: 'N/A' },
              { data: 'Address', defaultContent: 'N/A' },
              { data: 'Action', defaultContent: 'N/A' }
            ]

        });
    }
    $.ajax({
        type: "GET",
        url: window.BasePath.BaseApiUrl + "Party/GetParties",
        success: function (data) {
            fillDataTable(data);
            console.log(data);
        },
        error: function (data) {
            console.log(data);
        }
    });

    /*For Insert Party*/
    $("#partySaveAddMoreId , #partySaveCloseId").click(function () {
        var emt = $(this);        
        var Party = { PartyId: "1", PartyName: $("#name").val(), CategoryObj: { CategoryId: $("#partyProcess").val() }, Mobile: $("#mobile").val(), Address: $("#address").val() };
       $.ajax({
            type: "POST",
            url: window.BasePath.BaseApiUrl + "Party/SaveParty",
            data: JSON.stringify(Party),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success:
                function(data){doNext(data,emt)},
                
            error:
                function () { console.log(data);}

            });

        

    });
    function doNext(data,emt){
        
            if (emt.attr("id") == "partySaveAddMoreId") {
                $('input[type=text]').val('');
                $('textarea').val('');
                $('#queryMsg').html("<h5 class='text-info'>" + data + "</h5>").slideDown(1000);
              
            }
            else {
                $('#queryMsgForPartyIndex').html('<h4 class="lead text-primary">' + data + '</h4>').slideDown(1000);
               
            }
        
    
    }
    
    $("#name").focusin(function () {
        $('#queryMsg').slideUp(1000);
    });
});
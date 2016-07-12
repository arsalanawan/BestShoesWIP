$(document).ready(function () {
    console.log("ready!");
    var dataTbl = "";
    var fromDate = "";
    var toDate = "";
    var articleId = "";
    var processTbl = 0;
    $("#collapseFour").addClass("in");
    //$("#processTbl").DataTable();
    // Make and fill the datatable by search result.
    //function fillDataTable(members) {
    //    dataTbl = $("#planTbl").DataTable({
    //        "aaData": members,
    //        //"aoColumnDefs": [{ "bSortable": false, "aTargets": [3] }],
    //        scrollX: false,
    //        scrollCollapse: false,
    //        paging: true,
    //        "bSort": true,
    //        "bSortCellsTop": false,
    //        "bAutoWidth": false,
    //        "pageLength": 10,
    //        "bPaginate": true,
    //        "bLengthChange": true,
    //        "bFilter": true,
    //        "bInfo": true, //Hide Data Table Bottom Total Entries Message
    //        destroy: true,
    //        "fnRowCallback": function (nRow, aData) {
    //            $('td:eq(9)', nRow).html('<input type="checkbox" class="status" style="width: 15px; height: 23px;" />');
    //            $('td:eq(10)', nRow).html('<a class="editPlan glyphicon glyphicon-edit" data-toggle="modal" data-target="#editPlanModal"></a><span class="glyphicon glyphicon-remove"></span>');
    //            //$('td:eq(0)', nRow).addClass('member-' + aData.status);
    //            //$('td:eq(1)', nRow).html('<a href="Member/' + aData.memberId + '">' + aData.firstName + '</a>');
    //            //if (aData.status === "Active") {
    //            //    $('td:eq(5)', nRow).html('<span class="text-success">' + aData.status + '</span>');
    //            //}
    //            //if (aData.status === "Inactive") {
    //            //    $('td:eq(5)', nRow).html('<span class="text-danger">' + aData.status + '</span>');
    //            //}
    //        },
    //        "aoColumns": [
    //          { data: 'PlanProcessObj.StartDate' },
    //          { data: 'PlanProcessObj.Day', defaultContent: 'N/A' },
    //          { data: 'ArticleObj.ArticleNo', defaultContent: 'N/A' },
    //          { data: 'ArticleObj.ArticleName', defaultContent: 'N/A' },
    //          { data: 'PartyObj.PartyName', defaultContent: 'N/A' },
    //          { data: 'ColorObj.ColorName', defaultContent: 'N/A' },
    //          { data: 'Quantity', defaultContent: 'N/A' },
    //          { data: 'SizeObj.SizeNo', defaultContent: 'N/A' },
    //          { data: 'PlanNo', defaultContent: 'N/A' },
    //          { data: 'StatusesObj.Status', defaultContent: 'N/A' },
    //          { data: 'Actions', defaultContent: 'N/A' }
    //        ]

    //    });
    //    $(".status").change(function () {
    //        if ($(this).is(":checked")) {
    //            var returnVal = confirm("Are you sure?");
    //            $(this).attr("checked", returnVal);
    //        }
    //    });
    //}

    $(".chosen-select").chosen({ width: "66%" });
    // To reset bootstrap modal fields values    
    $('.modal').on('hidden.bs.modal', function () {
        $(this).find("input[type='text'],textarea").val('').end();
    });

    $(".process").click(function (e) {
        debugger;
        var plan = {
            PlanProcessObj: { PlanProcessId: $("#planProcess").val(), StartDate: fromDate, EndDate: toDate },
            StatusesObj: { StatusId: $("#planStatus").val() }
        };
        $.ajax({
            type: "POST",
            url: window.BasePath.BaseApiUrl + "Plan/GetPlans",
            data: plan,
            success: function (data) {
                console.log(data);
                $.each(data.Statuses, function (i, dd) {
                    $("#planStatus").append('<option value="' + dd.StatusId + '">' + dd.StatusDesc + '</option>');
                });
                $.each(data.Processes, function (i, dd) {
                    $("#planProcess").append('<option value="' + dd.ProcessId + '">' + dd.ProcessName + '</option>');
                    if (processTbl == 0) {
                        $("#processTbl").append('<tr><td><a href="Home/Process/1">' + dd.ProcessName + '</a></td></tr>');
                    }
                });
                $(".chosen-select").chosen({ width: "66%" });
                fillDataTable(data.PlanObj);
                processTbl++;
            },
            error: function (data) {
                //console.log(data);
            }
        });
    });

    $("#addPlan").click(function (e) {
        var planDate = "";
        $(function () {
            $.ajax({
                type: "GET",
                url: window.BasePath.BaseApiUrl + "Plan/GetColorsAndPlanNO",
                data: { articleNo: $("#articleNo").val() },
                success: function (data) {
                    $.each(data.Colors, function (i, dd) {
                        $("#colorName").append('<option value="' + dd.ColorId + '">' + dd.ColorName + '</option>');
                    });
                    $("#planNo").val(data.PlanId);
                    $(".colorName").chosen({ width: "100%" });
                },
                error: function (data) {
                    //console.log(data);
                }
            });
        });
        $(function () {
            $("#planDate").datepicker({
                constrainInput: true,
                onSelect: function () {
                    debugger;
                    planDate = $(this).datepicker('getDate');
                    //var day = date.getDate();
                    //var month = date.getMonth() + 1;
                    //var year = date.getFullYear();
                    //var dayOfWeek = date.getUTCDay() + 1;
                    var dayName = $.datepicker.formatDate('DD', planDate);
                    $("#day").val(dayName);
                }
                //showOn: 'button',
                //buttonText: 'To'
            });
        });
        $("#articleNo").focusout(function () {
            $.ajax({
                type: "GET",
                url: window.BasePath.BaseApiUrl + "Plan/GetArticle",
                data: { articleNo: $("#articleNo").val() },
                success: function (data) {
                    articleId = data.ArticleId;
                    $("#articleName").val(data.ArticleName);
                    $("#articleSize").val(data.Size);
                },
                error: function (data) {
                    $.notify("ArticleNo " + $("#articleNo").val() + " not found. Please try again", { color: "#fff", background: "#D44950" });
                }
            });
        });
    });

    $(".savePlan").click(function (e) {
        var $myForm = $('#addPlanForm')
        if ($myForm[0].checkValidity()) {
            var id = $(this).prop('id');
            e.preventDefault();
            var Plan = {
                PlanId: $("#planNo").val(),
                PlanProcessObj: { StartDate: $("#planDate").val() },
                ArticleObj: { ArticleId: articleId, ArticleName: $("#articleName").val(), Size: $("#articleSize").val() },
                ColorObj: { ColorId: $("#colorName").val() },
                Quantity: $("#quantity").val(),
            };
            $.ajax({
                type: "POST",
                url: window.BasePath.BaseApiUrl + "Plan/AddPlan",
                data: Plan,
                success: function (data) {
                    debugger;
                    $.notify("Plan successfully added!", { type: "success" });
                    if (id === "saveAndClose") {
                        $('#addPlanModal').modal('toggle');
                    }
                    else {
                        $("#addPlan").trigger('click');
                    }
                },
                error: function (data) {
                    //console.log(data);
                }
            });
        }
    });

    $('#planTbl tbody').on('click', '.editPlan', function () {
        debugger;
        var data = dataTbl.row($(this).parents('tr')).data();
    });

    $("#updatePlan").click(function () {
    });

    //$(".process").trigger("click");
});
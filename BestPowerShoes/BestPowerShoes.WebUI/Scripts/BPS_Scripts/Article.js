$(document).ready(function () {
    
    // Make and fill the datatable by search result ofArticle.
    function fillDataTable(members) {
        $("#artTbl").DataTable({
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
                $('td:eq(3)', nRow).html('<a href="' + aData.ArticleId + '" id="articleEdit" class="editPlan glyphicon glyphicon-edit" data-toggle="modal" data-target="#editPlanModal"></a><span class="glyphicon glyphicon-remove"></span>');
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
              { data: 'ArticleNo', defaultContent: 'N/A' },
              { data: 'ArticleName', defaultContent: 'N/A' },
              { data: 'Size', defaultContent: 'N/A' },
              { data: 'Actions', defaultContent: 'N/A' }
            ]

        });
    }

    $.ajax({
        type: "GET",
        url: window.BasePath.BaseApiUrl + "Article/GetArticles",
        success: function (data) {
            fillDataTable(data);
            console.log(data);            
        },
        error: function (data) {
            console.log(data);
        }
    });

    /*For Insert Article*/
    $("#articleSaveAddMoreId , #articleSaveCloseId").click(function (event) {
        event.preventDefault();
        
        var emt = $(this);
        var Process = [];
        Process.push( { ProcessId: 1, InRate: $("#cuttingFactoryInRate").val(), OutRate: $("#cuttingFactoryOutRate").val() } );
        Process.push( { ProcessId: 2, InRate: $("#stichingFactoryInRate").val(), OutRate: $("#stichingFactoryOutRate").val() } );
        Process.push( { ProcessId: 3, InRate: $("#printingFactoryInRate").val(), OutRate: $("#printingFactoryOutRate").val() } );
        Process.push( { ProcessId: 4, InRate: $("#soleInjectionFactoryInRate").val(), OutRate: $("#soleInjectionFactoryOutRate").val() } );
        Process.push( { ProcessId: 5, InRate: $("#packingFactoryInRate").val(), OutRate: $("#packingFactoryOutRate").val() } );
       
        var ArticleRate = {
            ArticleObj: { ArticleId: $("#ArticleId").val(), ArticleNo: $("#articalNo").val(), ArticleName: $("#articleName").val(), Size: $("#size").val() },
            ProcessObj:  Process 
        };
        $.ajax({
            type: "POST",
            url: window.BasePath.BaseApiUrl + "Article/SaveArticle",
            data: JSON.stringify(ArticleRate),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success:
                function (data) { doNext(data, emt) },

            error:
                function () { console.log("Error"); }

        });



    });
    function doNext(data, emt) {
        $("#articalNo").focus();
        if (emt.attr("id") == "articleSaveAddMoreId") {
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

    $(document).on("click", "#articleEdit", function () {

        var emt = $(this);
        var Art = emt.attr("href");

        var Article = { ArticleID: Art[0]};
                
      
        $.ajax({
            type: "POST",
            url: window.BasePath.BaseApiUrl + "Article/ArticleProfile",
            data: Article,
            success:
                function (data) {
                    console.log(data);
                    loadEditModel(data);
                    $('#myModal').modal('show');
                   
                },
            error: function (data) {
                console.log(data);
            }

        });


    });
    
    function loadEditModel(data) {
        $("#ArticleId").val(data[0].ArticleObj.ArticleId);
        $("#articalNo").val(data[0].ArticleObj.ArticleNo);
        $("#articleName").val(data[0].ArticleObj.ArticleName);
        $("#size").html('<option value=' + data[0].ArticleObj.Size + ' selected>' + data[0].ArticleObj.Size + '</option>');
        $("#cuttingFactoryInRate").val(data[0].ProcessObj[0].InRate);
        $("#cuttingFactoryOutRate").val(data[0].ProcessObj[0].OutRate);
        $("#stichingFactoryInRate").val(data[0].ProcessObj[1].InRate);
        $("#stichingFactoryOutRate").val(data[0].ProcessObj[1].OutRate);
        $("#printingFactoryInRate").val(data[0].ProcessObj[2].InRate);
        $("#printingFactoryOutRate").val(data[0].ProcessObj[2].OutRate);
        $("#soleInjectionFactoryInRate").val(data[0].ProcessObj[3].InRate);
        $("#soleInjectionFactoryOutRate").val(data[0].ProcessObj[3].OutRate);
        $("#packingFactoryInRate").val(data[0].ProcessObj[4].InRate);
        $("#packingFactoryOutRate").val(data[0].ProcessObj[4].OutRate);
        
       

    }
});
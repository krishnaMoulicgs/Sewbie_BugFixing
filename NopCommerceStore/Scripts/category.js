$(document).ready(
	function () {
	    var categoryid = $('#ctl00_ctl00_cph1_cph1_hidCategoryID').val();
	    var PageNo = $('#ctl00_ctl00_cph1_cph1_hdPageNo').val();
	    var pageNumber = 1;
	    var strNoOfItemsPerPage = '25';
	    $(".top-cont").hide();
	    $(".bottom-cont").hide();

	    $('#hidPageNumber').val(pageNumber);

	    $('#divLoading').show();
	    $('#overlay').show();

	    var requestUrl = window.location.href.split('&');
	    if (requestUrl[1] != null) {


	        var requestUrl = window.location.href.split('?');
	        var tnewurl = '';
	        var tequalsplit;
	        var strPageNo = pageNumber;

	        if (requestUrl.length > 1) {
	            var splitamp = requestUrl[1].split('&');

	            for (var i = 0; i < splitamp.length; i++) {
	                tequalsplit = splitamp[i].split('=');
	                
                    if (tequalsplit[0] == 'PageNo') {
	                    strPageNo = tequalsplit[1];
	                }
	                if (tequalsplit[0] == 'NoOfItemsPerPage') {
	                    strNoOfItemsPerPage = tequalsplit[1];
	                }

	            }
	        }


	        $.ajax({
	            type: "POST",
	            contentType: "application/json; charset=utf-8",
	            dataType: "json",
	            url: "../Services/SearchService.svc/SearchByCategory?PageNo='" + strPageNo + "'&NoOfItemsPerPage='" + strNoOfItemsPerPage + "'",
	            method: "POST",
	            data: '{"category":"' + categoryid + '", "page":"' + (parseInt(strPageNo, 10) - 1) + '"}',
	            success: search_success,
	            error: search_error
	        });

	    }
	    else {

	        $.ajax({
	            type: "POST",
	            contentType: "application/json; charset=utf-8",
	            dataType: "json",
	            url: "../Services/SearchService.svc/SearchByCategory",
	            method: "POST",
	            data: '{"category":"' + categoryid + '", "page":"' + (parseInt(pageNumber, 10) - 1) + '"}',
	            success: search_success,
	            error: search_error
	        });

	    }
	}
);

	function ShowPage(pageno,tmode) {

	    var tpagecount;    


	    tpagecount = $('#hidPageCount').val();

	    if (parseInt(pageno) <= 0) {

	        alert("Please enter a correct page number");
	        return;
	    }
	    
        if (pageno == "") {
	        alert("Please enter a page number");    
	        return;
	    }

	    if (parseInt(pageno) > tpagecount) {
	        alert("Please enter a valid page number");
	        return;
	    }


	    $('#divLoading').show();
	    $('#overlay').show();
	   
	    $(".bottom-cont").hide();

	    if (tmode == 1) {
	        $('#TxtPageNo').val('');
	    }

	    $('#hidPageNumber').val(pageno);

	    var pagesize = $("#lnkSelectPageSize").val();

	    var categoryid = $('#ctl00_ctl00_cph1_cph1_hidCategoryID').val();
	    var pageNumber = pageno;
	    var maxRecords = $('#hidMaxRecords').val();

	    //check to make sure there are more records to fetch.
	    if (((parseInt(pageNumber, 10) - 1) * pagesize) < maxRecords) {
	        //check to make sure all requests are back.

	        if ($('#hidRequestOut').val() == 0) {

	            $('#hidRequestOut').val('1');

	            //send a query to server side to present new content

	            $.ajax({
	                type: "POST",
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                url: "../Services/SearchService.svc/SearchByCategoryPaging",
	                method: "POST",
	                data: '{"category":"' + categoryid + '", "page":"' + (parseInt(pageNumber, 10) - 1) + '", "pagesize":"' + pagesize + '"}',
	                success: paging_success,
	                error: search_error
	            });


	        }
	    } 
        
        else {
            $('#divLoading').hide();
            $('#overlay').hide();
            $('#divSearchResults').show();
            $(".bottom-cont").show();
	        $('#divResultEnd').hide();
	    }
	}


	function ReArrangePages(pagesize) {	

	    var categoryid = $('#ctl00_ctl00_cph1_cph1_hidCategoryID').val();
	    var pageNumber = 1;
	    var maxRecords = $('#hidMaxRecords').val();
  
        $('#TxtPageNo').val('');
        $('#TxtPageNoBottom').val('');

        $("#SelectPageSizeBottom").val(pagesize);
        $("#lnkSelectPageSize").val(pagesize);

        $('#divLoading').show();
        $('#overlay').show();
       
        $(".bottom-cont").hide();

	    //check to make sure there are more records to fetch.
        if (((parseInt(pageNumber, 10) - 1) * pagesize) < maxRecords) {
	        //check to make sure all requests are back.
	        if ($('#hidRequestOut').val() == 0) {

	            $('#hidRequestOut').val('1');

	            //send a query to server side to present new content
	            $.ajax({
	                type: "POST",
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                url: "../Services/SearchService.svc/SearchByCategoryPaging",
	                method: "POST",
	                data: '{"category":"' + categoryid + '", "page":"' + (parseInt(pageNumber, 10) - 1) + '", "pagesize":"' + pagesize + '"}',
	                success: paging_successnew,
	                error: search_error
	            });

	            

	        }
	    } else {
	        $('#divLoading').hide();
	        $('#overlay').hide();
	        $('#divSearchResults').show();
	        $(".bottom-cont").show();
	        $('#divResultEnd').hide();
	    }
	    

	}


	function ShowPageNumbers(showmode) {

	    var pageStr = "";
	    var pageIndex = $('#hidPageNumber').val();
	    var pageCount = $('#hidPageCount').val();
	    
	    var NoOfItemsPerPage = $("#lnkSelectPageSize").val();
	  
	    var url;
	    url = GenerateUrl(pageIndex, NoOfItemsPerPage);
	    
        pageStr += "<a href='#' class='category_pager_a' onclick='ShowPage(" + "1,1" + ")'>" + "1" + "</a>";
	    if (pageIndex == 1) {
            
            if(showmode == 1)window.location.href = url;
	    }
	   
	    var pageStart = pageIndex - 3 > 1 ? pageIndex - 3 : 1;	 
        if (pageCount > 1 || pageIndex > 1) {            
            if (pageIndex - 1 >= 1) {
                pageStr += "<a href ='#' class='category_pager_a' onclick='ShowPage(" + (pageIndex - 1) + ",1)'>" + "«" + "</a>";
                
            }
            else {
                if (pageIndex != 1)
                    pageStr += "<a href ='#' class='category_pager_a' onclick='ShowPage(1,1)'>" + "«" + "</a>";
            }            
            
        }

        var tval = parseInt(pageIndex) + parseInt(1);
        
        for (var i = 0; i < 9; i++) {
           
            if (pageIndex == pageStart) {
                if (pageStart != 1 && pageStart!=pageCount) {
                    pageStr += "<a href ='#' class='category_pager_a_select' onclick='ShowPage(" + pageStart + ",1)'>" + pageStart + "</a>";
                    if (showmode == 1) window.location.href = url;
                }
            }
            else {
                if (pageStart != 1 && pageStart != pageCount) {
                    pageStr += "<a href ='#' class='category_pager_a' onclick='ShowPage(" + pageStart + ",1)'>" + pageStart + "</a>";
                        
                }
            }
           
            pageStart++;
            if (pageStart > pageCount) {
                break;
            }
        }
       
         if (pageIndex + 1 < pageCount) {
             
             pageStr += "<a  href ='#' class='category_pager_a'  onclick='ShowPage(" + tval + ",1)'>" + "»" + "</a>";
          }
          else
          {
              if (pageIndex != pageCount) {
                  pageStr += "<a  href ='#' class='category_pager_a'  onclick='ShowPage(" + tval + ",1)'>" + "»" + "</a>";
              }
          }

          pageStr += "<a href ='#' class='category_pager_a' onclick='ShowPage(" + (pageCount) + ",1)'>" + pageCount + "" + "</a>";
          
          if (pageIndex == pageCount) {
              if (showmode == 1) window.location.href = url;
          }
         $("#pageContainerTopPage").html(pageStr);
         $("#pageContainerBottomPage").html(pageStr);

        
	}


function search_success(result) {
    //display the results
    
	if (result.d.products.length > 0) {
    
        $("#divSearchResults").append($("#itemTemplate").render(result.d.products));
	    
		$('#divSearchResults').imagesLoaded(function () {

		    $('#divLoading').hide();
		    
            $('#overlay').hide();

			$(".top-cont").show();
            $(".bottom-cont").show();

			$('#divSearchResults').isotope({
					itemSelector: '.item-box',
					columnWidth: 243,
                    resizesContainer: true
				});
			$('.item-box').show();
		});
			
        $('#hidMaxRecords').val(result.d.maxRecords);
        $('#lnkSelectPageSize').val(25);
        $('#lnkSelectPageSizeBottom').val(25)
        
        var tpagecount = $('#hidMaxRecords').val() / $('#lnkSelectPageSize').val();
        var tval = $('#hidMaxRecords').val() % $('#lnkSelectPageSize').val();
        
        tpagecount = parseInt(tpagecount,10);

        if (tval != 0) tpagecount = tpagecount + 1;
        $("#hidPageCount").val(tpagecount);

        ShowPageNumbers(0);
        
        $("#lnkSelectPageSize").show();
        $("#lnkSelectPageSizeBottom").show();      

	} else {
        //draw blank.
        $('#hidRequestOut').val('0');
        $('#divLoading').hide();
        $('#overlay').hide();
        $('#divResultEnd').show();
        
        
	}
}

function paging_success(result) {
	//display the results
	if (result.d.products.length > 0) {

	    var itl;
	    var $removeItem;
	    
        $('#divResultEnd').hide();

        $('.item-frame').remove();
        $('.item-box').remove();

        var newelems = $($("#itemTemplate").render(result.d.products));
        $("#divSearchResults").append($("#itemTemplate").render(result.d.products));

        $('#divSearchResults').imagesLoaded(function () {
            $('#divLoading').hide();
            $('#overlay').hide();
            $('#divSearchResults').show();
            $(".bottom-cont").show();

            $('#divSearchResults').isotope('destroy');

            $('#divSearchResults').isotope({
                itemSelector: '.item-box',
                columnWidth: 243,
                resizesContainer: true
            });

            $('.item-box').show();

            $('#divSearchResults').isotope('appended', newelems, function () { });
            
        });
		
		
		$('#hidRequestOut').val('0');
		$('#hidMaxRecords').val(result.d.maxRecords);
		
        ShowPageNumbers(1);


    } else {
        $('#hidRequestOut').val('0');
        $('#divLoading').hide();
        $('#overlay').hide();
        $('#divResultEnd').show();    
        //draw blank.
	}
}


function paging_successnew(result) {
    //display the results
    if (result.d.products.length > 0) {

        var itl;
        var $removeItem;

        
        $('#divResultEnd').hide();

        $('.item-frame').remove();
        $('.item-box').remove();

        var newelems = $($("#itemTemplate").render(result.d.products));
        $("#divSearchResults").append($("#itemTemplate").render(result.d.products));

        $('#divSearchResults').imagesLoaded(function () {
            $('#divLoading').hide();
            $('#overlay').hide();
            $('#divSearchResults').show();
            $(".bottom-cont").show();

            $('#divSearchResults').isotope('destroy');
            $('#divSearchResults').isotope({
                itemSelector: '.item-box',
                columnWidth: 243,
                resizesContainer: true
            });

            $('.item-box').show();

            $('#divSearchResults').isotope('appended', newelems, function () { });
            
        });


        $('#hidRequestOut').val('0');
        $('#hidMaxRecords').val(result.d.maxRecords);

        var tpagecount = $('#hidMaxRecords').val() / $('#lnkSelectPageSize').val();
        var tval = $('#hidMaxRecords').val() % $('#lnkSelectPageSize').val();
        

        tpagecount = parseInt(tpagecount, 10);
        
        if (tval != 0) tpagecount = tpagecount + 1;
        $('#hidPageCount').val(tpagecount);

        $('#hidPageNumber').val("1");
        
        ShowPageNumbers(1);


    } else {
        $('#hidRequestOut').val('0');
        $('#divLoading').hide();
        $('#overlay').hide();
        $('#divResultEnd').show();
        //draw blank.
    }
}


function GenerateUrl(pageno,noofitems) {

    var requestUrl = window.location.href.split('?');
    var tnewurl = '';
    var tequalsplit;

    if (requestUrl.length > 1) {
        var splitamp = requestUrl[1].split('&');
        
        for (var i = 0; i < splitamp.length; i++) {
            tequalsplit = splitamp[i].split('=');
            if (tequalsplit[0] != 'PageNo' && tequalsplit[0] != 'NoOfItemsPerPage') {
                tnewurl = tnewurl + splitamp[i] + '&';
            }  
        }
    }

    tnewurl = requestUrl[0] + "?" + tnewurl + "PageNo=" + pageno + "&" + "NoOfItemsPerPage=" + noofitems + "";
    return tnewurl; 

}

function search_error(result) {
	//display an error or blank page.
    //reset counts.
    
    $('#hidRequestOut').val('0');
}

function infiniteScroll() {
	//show loading image.

	//get categoryid
	var categoryid = $('#ctl00_ctl00_cph1_cph1_hidCategoryID').val();
	var pageNumber = $('#hidPageNumber').val();
	var maxRecords = $('#hidMaxRecords').val();


	$('#divLoading').show();
	$('#overlay').show();
	$('#divSearchResults').hide();
	$(".bottom-cont").hide();

	//check to make sure there are more records to fetch.
	if ((pageNumber * 20) < maxRecords) {
		//check to make sure all requests are back.
		if ($('#hidRequestOut').val() == 0) {

			$('#hidRequestOut').val('1');

			//send a query to server side to present new content
			$.ajax({
				type: "POST",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				url: "../Services/SearchService.svc/SearchByCategory",
				method: "POST",
				data: '{"category":"' + categoryid + '", "page":"' + pageNumber + '"}',
				success: paging_success,
				error: search_error
			});
		}
	} else {
    $('#divLoading').hide();
    $('#overlay').hide();
    $('#divSearchResults').show();
    $(".bottom-cont").show();
    $('#divResultEnd').show();
	}

	//update page number control.
}



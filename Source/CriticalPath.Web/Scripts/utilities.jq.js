function numbersOnly(evt, arrayExceptions) {
    if (arrayExceptions == null) {
        arrayExceptions = [8, 9, 13, 16, 17, 18, 20, 27, 35, 36, 37, 38, 39, 40, 45, 46, 144];
    }
    if ((evt.keyCode < 48 || evt.keyCode > 57) &&
            (evt.keyCode < 96 || evt.keyCode > 106) && // NUMPAD
            $.inArray(evt.keyCode, arrayExceptions) === -1) {
        return false;
    }
}
function decNumbersOnly(evt) {
    var arrayExceptions = [8, 9, 13, 16, 17, 18, 20, 27, 35, 36, 37, 38, 39, 40, 45, 46, 144, 188, 190];
    return numbersOnly(evt, arrayExceptions);
}
function getObjectById(array, id) {
    var found;
    $.each(array, function (index, obj) {
        if (obj.Id == id) {
            found = obj;
        }
    });
    return found;
}
function setPagerButtons(pagerParent, page, pageCount, buttonsCount) {
    pagerParent.empty();
    if (pageCount > 1) {
        if (buttonsCount == null || !(buttonsCount > 0)) {
            buttonsCount = 10;
        }
        pagerParent.append('<li class="active"><span id="recordsStats" style="margin:0 0 0 2px;min-width:92px;text-align:center;"></span></li>');
        pagerParent.append(createPagerButton(0, page, pageCount));
        pagerParent.append(createPagerButton(-1, page, pageCount));
        var pagers = pageCount > buttonsCount ? buttonsCount : pageCount;
        var pagerStart = page > (buttonsCount / 2) ? page - (pagers / 2) : 1;
        if ((pagerStart + pagers) > pageCount) {
            pagerStart = pageCount - pagers + 1;
        }
        for (var i = pagerStart; i < (pagerStart + pagers) ; i++) {
            pagerParent.append(createPagerButton(i, page, pageCount));
        }
        pagerParent.append(createPagerButton(-2, page, pageCount));
        pagerParent.append(createPagerButton(-3, page, pageCount));
    }
}
/* Creates Bootstrap style pager button esp. for setPagerButtons function */
function createPagerButton(btnNr, page, pageCount) {
    var li = $('<li/>');
    if (page == btnNr) {
        li.addClass("active");
    }
    var label = btnNr == 0 ? '&laquo;' : btnNr == -1 ? '&lsaquo;' : btnNr == -2 ? '&rsaquo;' : btnNr == -3 ? '&raquo;' : btnNr;
    var ariaLabel = btnNr < 0 ? 'LastPage' : btnNr == 0 ? 'FirstPage' : btnNr;
    if (btnNr <= 0) {
        var nextPage = page < pageCount ? page + 1 : pageCount;
        var prevPage = page > 1 ? page - 1 : 1;
        btnNr = btnNr == -3 ? pageCount : btnNr == -2 ? nextPage : btnNr == -1 ? prevPage : 1;
        if (page == btnNr) {
            li.addClass("disabled");
        }
    }
    li.append('<a href="#" style="min-width:44px;text-align:center;" aria-label="' +
        ariaLabel + '" onclick="setPager(' +
        btnNr + ')"><span aria-hidden="true">' + label + '</span></a>');
    return li;
}

function displayRecordStats(visibleRecords, totalRecords) {
    $('#recordsStats').html(visibleRecords + ' / ' + totalRecords);
}
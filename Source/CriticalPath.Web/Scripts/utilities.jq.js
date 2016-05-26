﻿function numbersOnly(evt, arrayExceptions) {
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
    var arrayExceptions = [8, 9, 13, 16, 17, 18, 20, 27, 35, 36, 37, 38, 39, 40, 45, 46, 144];
    var decimalSeperator = getDecimalSeperator();//Mod 2015.12
    if (!(evt.target.value.indexOf(decimalSeperator) > -1)) {
        if (decimalSeperator == '.') {
            arrayExceptions.push(190);
        }
        else if (decimalSeperator == ',') {
            arrayExceptions.push(188);
        }
    }
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
/* Bootstrap pager functions */
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
    if (btnNr > 0 && page != btnNr && (page < btnNr - 2 || page > btnNr + 2)) {
        li.addClass("visible-lg-inline");
        li.addClass("visible-md-inline");
    }
    if (btnNr > 0 && page != btnNr && (page == btnNr - 1 || page == btnNr + 1 || page == btnNr - 7 || page == btnNr + 7 || page == btnNr - 9 || page == btnNr + 9)) {
        li.addClass("visible-lg-inline");
        li.addClass("visible-md-inline");
        li.addClass("visible-sm-inline");
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
    var lnk = page == btnNr ? '' : ' href="javascript:;" onclick="getPagedData(' + btnNr + ')"';
    li.append('<a style="min-width:44px;text-align:center;" aria-label="' + ariaLabel + '"' +
        lnk + '><span aria-hidden="true">' + label + '</span></a>');
    return li;
}
function displayRecordStats(visibleRecords, totalRecords) {
    $('#recordsStats').html(visibleRecords + ' / ' + totalRecords);
}
/* End - Bootstrap pager functions */

/* * * * * * * * * * * * * * * * * * * * * * * * *
 * Changes selection of a selectlist by selectId 
 * usage sample => setSelectListID('#SellingCurrencyId > option', ui.item.SellingCurrencyId);
 */
function setSelectListID(selectListOpts, selectedId) {
    $(selectListOpts).each(function () {
        if (this.value == selectedId) {
            $(this).prop('selected', true);
        }
        else {
            $(this).prop('selected', false);
        }
    });
}

function jsonDateToLocaleDateString(jsonDate) {
    if (jsonDate != null && jsonDate != '') {
        var date = new Date(jsonDate);
        return date.toLocaleDateString();
    }
    else {
        return '';
    }
}

function jsonDateToString(jsonDate) {
    var dateString = '';
    if (jsonDate != null && jsonDate != '' && jsonDate != "undefined") {
        var date = new Date(jsonDate);
        dateString = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
    }
    return dateString;
}

function jsonDateToDate(jsonDate) {
    if (jsonDate != null && jsonDate != '' && jsonDate != "undefined") {
        return new Date(jsonDate);
    }
    else {
        return new Date();
    }
}

function areDaysSame(date1, date2) {
    if (date1 == null || date1 == '' || date1 == 'undefined' ||
        date2 == null || date2 == '' || date2 == 'undefined') {
        return false;
    }

    var parsed1 = new Date(date1);
    var parsed2 = new Date(date2);
    return parsed1.getDate() == parsed2.getDate() && parsed1.getMonth() == parsed2.getMonth() && parsed1.getFullYear() == parsed2.getFullYear();
}

function fixDecimalVal(dec) {
    if (dec == null || dec == '') {
        return '';
    }
    var strDec = dec.toFixed(2);
    var decSeperator = getDecimalSeperator();
    if (decSeperator == '.') {
        return strDec;
    }
    else {
        return strDec.replace('.', decSeperator);
    }
}
/* getDecimalSeperator - Özalp Döndüren 2015.12
 * Basitçe decimal ayıracının nokta yada virgül
 * karakterlerinden hangisi olduğunu yakalıyor.
 *  
 * Sayfanın bir yerinde alttaki koda ihtiyaç duyar.
 *  @Html.Hidden("decimalSample", 1.1m)
 */
function getDecimalSeperator() {
    var sample = $('#decimalSample').val();
    if (sample != null && sample.indexOf(',') > -1) {
        return ',';
    }
    else {
        return '.';
    }
}
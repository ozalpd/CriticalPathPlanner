function boolToGlyphicon(value) {
    if (value) {
        return '<span style="font-size:150%;" class="glyphicon glyphicon-check text-success"></span>';
    }
    else {
        return '<span style="font-size:150%;" class="glyphicon glyphicon-unchecked text-danger"></span>';
    }
}
function boolToGlyphRemoved(value) {
    if (value) {
        return '<span style="font-size:150%;" class="glyphicon glyphicon-remove text-danger"></span>';
    }
    else {
        return '<span style="font-size:150%;" class="glyphicon glyphicon-unchecked text-success"></span>';
    }
}
function boolCellRenderer(params) {
    return boolToGlyphicon(params.value);
}
function discontinueCellRenderer(params) {
    if (params.value) {
        var discontinueDate = new Date(params.data.DiscontinueDate);
        return '<div style="margin-top:-4px;">' + boolToGlyphRemoved(params.value) +
            ' <span class="checkboxText">' + discontinueDate.toLocaleDateString() + '</span></div>';
    }
    else {
        return '';
    }
}

function cancelledCellRenderer(params) {
    if (params.value) {
        var date = new Date(params.data.CancelDate);
        return '<div style="margin-top:-4px;"><span style="font-size:18px;" class="glyphicon glyphicon-remove text-danger"></span>' +
            ' <span class="checkboxText">' + date.toLocaleDateString() + '</span></div>';
    }
    else {
        return '';
    }
}

function dateCellRenderer(params) {
    if (params.value) {
        var date = new Date(params.value);
        return date.toLocaleDateString();
    }
    else {
        return '';
    }
}
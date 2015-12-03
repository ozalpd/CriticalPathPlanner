
function boolCellRenderer(params) {
    if (params.value) {
        return '<span style="font-size:16px;" class="glyphicon glyphicon-check text-success"></span>';
    }
    else {
        return '<span style="font-size:16px;" class="glyphicon glyphicon-unchecked text-danger"></span>';
    }
}

function discontinueCellRenderer(params) {
    if (params.value) {
        var discontinueDate = new Date(params.data.DiscontinueDate);
        return '<div style="margin-top:-4px;"><span style="font-size:18px;" class="glyphicon glyphicon-remove text-danger"></span>' +
            ' <span class="checkboxText">' + discontinueDate.toLocaleDateString() + '</span></div>';
    }
    else {
        return '';
    }
}
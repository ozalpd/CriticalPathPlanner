function numbersOnly(evt) {
    var arrayExceptions = [8, 9, 13, 16, 17, 18, 20, 27, 35, 36, 37,
        38, 39, 40, 45, 46, 144];
    if ((evt.keyCode < 48 || evt.keyCode > 57) &&
            (evt.keyCode < 96 || evt.keyCode > 106) && // NUMPAD
            $.inArray(evt.keyCode, arrayExceptions) === -1) {
        return false;
    }
}

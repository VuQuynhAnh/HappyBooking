window.createObjectURLFromArray = function (fileBytes) {
    var blob = new Blob([fileBytes]);
    var url = URL.createObjectURL(blob);
    return url;
};
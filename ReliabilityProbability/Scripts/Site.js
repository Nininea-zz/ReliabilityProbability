$(function () {
    doCheck();
    $('.el-prop').keyup(doCheck).focusout(doCheck);
    function doCheck() {
        var allFilled = true;
        $('.el-prop').each(function () {
            if ($(this).val() == '') {
                allFilled = false;
                return false;
            }
        });
        $('.btn-default').prop('disabled', !allFilled);
    }
});
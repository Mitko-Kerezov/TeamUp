(function () {
    var isHidden = true;
    var $additionalInfo = $('#additional-info-legend');
    
    $additionalInfo.click(reverseHide);

    function reverseHide() {
        var $siblings = $(this).siblings('fieldset');
        if (isHidden) {
            $siblings.removeClass('hidden');
        }
        else {
            $siblings.addClass('hidden');
        }
        isHidden = !isHidden;
    }
}())
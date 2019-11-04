var formdata = new FormData();
$.each($('#fileu').prop('files'), function (i, e) {
    formdata.append('files', e); style = "display: none"
});

done = false;
$('#alerts').html('');
$('#prog').show();
$.ajax({
    url: 'Files/uploadFile',
    type: "POST",
    data: formdata,
    processData: false,
    contentType: false,
    xhr: function() {
        var mxhr = $.ajaxSettings.xhr();
        if (mxhr.upload) {
            mxhr.upload.addEventListener('progress', progress, false);
        }
        return mxhr;
    },
    success: function (response) {
        done = true;
        if (response.status === "success") {
            $('#fileu').val('');
            $('#prog').hide();
            $('#progbar').css('width', `0%`).attr('aria-valuenow', 0);
        }
    },
    error: function () {
        $('#prog').hide();
        $('#progbar').css('width', `0%`).attr('aria-valuenow', 0);
    }
});

function progress(e) {
    if (e.lengthComputable && !done) {
        console.log(`${e.loaded} / ${e.total}`);
        var max = e.total;
        var current = e.loaded;
        var Percentage = Math.round((current * 100) / max);
        console.log(Percentage);
        $('#progbar').css('width', `${Percentage}%`).attr('aria-valuenow', Percentage);
    }
}

//HTML: <input type="file" name="files" id="fileu" multiple>
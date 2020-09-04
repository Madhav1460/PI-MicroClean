$(document).ready(function () {
    var url = 'OnlineTraning/gettraningdocument'
    GetProfile(url).then(data => {
        console.log(data);
        var docinfo = "";
        if (data.data.length !== 0) {
            for (var i = 0; i < data.data.length; i++) {

                docinfo += `<div class="col-lg-3 col-md-4 col-sm-6 col-12">`
                if (data.data[i].docTypeId == 1) {
                    docinfo += ` <div class="hovereffect">
                        <img class="img-responsive" src="/theams/dist/img/img1.jpg" alt="">
                        <div class="overlay">
                            <h2>`+ data.data[i].title + `</h2>
                            <p>`+ data.data[i].description + `</p>
                            <p><input type="hidden" id="hdnvideopath_`+ i + `" value="` + imagebaseurl + data.data[i].documnetPath + `"></input><a onclick="VideoPopUp(` + i + `);" target="_blank">View</a></p>
                        </div>
                    </div>`
                } else {
                    docinfo += ` <div class="hovereffect">
                        <img class="img-responsive" src="/theams/dist/img/img1.jpg" alt="">
                        <div class="overlay">
                            <h2>`+ data.data[i].title + `</h2>
                            <p>`+ data.data[i].description + `</p>
                            <p><a href="`+ imagebaseurl + data.data[i].documnetPath + `" target="_blank" download="nodownload">View</a></p>
                        </div>
                    </div>`
                }
                docinfo += `</div>`
            }
            $('#loadvideodata').html(docinfo);
        }
        else {
            docinfo += `<div class="no-doc"><p>Document not available at the moment. Please Contact Owner!</p></div>`;
            $('#loadvideodata').html(docinfo);
        }
    });
    $('#close').css('display', 'none');
    $('#close').click(function () {
        $('#videopreview').get(0).pause()
        $('#videopreview').css('display', 'none');
        $('#close').css('display', 'none');
    });
    $('div.divnoneed').css('display', 'block');
});
function VideoPopUp(ctr) {
    $('#frame').html('<video class="showvideo" controls="controls" muted controlsList="nodownload" src="' + $('#hdnvideopath_' + ctr + '').val() + '" id="videopreview"></video>');
    //$('#videopreview').css('display', 'block');
    //$('#videopreview').css('position', 'absolute');
    //$('#close').css('position', 'fixed');
    $('#close').css('display', 'block');
}
function datasave(url, formdata) {
    var token = localStorage.getItem('token');
    let fullurl = baseurl + url;
    return axios.post(fullurl, formdata, {
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            "Authorization": "Bearer " + token
        }
    }).then(res => {
        return res;
    }).catch(error => {
        console.log(error);
    });
}

function dataSaveForUser(url, formdata) {
    var token = localStorage.getItem('token');
    let fullurl = baseurl + url;
    return axios.post(fullurl, formdata, {
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            "Authorization": "Bearer " + token
        }
    }).then(res => {
        alert(res.data.message);
        window.location = "/Account/UserList";
    }).catch(error => {
        console.log(error)
    });
}

function uploadtrainingdoc(url, formdata) {
    var token = localStorage.getItem('token');
    let fullurl = baseurl + url;
    return axios.post(fullurl, formdata, {
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            "Authorization": "Bearer " + token
        }
    }).then(res => {
        hideProgress();
        window.location = "/Admin/TrainingDocUpload";
        alert("File Uploaded Successfully...");
        return res.data;
    }).catch(error => {
        console.log(error)
    });
}

function GetProfile(url) {
    let fullurl = baseurl + url;
    var token = localStorage.getItem('token');
    return axios.get(fullurl, {
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            "Authorization": "Bearer " + token
        }
    }).then(res => {
        return res.data;
    }).catch(error => {
        console.log(error)
    });
}
function downloadfile(url, imgdata, imgName) {
    if (imgdata === undefined) {
        return false;
    }
    var afterDot = imgdata.substr(imgdata.indexOf('.'));
    axios({
        url: url, params: {
            filePath: imgdata
        },
        method: 'GET',
        responseType: 'blob', // important
    }).then((response) => {
        if (response.data.type === 'text/plain') {
            alert('file not found for the requesting item');
            return false;
        }
        const url = window.URL.createObjectURL(new Blob([response.data]));
        const link = document.createElement('a');
        link.href = url;
        debugger;
        //link.setAttribute('download', imgName+'.'+ afterDot + '');
        link.setAttribute('download', imgName);
        document.body.appendChild(link);
        link.click();
    });
}

function GetKycDocumentProfile(url, query) {
    let fullurl = baseurl + url;
    var token = localStorage.getItem('token');
    return axios.get(fullurl, {
        params: {
            franchiseeId: query
        }
    }, {
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            "Authorization": "Bearer " + token
        }
    }).then(res => {
        return res.data;
    }).catch(error => {
        console.log(error)
    });
}

function GetFeeData(url, query) {
    let fullurl = baseurl + url + '?userid=' + query + '';
    var token = localStorage.getItem('token');
    return axios.get(fullurl,{
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            "Authorization": "Bearer " + token
        }
    }).then(res => {
        return res.data;
    }).catch(error => {
        console.log(error)
    });
}

function GetQuizForTest(url, data) {
    let fullurl = baseurl + url + '?TestId=' + data + '';
    var token = localStorage.getItem('token');
    return axios.get(fullurl, {
        headers: {
            'Authorization': `Bearer ${token}`
        }
    }, {
        params: {
            TestId: data
        }
    }).then(res => {
        return res.data;
    }).catch(error => {
        console.log(error)
    });
}
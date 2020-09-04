var sec = 0;
function GetQuiz(id) {
    var quizdatahtml = "";
    var stephtml = "";
    let questioncount = 1;
    let optioncount = 1;
    var url = 'EndUseTest/getendusertestbytestid'
    GetQuizForTest(url, id).then(data => {
        if (data.data !== null) {
            $("#overlay1").show();
            const alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".split('');
            quizdatahtml += `<h1>` + data.data.testName + ` | ` + data.data.userName + `</h1>
           <input type="hidden" id="hdntestid" name="TestId" value="`+ data.data.testId + `" />
                <input type="hidden" id="hdnuserid" name="UserId" value="`+ data.data.userId + `" /> `;
            for (var i = 0; i < data.data.questions.length; i++) {
                let count = 1;
                quizdatahtml += `<div class="tab"><section>
                    <div class="form-group"><div class="title">` + data.data.questions[i].question + `
                <input type="hidden" id="hdntimerval_`+ questioncount + `" value="` + data.data.questions[i].takeActionDuration + `" /></div>
                        <div>
                            <input type="hidden" id="hdnquestionid_`+ questioncount + `" value="` + data.data.questions[i].questionId + `" />
                                    </div>`
                for (var j = 0; j < data.data.questions[i].options.length; j++) {
                    quizdatahtml += `<div class="">
                                    <input type="hidden" id="hdnoptionselected_`+ optioncount + `">
                                    <input type="hidden" value="`+ data.data.questions[i].options[j].optionId + `" id="hdnoptionid_` + optioncount + `">
                                    <input type="hidden" value="`+ data.data.questions[i].options[j].submmitedAnswerId + `" id="hdnsubmmitedAnswerId_` + optioncount + `">
                                    <span><b>` + alphabet[j] + `</b>.</span>
                                    <input type="checkbox" onclick="checkedAnswerd(`+ optioncount + `);" data-value="` + questioncount + `" id="chkoptionid_` + optioncount + `">
                                    <label for="checkboxPrime">` + data.data.questions[i].options[j].option + ` </label></div>`

                    optioncount++;
                    count++;
                }
                questioncount++;
                quizdatahtml += `</div></section></div>`
                stephtml += `<span class="step"></span>`
            }
            $('#quizdatatab').html(quizdatahtml);
            $('#stepid').html(stephtml);
            $("#overlay1").hide();
            LoadTab();
        } else {
            alert(data.message);
            window.location.href = 'test';
        }
        
    });
}

var currentTab = 0; // Current tab is set to be the first tab (0)
function LoadTab() {
    showTab(currentTab); // Display the current tab 
}
var x = document.getElementsByClassName("tab");
function showTab(n) {
    // This function will display the specified tab of the form...
   
    if (x.length > 0) {
        sec = $('#hdntimerval_' + (n + 1) + '').val();
        x[n].style.display = "block";
    }
    //... and fix the Previous/Next buttons:

    if (n == x.length - 1) {
        document.getElementById("nextBtn").innerHTML = "Submit";
    } else {
        document.getElementById("nextBtn").innerHTML = "Next";
    }
    //... and run a function that will display the correct step indicator:
    fixStepIndicator(n);
}

function nextPrev(n) {
    // This function will figure out which tab to display
    var x = document.getElementsByClassName("tab");
    // Hide the current tab:
    if (x.length > 0) {
        x[currentTab].style.display = "none";
    }
    // Increase or decrease the current tab by 1:
    currentTab = currentTab + n;
    // if you have reached the end of the form...
    if (currentTab >= x.length) {
        // ... the form gets submitted:
        datapush();
        return false;
    }
    // Otherwise, display the correct tab:
    showTab(currentTab);
}
function fixStepIndicator(n) {
    // This function removes the "active" class of all steps...
    var i,
        x = document.getElementsByClassName("step");
    for (i = 0; i < x.length; i++) {
        x[i].className = x[i].className.replace(" active", "");
    }
    //... and adds the "active" class on the current step:
    x[n].className += " active";
}

function showTimer() {
    var h3 = document.getElementsByTagName("h3");
    h3[0].innerHTML = "";
    countDiv = document.getElementById("timer"),
        secpass,
        countDown = setInterval(function () {
            "use strict";
            secpass();
        }, 1000);

    function secpass() {
        "use strict";
        var min = Math.floor(sec / 60),
            remSec = sec % 60;

        if (remSec < 10) {
            remSec = "0" + remSec;
        }
        if (min < 10) {
            min = "0" + min;
        }
        countDiv.innerHTML = min + ":" + remSec;

        if (sec > 0) {
            sec = sec - 1;
        } else {
            clearInterval(countDown);
            if (x.length > 0) {
                nextPrev(1);
            }
            showTimer();
        }
    }
}
var questionArr = [];
var optionsArr = [];
var formData = new FormData();
let optionval = null;
let optionidval = null;
let questionidval = null;
var optionscount = 0;
var questionscount = 0;
function checkedAnswerd(ctr) {
    questionidval = $('#hdnquestionid_' + $('#chkoptionid_' + ctr + '').attr("data-value") + '').val();
    if (!questionArr.includes(questionidval)) {
        if (questionArr.length != 0) {
            questionscount++;
            optionscount = 0;
        }
        questionArr.push(questionidval);
        formData.append("QuestionsCommand[" + questionscount + "].QuestionId", questionidval);
    }
    let optionchecked = $('#chkoptionid_' + ctr + '').prop("checked");
    if (optionchecked === true) {
        optionval = $('#hdnoptionselected_' + ctr + '').val(true);
        optionidval = $('#hdnoptionid_' + ctr + '').val();
        submmitedAnswerval = $('#hdnsubmmitedAnswerId_' + ctr + '').val();
        formData.set("QuestionsCommand[" + questionscount + "].OptionsCommand[" + optionscount + "].OptionId", optionidval);
        formData.set("QuestionsCommand[" + questionscount + "].OptionsCommand[" + optionscount + "].SubmitedAnswerId", submmitedAnswerval);
        formData.set("QuestionsCommand[" + questionscount + "].OptionsCommand[" + optionscount + "].IsMatched", optionval.val());
        optionscount++;
    } else {
        optionscount = optionscount - 1;
        optionval = $('#hdnoptionselected_' + ctr + '').val(false);
        optionidval = $('#hdnoptionid_' + ctr + '').val();
        submmitedAnswerval = $('#hdnsubmmitedAnswerId_' + ctr + '').val();
        formData.set("QuestionsCommand[" + questionscount + "].OptionsCommand[" + optionscount + "].SubmitedAnswerId", submmitedAnswerval);
        formData.set("QuestionsCommand[" + questionscount + "].OptionsCommand[" + optionscount + "].OptionId", optionidval);
        formData.set("QuestionsCommand[" + questionscount + "].OptionsCommand[" + optionscount + "].IsMatched", optionval.val());
        optionscount++;
    }
}
function datapush() {
    let testidval = $('#hdntestid').val();
    let useridval = $('#hdnuserid').val();
    if (useridval !== undefined && testidval !== undefined) {
        formData.append("TestId", testidval);
        formData.append("UserId", useridval);
        for (var pair of formData.entries()) {
            console.log(pair[0] + ', ' + pair[1]);
        }
        datasave('EndUseTest/submittest', formData).then(data => {
            if (data.data.status) {
                window.location.href = 'result/?testid=' + testidval;
            } else {
                alert(data.data.message);
                window.location.href = 'test';
            }
        });
    }
}
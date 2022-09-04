// $(document).ready(function() {
// });

function trapTab(divId) {
    console.log("traptab")
    var focusableElements = "a[href], area[href], input:not([disabled]), select:not([disabled]), textarea:not([disabled]), button:not([disabled]), object, embed, *[tabindex], *[contenteditable], iframe";
    var elements = $("." + divId).find("*").filter(focusableElements);
    elements.get(0).focus();
    $(window).on("keydown", function(e) {
        if (e.which == 9) {
            var item = $(':focus');
            var index = elements.index(item);
            if (e.shiftKey) {
                if (index == 0) {
                    elements.get(elements.length - 1).focus();
                    e.preventDefault();
                }
            } else {
                if (index == (elements.length - 1)) {
                    elements.get(0).focus();
                    e.preventDefault();
                }
            }
        }
    });
}
$('a.delete').confirm({
    columnClass: 'col-md-offset-3 col-md-6 delete alert',
    icon: 'icon-times',
    title: 'Delete!',
    content: 'Lorem ipsum, or lipsum as it is sometimes known, is dummy text used in laying out print, graphic or web designs.',
    animateFromElement: false,
    buttons: {
        'Cancel': function() {},
        'Yes, delete': function() {
            $.alert('Canceled!');
        }
    }
});
$('a.success').confirm({
    columnClass: 'col-md-offset-3 col-md-6 success alert',
    icon: 'icon-check-sign',
    title: 'Success!',
    content: 'Lorem ipsum, or lipsum as it is sometimes known, is dummy text used in laying out print, graphic or web designs.',
    animateFromElement: false,
    buttons: {
        'Cancel': function() {},
        'Ok': function() {
            $.alert('Canceled!');
        }
    }
});

$('button.warning').confirm({
    columnClass: 'col-md-offset-3 col-md-6 warning alert',
    icon: 'icon-exclamation',
    title: 'Confirmation',
    content: 'You have not saved what you have edited. Do you want to continue without saving the edited field(s)?',
    animateFromElement: false,
    buttons: {
        'Cancel': function() {},
        'Yes': function() {
            parent.jQuery.colorbox.close();
        }
    }
});
// import { EditVideoInterviewQuestion } from "https://corporate3.bdjobs.com/js/video-interview-edit.js";
// $.getScript("https://corporate3.bdjobs.com/js/video-interview-edit.js");

// import questions from 'https://corporate3.bdjobs.com/js/video-interview-ex.js';

$('button.btnAlreadyInvited').confirm({
    columnClass: 'col-md-offset-3 col-md-6 warning alert',
    icon: 'icon-exclamation',
    title: 'Confirmation',
    content: "Maintain similar context to change the questions. Completely changed questions may create a mismatch with jobseeker's earlier submissions. Do you want to continue?",
    //content: 'Do you want?',
    animateFromElement: false,
    buttons: {
        'Cancel': function() {},
        'Yes': function() {

            let questionNo = $("#hidEdit-QuestionNo").val();
            let questionText = $("#hidEdit-QuestionText").val();
            let questionLimit = $("#hidEdit-QuestionLimit").val();
            let strUpdateTxt = $("#hidEdit-UpdateTxt").val();
            let hidIntQId = $("#hidEdit-IntQId").val();
            let targetQuestion = $("#hidEdit-TargetQuestion").val();
            //var targetQuestion = $(this).parents(".question");
            //var targetQuestion = $("#questionView" + questionNo).parents(".question");
            // var targetQuestion = document.querySelectorAll("#questionView");


            // $(funcEditQuestion(questionNo, questionNo, questionText, questionLimit, false, strUpdateTxt, hidIntQId, "")).insertAfter(targetQuestion.last());
            // //$(this).parents(".question").attr("question-no", questionNo).addClass("hide");
            // //$("#questionView" + questionNo).parents(".question").attr("question-no", questionNo).addClass("hide");
            // $('#questionView' + questionNo).addClass('hide');
            // $('#questionView' + questionNo).attr('question-no', questionNo);
            // $("#question" + questionNo).focus();



            //alert("questionNo: " + questionNo + "--questionText: " + questionText + "--questionLimit: " + questionLimit + "--strUpdateTxt: " + strUpdateTxt + "--hidIntQId: " + hidIntQId + "--targetQuestion: " + targetQuestion);
            //alert($(".question")[targetQuestion])
            EditVideoInterviewQuestion(questionNo, questionText, questionLimit, strUpdateTxt, hidIntQId, $(".question")[targetQuestion]);

            // questions.EditVideoInterviewQuestion(questionNo, questionText, questionLimit, strUpdateTxt, hidIntQId, targetQuestion);

            // $("#hidIsEditOptionOpen").val("true");
            // let isEditOptionOpen = $("#hidIsEditOptionOpen").val();
            // //strUpdateTxt,intQId,indexNo
            // if (isEditOptionOpen == "true") {
            //     $(funcEditQuestion(questionNo, questionNo, questionText, questionLimit, false, strUpdateTxt, hidIntQId, "")).insertAfter(targetQuestion.last());
            // }
            // //$(funcEditQuestion(questionNo, questionNo, questionText, questionLimit, false, strUpdateTxt, hidIntQId)).insertAfter(targetQuestion.eq(questionNo - 1));
            // $(this).parents(".question").attr("question-no", questionNo).addClass("hide");

            // // LC ALAMIN MIR
            // $("#question" + questionNo).focus();
        }
    }
});

//invited VIQ edited
// $('button.btnAlreadyInvited').confirm({
//     columnClass: 'col-md-offset-3 col-md-6 warning alert',
//     icon: 'icon-exclamation',
//     title: 'Confirmation',
//     content: 'This modification will affect to jobseeker!',
//     animateFromElement: false,
//     buttons: {
//         'Cancel': function() {},
//         'Yes': function() {

//             let questionNo = $("#hidEdit-QuestionNo").val();
//             let questionText = $("#hidEdit-QuestionText").val();
//             let questionLimit = $("#hidEdit-QuestionLimit").val();
//             let strUpdateTxt = $("#hidEdit-UpdateTxt").val();
//             let hidIntQId = $("#hidEdit-IntQId").val();
//             let targetQuestion = $("#hidEdit-TargetQuestion").val();
//             alert("questionNo: " + questionNo + "--questionText: " + questionText + "--questionLimit: " + questionLimit + "--strUpdateTxt: " + strUpdateTxt + "--hidIntQId: " + hidIntQId + "--targetQuestion: " + targetQuestion);

//             EditVideoInterviewQuestion(questionNo, questionText, questionLimit, strUpdateTxt, hidIntQId, targetQuestion);

//             // $("#hidIsEditOptionOpen").val("true");
//             // let isEditOptionOpen = $("#hidIsEditOptionOpen").val();
//             // //strUpdateTxt,intQId,indexNo
//             // if (isEditOptionOpen == "true") {
//             //     $(funcEditQuestion(questionNo, questionNo, questionText, questionLimit, false, strUpdateTxt, hidIntQId, "")).insertAfter(targetQuestion.last());
//             // }
//             // //$(funcEditQuestion(questionNo, questionNo, questionText, questionLimit, false, strUpdateTxt, hidIntQId)).insertAfter(targetQuestion.eq(questionNo - 1));
//             // $(this).parents(".question").attr("question-no", questionNo).addClass("hide");

//             // // LC ALAMIN MIR
//             // $("#question" + questionNo).focus();
//         }
//     }
// });


$('a.exists').confirm({
    columnClass: 'col-md-offset-3 col-md-6 exists alert',
    icon: 'icon-forbidden',
    title: 'Email address is already exists',
    content: '<ul><li>Lorem ipsum, or lipsum as it is sometimes known, is dummy text used in laying out print, graphic or web designs.</li><li>Lorem ipsum, or lipsum as it is sometimes known, is dummy text used in laying out print, graphic or web designs.</li><li>Lorem ipsum, or lipsum as it is sometimes known, is dummy text used in laying out print, graphic or web designs.</li></ul>',
    animateFromElement: false,
    buttons: {
        'Cancel': function() {
            return false;
        },
        'Yes': function() {
            parent.jQuery.colorbox.close();
        }
    }
});


$('button.DeleteOrderBtn').confirm({
    columnClass: 'col-md-offset-3 col-md-6 warning alert',
    icon: 'icon-exclamation',
    title: 'Confirmation',
    content: "Do you want to delete the order?",
    //content: 'Do you want?',
    animateFromElement: false,
    buttons: {
        'Cancel': function () { },
        'Yes': function () { }
    }
});

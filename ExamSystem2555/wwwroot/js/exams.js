
var currentIndex = 0;
var selectedAnswersList = [];

function loadQuestion() {
    var x = document.getElementById("question");
    x.innerHTML = questions[currentIndex].questionDisplay;


};

function loadAnswer() {

    var y = document.getElementById("answers");
    y.innerHTML = "";
    for (var i = 0; i < questions[currentIndex].possibleAnswers.length; i++) {
        y.innerHTML += questions[currentIndex].possibleAnswers[i].questionPossibleAnswer;
        y.innerHTML += `<label asp-for="@Model.Questions[${i}].PossibleAnswers[${i}].IsSelectedAnswer" class="control-label"></label>`;
        y.innerHTML += `<input class="form-check-input" type="checkbox" asp-for="@Model.Questions[${i}].PossibleAnswers[${i}].IsSelectedAnswer" id="check${i}" name="pAnswer"/>`;
    }

    //var answerList = document.querySelector("#answer-list");

    //for (var i = 0; i < questions[currentIndex].possibleAnswers.length; i++) {
    //    var answer = questions[currentIndex].possibleAnswers[i].questionPossibleAnswer;
    //    answerList.innerHTML += answer
    //    console.log(answer);
    //'<label asp-for="item.IsSelectedAnswer" class="control-label"></label>' +
    //'<input class="form-check-input" asp-for="item.IsSelectedAnswer" />' +
    //'<span asp-validation-for="item.QuestionPossibleAnswer" class="text-danger"></span>';
}

document.addEventListener("DOMContentLoaded", loadQuestion);
document.addEventListener("DOMContentLoaded", loadAnswer);


document.getElementById("previous").addEventListener("click", function () {
    if (currentIndex > 0) {
        currentIndex--;
        loadQuestion();
        loadAnswer();
    }
});

document.getElementById("next").addEventListener("click", function () {
    //$("check[type='checkbox']:checked").(function () { selectedAnswersList.push($(this).val()); });
    var answer = document.getElementById("check1");
    
    if (currentIndex < questions.length - 1) {
        currentIndex++;
        loadQuestion();
        loadAnswer();
    }
});




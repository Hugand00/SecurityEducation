let slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    let dots = document.getElementsByClassName("dot");
    let prevBtn = document.querySelector(".prev");
    let nextBtn = document.querySelector(".next");
    console.log(nextBtn, prevBtn);
    console.log(slides)
    if (n === slides.length) { nextBtn.style.display = "none"; }
    else { nextBtn.style.display = "block"; }
    if (n === 1) { prevBtn.style.display = "none" }
    else { prevBtn.style.display = "block" }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}

function checkAnswers(chapterId, episodeId) {
    var checkedAnswers = document.querySelectorAll('input[type="radio"]:checked');
    let correctAnswers = []
    if (Array.isArray(answers)) {
        console.log(answers)
    }
    
    answers.forEach(answer => {
        // Iterera genom alla checkedAnswers
        checkedAnswers.forEach(checkedAnswer => {
           
            if (answer.Id == checkedAnswer.value && answer.IsCorrect == true) {
                correctAnswers.push(answer);
            }
        });
    });

    window.sessionStorage.setItem('correctAnswers', correctAnswers.length)
    const fullUrl = `${url}/${chapterId}/${episodeId}`;
    location.href = fullUrl;
}

function checkFinalAnswers() {
    console.log("Knappen fungerar!");
    var checkedAnswers = document.querySelectorAll('input[type="radio"]:checked');
    let correctAnswers = [];

    if (Array.isArray(answers)) {
        console.log(answers);
    }

    answers.forEach(answer => {
        checkedAnswers.forEach(checkedAnswer => {
            if (answer.Id == checkedAnswer.value && answer.IsCorrect === true) {
                correctAnswers.push(answer);
            }
        });
    });

    const correctCount = correctAnswers.length;
    const totalQuestions = document.querySelectorAll('.question-block').length;

    // Spara till sessionStorage så vi kan hämta i resultatsidan om vi vill
    window.sessionStorage.setItem('correctFinalAnswers', correctCount);
    window.sessionStorage.setItem('totalQuestions', totalQuestions);

    // Vidare till resultatsidan för slutprov
    location.href = "/Test/ExaminationResult";
}



